﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pyro.DataModel.DatabaseModel;
using Pyro.DataModel.DatabaseModel.Base;
using Pyro.DataModel.Support;
using Pyro.DataModel.IndexSetter;
using Pyro.Common.BusinessEntities.Search;
using Pyro.Common.Interfaces;
using Pyro.Common.Interfaces.Repositories;
using Pyro.Common.Interfaces.UriSupport;
using Hl7.Fhir.Model;
using Hl7.Fhir.Introspection;



namespace Pyro.DataModel.Repository
{
  public partial class PaymentNoticeRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_PaymentNotice, new() 
    where ResourceHistoryType :Res_PaymentNotice_History, new()
  {
    public PaymentNoticeRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_PaymentNotice_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.created_DateTimeOffset = null;      
      ResourceEntity.organization_identifier_Code = null;      
      ResourceEntity.organization_identifier_System = null;      
      ResourceEntity.organization_reference_VersionId = null;      
      ResourceEntity.organization_reference_FhirId = null;      
      ResourceEntity.organization_reference_Type = null;      
      ResourceEntity.organization_reference_Url = null;      
      ResourceEntity.organization_reference_ServiceRootURL_StoreID = null;      
      ResourceEntity.payment_status_Code = null;      
      ResourceEntity.payment_status_System = null;      
      ResourceEntity.provider_identifier_Code = null;      
      ResourceEntity.provider_identifier_System = null;      
      ResourceEntity.provider_reference_VersionId = null;      
      ResourceEntity.provider_reference_FhirId = null;      
      ResourceEntity.provider_reference_Type = null;      
      ResourceEntity.provider_reference_Url = null;      
      ResourceEntity.provider_reference_ServiceRootURL_StoreID = null;      
      ResourceEntity.request_identifier_Code = null;      
      ResourceEntity.request_identifier_System = null;      
      ResourceEntity.request_reference_VersionId = null;      
      ResourceEntity.request_reference_FhirId = null;      
      ResourceEntity.request_reference_Type = null;      
      ResourceEntity.request_reference_Url = null;      
      ResourceEntity.request_reference_ServiceRootURL_StoreID = null;      
      ResourceEntity.response_identifier_Code = null;      
      ResourceEntity.response_identifier_System = null;      
      ResourceEntity.response_reference_VersionId = null;      
      ResourceEntity.response_reference_FhirId = null;      
      ResourceEntity.response_reference_Type = null;      
      ResourceEntity.response_reference_Url = null;      
      ResourceEntity.response_reference_ServiceRootURL_StoreID = null;      
      ResourceEntity.statusdate_Date = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_PaymentNotice_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_PaymentNotice_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_PaymentNotice_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_PaymentNotice_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as PaymentNotice;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Created != null)
      {
        if (ResourceTyped.CreatedElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.CreatedElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.created_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Organization != null)
      {
        if (ResourceTyped.Organization is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Organization, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.organization_identifier_Code = Index.Code;
            ResourseEntity.organization_identifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Organization != null)
      {
        if (ResourceTyped.Organization is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Organization, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.organization_reference_Type = Index.Type;
            ResourseEntity.organization_reference_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.organization_reference_Url = Index.Url;
            }
            else
            {
              ResourseEntity.organization_reference_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.PaymentStatus != null)
      {
        if (ResourceTyped.PaymentStatus is Hl7.Fhir.Model.Coding)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.PaymentStatus, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.payment_status_Code = Index.Code;
            ResourseEntity.payment_status_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Provider != null)
      {
        if (ResourceTyped.Provider is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Provider, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.provider_identifier_Code = Index.Code;
            ResourseEntity.provider_identifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Provider != null)
      {
        if (ResourceTyped.Provider is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Provider, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.provider_reference_Type = Index.Type;
            ResourseEntity.provider_reference_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.provider_reference_Url = Index.Url;
            }
            else
            {
              ResourseEntity.provider_reference_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Request != null)
      {
        if (ResourceTyped.Request is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Request, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.request_identifier_Code = Index.Code;
            ResourseEntity.request_identifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Request != null)
      {
        if (ResourceTyped.Request is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Request, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.request_reference_Type = Index.Type;
            ResourseEntity.request_reference_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.request_reference_Url = Index.Url;
            }
            else
            {
              ResourseEntity.request_reference_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Response != null)
      {
        if (ResourceTyped.Response is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Response, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.response_identifier_Code = Index.Code;
            ResourseEntity.response_identifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Response != null)
      {
        if (ResourceTyped.Response is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Response, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.response_reference_Type = Index.Type;
            ResourseEntity.response_reference_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.response_reference_Url = Index.Url;
            }
            else
            {
              ResourseEntity.response_reference_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.StatusDate != null)
      {
        if (ResourceTyped.StatusDateElement is Hl7.Fhir.Model.Date)
        {
          var Index = new DateIndex();
          Index = IndexSetterFactory.Create(typeof(DateIndex)).Set(ResourceTyped.StatusDateElement, Index) as DateIndex;
          if (Index != null)
          {
            ResourseEntity.statusdate_Date = Index.Date;
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_PaymentNotice_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_PaymentNotice_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Profile != null)
        {
          foreach (var item4 in ResourceTyped.Meta.ProfileElement)
          {
            if (item4 is Hl7.Fhir.Model.FhirUri)
            {
              var Index = new Res_PaymentNotice_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_PaymentNotice_Index__profile;
              ResourseEntity._profile_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Security != null)
        {
          foreach (var item4 in ResourceTyped.Meta.Security)
          {
            if (item4 is Hl7.Fhir.Model.Coding)
            {
              var Index = new Res_PaymentNotice_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_PaymentNotice_Index__security;
              ResourseEntity._security_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Tag != null)
        {
          foreach (var item4 in ResourceTyped.Meta.Tag)
          {
            if (item4 is Hl7.Fhir.Model.Coding)
            {
              var Index = new Res_PaymentNotice_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_PaymentNotice_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

