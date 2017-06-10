﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Pyro.Common.Interfaces.Service;
using Pyro.Common.Interfaces.UriSupport;
using Pyro.Common.Interfaces.Dto;
using Pyro.Common.Interfaces.Dto.Headers;
using Pyro.Common.Tools;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Model;
using Pyro.Common.Extentions;

namespace Pyro.Common.BusinessEntities.Service
{
  public class BundleTransactionService : IBundleTransactionService
  {
    private IResourceServiceRequestTransactionBundle _ResourceServiceRequest;
    private IResourceServiceOutcome _ServiceOperationOutcome;
    private Dictionary<string, string> OldNewResourceReferanceMap;
    
    internal BundleTransactionService(IResourceServiceRequestTransactionBundle ResourceServiceRequestTransactionBundle)
    {
      _ResourceServiceRequest = ResourceServiceRequestTransactionBundle;
    }

    public IResourceServiceOutcome Transact()
    {
      if (_ResourceServiceRequest.ResourceServices == null)
      {
        throw new ArgumentNullException("DefaultResourceServices can not be null.");
      }
      if (_ResourceServiceRequest == null)
      {
        throw new ArgumentNullException("_ResourceServiceRequest can not be null.");
      }

      _ServiceOperationOutcome = Common.CommonFactory.GetResourceServiceOutcome();
      _ServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;     
      Bundle bundle = _ResourceServiceRequest.Resource as Bundle;

      if (bundle == null)
      {
        var Message = $"The FHIR server's service root endpoint can only accept 'Bundle' resources. Resource received was: {_ResourceServiceRequest.Resource.ResourceType.ToString()}";
        var OpOutcome = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid, Message);
        _ServiceOperationOutcome.ResourceResult = OpOutcome;
        _ServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Forbidden;
        _ServiceOperationOutcome.OperationType = Enum.RestEnum.CrudOperationType.Create;
        _ServiceOperationOutcome.ServiceRootUri = _ResourceServiceRequest.RequestUri.PrimaryRootUrlStore.RootUri;
        return _ServiceOperationOutcome;
      }
      else if (bundle.Type != Bundle.BundleType.Transaction || (bundle.Type != Bundle.BundleType.Transaction && bundle.Type != Bundle.BundleType.Batch))
      {
        var Message = $"The FHIR server's service root endpoint can only accept Bundle resources of Bundle.type = 'Transaction' or Bundle.type = 'Batch'. Type found was {bundle.Type.ToString()}";
        var OpOutcome = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid, Message);
        _ServiceOperationOutcome.ResourceResult = OpOutcome;
        _ServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Forbidden;
        _ServiceOperationOutcome.OperationType = Enum.RestEnum.CrudOperationType.Create;
        _ServiceOperationOutcome.ServiceRootUri = _ResourceServiceRequest.RequestUri.PrimaryRootUrlStore.RootUri;
        return _ServiceOperationOutcome;
      }
      else
      {

        bundle.Type = Bundle.BundleType.TransactionResponse;
        bundle.Id = Guid.NewGuid().ToString();
        if (bundle.Meta == null)
          bundle.Meta = new Meta();
        bundle.Meta.LastUpdated = DateTimeOffset.Now;
        

        //FHIR Spec: http://build.fhir.org/bundle.html#transaction
        //  If there is no request element, then there SHALL be a resource and the server must infer 
        //  whether this is a create or an update from the resource identity supplied.
        //  NO Request Processing        
        foreach (var NoRequestEntry in bundle.Entry.Where(x => x.Request == null || !x.Request.Method.HasValue))
        {
          NoRequestEntry.Request = GenerateRequestComponentForEntry(NoRequestEntry);
        }

        //All entries with a Request, shouidl be all at this point
        var EntryWithRequestList = bundle.Entry.Where(x => x.Request != null && x.Request.Method.HasValue);
        var EntryWithResourceAndRequestList = EntryWithRequestList.Where(x => x.Resource != null);

        var DeleteEntries = EntryWithRequestList.Where(x => x.Request.Method == Bundle.HTTPVerb.DELETE);
        var POSTEntries = EntryWithResourceAndRequestList.Where(x => x.Request.Method == Bundle.HTTPVerb.POST);
        var PUTEntries = EntryWithResourceAndRequestList.Where(x => x.Request.Method == Bundle.HTTPVerb.PUT);
        var GETEntries = EntryWithRequestList.Where(x => x.Request.Method == Bundle.HTTPVerb.GET);
        
        try
        {
          //DELETE Processing
          foreach (var DeleteEntry in DeleteEntries)
          {
            if (!DeleteProcessing(DeleteEntry))
            {
              return _ServiceOperationOutcome;
            }
          }

          //Assign new id's for POSTs and then update all POST and PUT entrie referances          
          AssignResourceIdsAndUpdateReferances(POSTEntries, PUTEntries);

          //POST Processing        
          foreach (var PostEntry in POSTEntries)
          {
            if (!PostProcessing(PostEntry))
            {
              _ServiceOperationOutcome.SuccessfulTransaction = false;
              return _ServiceOperationOutcome;
            }
          }

          //PUT Processing        
          foreach (var PutEntry in PUTEntries)
          {
            if (!PutProcessing(PutEntry))
            {
              _ServiceOperationOutcome.SuccessfulTransaction = false;
              return _ServiceOperationOutcome;
            }
          }

          //GET Processing        
          foreach (var GetEntry in GETEntries)
          {
            if (!GetProcessing(GetEntry))
            {
              _ServiceOperationOutcome.SuccessfulTransaction = false;
              return _ServiceOperationOutcome;
            }
          }

          _ServiceOperationOutcome.ResourceResult = bundle;
          _ServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.OK;          
          _ServiceOperationOutcome.OperationType = Enum.RestEnum.CrudOperationType.Update;
          _ServiceOperationOutcome.SuccessfulTransaction = true;          
        }
        catch (Exception Exec)
        {                    
          throw new Dto.DtoPyroException(System.Net.HttpStatusCode.InternalServerError, FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Exception, Exec.Message), Exec.Message);          
        }       
      }

      return _ServiceOperationOutcome;
    }    
    
    private bool DeleteProcessing(Bundle.EntryComponent DeleteEntry)
    {
      IFhirRequestUri EntryFhirRequestUri = Common.CommonFactory.GetFhirRequestUri(_ResourceServiceRequest.RequestUri.PrimaryRootUrlStore.Url, ConstructRequestUrl(DeleteEntry));
      IDtoRequestUri EntryRequestUri = Common.CommonFactory.GetRequestUri(_ResourceServiceRequest.RequestUri.PrimaryRootUrlStore, EntryFhirRequestUri);
      
      IDtoSearchParameterGeneric SearchParameterGeneric = Common.CommonFactory.GetDtoSearchParameterGeneric(EntryRequestUri.FhirRequestUri.Query);
      _ResourceServiceRequest.ResourceServices.SetCurrentResourceType(EntryRequestUri.FhirRequestUri.ResourseName);
      //var ResourceService = _ResourceServiceRequest.ServiceNegotiator.GetResourceService(EntryRequestUri.FhirRequestUri.ResourseName);
      IResourceServiceOutcome ResourceServiceOutcome = null;
      if (SearchParameterGeneric.ParameterList.Count > 0)
      {
        IResourceServiceRequestConditionalDelete ResourceServiceRequestConditionalDelete = Common.CommonFactory.GetResourceServiceRequestConditionalDelete(EntryRequestUri, SearchParameterGeneric);
        ResourceServiceOutcome = _ResourceServiceRequest.ResourceServices.ConditionalDelete(ResourceServiceRequestConditionalDelete);
      }
      else
      {
        IResourceServiceRequestDelete ResourceServiceRequest = Common.CommonFactory.GetResourceServiceRequestDelete(EntryRequestUri.FhirRequestUri.ResourceId, EntryRequestUri, SearchParameterGeneric);
        ResourceServiceOutcome = _ResourceServiceRequest.ResourceServices.Delete(ResourceServiceRequest);
      }

      if (ResourceServiceOutcome.SuccessfulTransaction)
      {
        DeleteEntry.Response = new Bundle.ResponseComponent();
        DeleteEntry.Response.Status = FormatHTTPStatusCodeAsString(ResourceServiceOutcome.HttpStatusCode);
        DeleteEntry.FullUrl = null;
        if (ResourceServiceOutcome.ResourceResult != null)
        {
          if (ResourceServiceOutcome.ResourceResult.ResourceType == ResourceType.OperationOutcome)
          {
            DeleteEntry.Response.Outcome = ResourceServiceOutcome.ResourceResult;
          }
          else
          {
            DeleteEntry.Resource = ResourceServiceOutcome.ResourceResult;
          }
        }
        if (ResourceServiceOutcome.LastModified != null && ResourceServiceOutcome.ResourceVersionNumber != null)
        {
          DeleteEntry.Response.Etag = HttpHeaderSupport.GetEntityTagHeaderValueFromVersion(ResourceServiceOutcome.ResourceVersionNumber).ToString();
          DeleteEntry.Response.LastModified = ResourceServiceOutcome.LastModified;          
        }
        return true;
      }
      else
      {
        if (ResourceServiceOutcome.ResourceResult != null && ResourceServiceOutcome.ResourceResult is OperationOutcome Op)
        {
          IdentifieBatchEntityToClient(Op, DeleteEntry.FullUrl);
        }
        _ServiceOperationOutcome = ResourceServiceOutcome;
        return false;
      }
    }
    private bool PostProcessing(Bundle.EntryComponent PostEntry)
    {
      IFhirRequestUri EntryFhirRequestUri = Common.CommonFactory.GetFhirRequestUri(_ResourceServiceRequest.RequestUri.PrimaryRootUrlStore.Url, ConstructRequestUrl(PostEntry));
      IDtoRequestUri EntryRequestUri = Common.CommonFactory.GetRequestUri(_ResourceServiceRequest.RequestUri.PrimaryRootUrlStore, EntryFhirRequestUri);      
      if (EntryRequestUri.FhirRequestUri.IsOperation)
      {
        var Message = $"The FHIR server does not support the use of Operations within Transaction Bundles, found Operation request type of : '{EntryRequestUri.FhirRequestUri.OperationName}'.";
        var OpOutcome = FhirOperationOutcomeSupport.Create(OperationOutcome.IssueSeverity.Error, OperationOutcome.IssueType.Invalid, Message);
        _ServiceOperationOutcome.ResourceResult = OpOutcome;
        _ServiceOperationOutcome.HttpStatusCode = System.Net.HttpStatusCode.Forbidden;
        _ServiceOperationOutcome.OperationType = Enum.RestEnum.CrudOperationType.Create;
        _ServiceOperationOutcome.ServiceRootUri = _ResourceServiceRequest.RequestUri.PrimaryRootUrlStore.RootUri;
        return false;
      }
      IFhirRequestUri ResourceIdToForce = Common.CommonFactory.GetFhirRequestUri(EntryRequestUri.PrimaryRootUrlStore.Url, OldNewResourceReferanceMap[GetUUIDfromFullURL(PostEntry.FullUrl)]);     
      IDtoRequestHeaders RequestHeaders = Common.CommonFactory.GetDtoRequestHeaders(PostEntry.Request);
      IDtoSearchParameterGeneric SearchParameterGeneric = Common.CommonFactory.GetDtoSearchParameterGeneric(EntryRequestUri.FhirRequestUri.Query);
      IResourceServiceRequestPost ResourceServiceRequestPost = Common.CommonFactory.GetResourceServiceRequestPost(PostEntry.Resource, EntryRequestUri, SearchParameterGeneric, RequestHeaders, ResourceIdToForce.ResourceId);
      _ResourceServiceRequest.ResourceServices.SetCurrentResourceType(EntryRequestUri.FhirRequestUri.ResourseName);
      //IResourceServices ResourceService = _ResourceServiceRequest.ServiceNegotiator.GetResourceService(EntryRequestUri.FhirRequestUri.ResourseName);
      IResourceServiceOutcome ResourceServiceOutcome = _ResourceServiceRequest.ResourceServices.Post(ResourceServiceRequestPost);

      if (ResourceServiceOutcome.SuccessfulTransaction)
      {
        PostEntry.FullUrl = CreateFullUrl(ResourceServiceOutcome);
        PostEntry.Response = new Bundle.ResponseComponent();
        PostEntry.Response.Status = FormatHTTPStatusCodeAsString(ResourceServiceOutcome.HttpStatusCode);

        if (ResourceServiceOutcome.ResourceResult != null)
        {
          if (ResourceServiceOutcome.ResourceResult.ResourceType == ResourceType.OperationOutcome)
          {
            PostEntry.Response.Outcome = ResourceServiceOutcome.ResourceResult;
          }
          else
          {
            PostEntry.Resource = ResourceServiceOutcome.ResourceResult;
          }
        }
        if (ResourceServiceOutcome.LastModified != null)
        {
          PostEntry.Response.Etag = HttpHeaderSupport.GetEntityTagHeaderValueFromVersion(ResourceServiceOutcome.ResourceVersionNumber).ToString();
          PostEntry.Response.LastModified = ResourceServiceOutcome.LastModified;
        }
        PostEntry.Response.Status = $"{((int)ResourceServiceOutcome.HttpStatusCode).ToString()} {ResourceServiceOutcome.HttpStatusCode.ToString()}";
        PostEntry.Response.Location = ResourceServiceOutcome.RequestUri.OriginalString;
        return true;
      }
      else
      {
        if (ResourceServiceOutcome.ResourceResult != null && ResourceServiceOutcome.ResourceResult is OperationOutcome Op)
        {
          IdentifieBatchEntityToClient(Op, PostEntry.FullUrl);
        }      
        _ServiceOperationOutcome = ResourceServiceOutcome;
        return false;
      }
    }    
    private bool PutProcessing(Bundle.EntryComponent PutEntry)
    {
      IFhirRequestUri EntryFhirRequestUri = Common.CommonFactory.GetFhirRequestUri(_ResourceServiceRequest.RequestUri.PrimaryRootUrlStore.Url, ConstructRequestUrl(PutEntry));
      IDtoRequestUri EntryRequestUri = Common.CommonFactory.GetRequestUri(_ResourceServiceRequest.RequestUri.PrimaryRootUrlStore, EntryFhirRequestUri);      
      IDtoRequestHeaders RequestHeaders = Common.CommonFactory.GetDtoRequestHeaders(PutEntry.Request);
      IDtoSearchParameterGeneric SearchParameterGeneric = Common.CommonFactory.GetDtoSearchParameterGeneric(EntryRequestUri.FhirRequestUri.Query);
      //var ResourceService = _ResourceServiceRequest.ServiceNegotiator.GetResourceService(EntryRequestUri.FhirRequestUri.ResourseName);
      _ResourceServiceRequest.ResourceServices.SetCurrentResourceType(EntryRequestUri.FhirRequestUri.ResourseName);
      IResourceServiceOutcome ResourceServiceOutcome = null;
      if (SearchParameterGeneric.ParameterList.Count > 0)
      {
        IResourceServiceRequestConditionalPut PyroServiceRequestConditionalPut = CommonFactory.GetResourceServiceRequestConditionalPut(PutEntry.Resource, EntryRequestUri, SearchParameterGeneric);
        ResourceServiceOutcome = _ResourceServiceRequest.ResourceServices.ConditionalPut(PyroServiceRequestConditionalPut);
      }
      else
      {
        IResourceServiceRequestPut ResourceServiceRequestPut = Common.CommonFactory.GetResourceServiceRequestPut(EntryRequestUri.FhirRequestUri.ResourceId, PutEntry.Resource, EntryRequestUri, SearchParameterGeneric, RequestHeaders);
        ResourceServiceOutcome = _ResourceServiceRequest.ResourceServices.Put(ResourceServiceRequestPut);
      }

      if (ResourceServiceOutcome.SuccessfulTransaction)
      {
        PutEntry.FullUrl = CreateFullUrl(ResourceServiceOutcome);
        PutEntry.Response = new Bundle.ResponseComponent();
        PutEntry.Response.Status = FormatHTTPStatusCodeAsString(ResourceServiceOutcome.HttpStatusCode);

        if (ResourceServiceOutcome.ResourceResult != null)
        {
          if (ResourceServiceOutcome.ResourceResult.ResourceType == ResourceType.OperationOutcome)
          {
            PutEntry.Response.Outcome = ResourceServiceOutcome.ResourceResult;
          }
          else
          {
            PutEntry.Resource = ResourceServiceOutcome.ResourceResult;
          }
        }
        if (ResourceServiceOutcome.LastModified != null)
        {
          PutEntry.Response.Etag = HttpHeaderSupport.GetEntityTagHeaderValueFromVersion(ResourceServiceOutcome.ResourceVersionNumber).ToString();
          PutEntry.Response.LastModified = ResourceServiceOutcome.LastModified;
          PutEntry.Response.Location = EntryRequestUri.FhirRequestUri.OriginalString;
        }
        return true;
      }
      else
      {
        if (ResourceServiceOutcome.ResourceResult != null && ResourceServiceOutcome.ResourceResult is OperationOutcome Op)
        {
          IdentifieBatchEntityToClient(Op, PutEntry.FullUrl);
        }      
        _ServiceOperationOutcome = ResourceServiceOutcome;        
        return false;
      }
    }
    private bool GetProcessing(Bundle.EntryComponent GetEntry)
    {
      IFhirRequestUri EntryFhirRequestUri = Common.CommonFactory.GetFhirRequestUri(_ResourceServiceRequest.RequestUri.PrimaryRootUrlStore.Url, ConstructRequestUrl(GetEntry));
      IDtoRequestUri EntryRequestUri = Common.CommonFactory.GetRequestUri(_ResourceServiceRequest.RequestUri.PrimaryRootUrlStore, EntryFhirRequestUri);      
      IDtoRequestHeaders RequestHeaders = Common.CommonFactory.GetDtoRequestHeaders(GetEntry.Request);
      IDtoSearchParameterGeneric SearchParameterGeneric = Common.CommonFactory.GetDtoSearchParameterGeneric(EntryRequestUri.FhirRequestUri.Query);
      IResourceServiceOutcome ResourceServiceOutcome = null;
      //var ResourceService = _ResourceServiceRequest.ServiceNegotiator.GetResourceService(EntryRequestUri.FhirRequestUri.ResourseName);
      _ResourceServiceRequest.ResourceServices.SetCurrentResourceType(EntryRequestUri.FhirRequestUri.ResourseName);
      if (SearchParameterGeneric.ParameterList.Count > 0)
      {
        IResourceServiceRequestGetSearch ResourceServiceRequestGetSearch = Common.CommonFactory.GetResourceServiceRequestGetSearch(EntryRequestUri, SearchParameterGeneric);
        ResourceServiceOutcome = _ResourceServiceRequest.ResourceServices.GetSearch(ResourceServiceRequestGetSearch);
      }
      else
      {
        IResourceServiceRequestGetRead ResourceServiceRequestGetRead = Common.CommonFactory.GetResourceServiceRequestGetRead(EntryRequestUri.FhirRequestUri.ResourceId, _ResourceServiceRequest.RequestUri, SearchParameterGeneric, RequestHeaders);
        ResourceServiceOutcome = _ResourceServiceRequest.ResourceServices.GetRead(ResourceServiceRequestGetRead);
      }

      if (ResourceServiceOutcome.SuccessfulTransaction)
      {
        GetEntry.FullUrl = CreateFullUrl(ResourceServiceOutcome);
        GetEntry.Response = new Bundle.ResponseComponent();
        GetEntry.Response.Status = FormatHTTPStatusCodeAsString(ResourceServiceOutcome.HttpStatusCode);

        if (ResourceServiceOutcome.ResourceResult != null)
        {
          if (ResourceServiceOutcome.ResourceResult.ResourceType == ResourceType.OperationOutcome)
          {
            GetEntry.Response.Outcome = ResourceServiceOutcome.ResourceResult;
          }
          else
          {
            GetEntry.Resource = ResourceServiceOutcome.ResourceResult;
          }
        }
        if (ResourceServiceOutcome.LastModified.HasValue)
        {
          GetEntry.Response.Etag = HttpHeaderSupport.GetEntityTagHeaderValueFromVersion(ResourceServiceOutcome.ResourceVersionNumber).ToString();
          if (ResourceServiceOutcome.IsDeleted.HasValue && !ResourceServiceOutcome.IsDeleted.Value)
            GetEntry.Response.LastModified = ResourceServiceOutcome.LastModified;
          GetEntry.Response.Location = EntryRequestUri.FhirRequestUri.OriginalString;
        }
        return true;
      }
      else
      {
        if (ResourceServiceOutcome.ResourceResult != null && ResourceServiceOutcome.ResourceResult is OperationOutcome Op)
        {
          IdentifieBatchEntityToClient(Op, GetEntry.FullUrl);
        }      
        _ServiceOperationOutcome = ResourceServiceOutcome;
        return false;
      }
    }

    private string ConstructRequestUrl(Bundle.EntryComponent Entry)
    {
      return _ResourceServiceRequest.RequestUri.PrimaryRootUrlStore.RootUri + "/" + Entry.Request.Url;      
    }

    private string CreateFullUrl(IResourceServiceOutcome ResourceServiceOutcome)
    {
      //Get Search results will have either a bundle or no resource and do not require FullURLs
      if (ResourceServiceOutcome.ResourceResult != null && ResourceServiceOutcome.ResourceResult.ResourceType != ResourceType.Bundle)
      {       
        return $"{ResourceServiceOutcome.ServiceRootUri.OriginalString}/{ResourceServiceOutcome.ResourceResult.TypeName}/{ResourceServiceOutcome.FhirResourceId}";       
      }
      return null;
    }

    private void IdentifieBatchEntityToClient(OperationOutcome op, string FullURL)
    {
      string Message = $"Issue found with the bundel entry identified by the FullURL: {FullURL}";
      OperationOutcome NewOp = FhirOperationOutcomeSupport.Append(OperationOutcome.IssueSeverity.Information, OperationOutcome.IssueType.Informational, Message, op);
    }

    private void AssignResourceIdsAndUpdateReferances(IEnumerable<Bundle.EntryComponent> PostEntryList, IEnumerable<Bundle.EntryComponent> PutEntryList)
    {
      OldNewResourceReferanceMap = new Dictionary<string, string>();
      //First assign a new GUID id for all FullUrls
      foreach (var PostEntry in PostEntryList)
      {
        string NewId = Guid.NewGuid().ToString();
        string ResourceName = PostEntry.Resource.ResourceType.ToString();    
        string FullUrlUUID = GetUUIDfromFullURL(PostEntry.FullUrl);
        OldNewResourceReferanceMap.Add(FullUrlUUID, $"{ResourceName}/{NewId}");
      }

      //Then roll through all POST and PUT entries updating referances     
      IEnumerable<Bundle.EntryComponent> PostAndPutList = PostEntryList.Concat(PutEntryList);
      foreach (var PostPutItem in PostAndPutList)
      {
        if (PostPutItem.Resource is DomainResource dr)
        {
          //Update all <a href="" & <img src=""
          dr.Text.UpdateAllReferances(OldNewResourceReferanceMap);
        }
      }

      List<ResourceReference> RefList = PostAndPutList.AllReferences();
      foreach (var resRef in RefList.Where(rr => !string.IsNullOrEmpty(rr.Reference)))
      {
        // TODO: If this is an identifier reference, then we must resolve the reference
        // Otherwise these are just normal references
        string UUID = resRef.Reference.Split(':')[resRef.Reference.Split(':').Length - 1];
        if (OldNewResourceReferanceMap.ContainsKey(UUID))
        {
          resRef.Reference = OldNewResourceReferanceMap[UUID];
        }
      }
    }
    private string GetUUIDfromFullURL(string FullURL)
    {
      string Result = FullURL;
      if (FullURL.Contains(":"))
      {
        var FullURLUUIDSplit = FullURL.Split(':');
        if (FullURLUUIDSplit.Length == 3)
        {
          Result = FullURLUUIDSplit[2];
        }
        else if (FullURLUUIDSplit.Length == 2)
        {
          Result = FullURLUUIDSplit[1];
        }
      }
      return Result;
    }
    private string FormatHTTPStatusCodeAsString(System.Net.HttpStatusCode HttpStatusCode)
    {
     return  $"{((int)HttpStatusCode).ToString()} {HttpStatusCode.ToString()}";
    }

    private Bundle.RequestComponent GenerateRequestComponentForEntry(Bundle.EntryComponent Entry)
    {
      var RequestComponent = new Bundle.RequestComponent();
      if (!string.IsNullOrWhiteSpace(Entry.Resource.Id))
      {
        //Perform a PUT (Update)
        RequestComponent.Method = Bundle.HTTPVerb.PUT;
        RequestComponent.Url = $"{Entry.Resource.TypeName}/{Entry.Resource.Id}";
      }
      else
      {
        //Perform a POST (Create)
        RequestComponent.Method = Bundle.HTTPVerb.POST;
        RequestComponent.Url = $"{Entry.Resource.TypeName}";
      }
      return RequestComponent;
    }
  }
}
