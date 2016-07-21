﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq.Expressions;
using Blaze.DataModel.DatabaseModel;
using Blaze.DataModel.DatabaseModel.Base;
using Blaze.DataModel.Support;
using Hl7.Fhir.Model;
using Blaze.Common.BusinessEntities;
using Blaze.Common.Interfaces;
using Blaze.Common.Interfaces.Repositories;
using Blaze.Common.Interfaces.UriSupport;
using Hl7.Fhir.Introspection;

namespace Blaze.DataModel.Repository
{
  public partial class EligibilityResponseRepository : CommonRepository, IResourceRepository
  {

    public EligibilityResponseRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    public IDatabaseOperationOutcome AddResource(Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as EligibilityResponse;
      var ResourceEntity = new Res_EligibilityResponse();
      this.PopulateResourceEntity(ResourceEntity, "1", ResourceTyped, FhirRequestUri);
      this.DbAddEntity<Res_EligibilityResponse>(ResourceEntity);
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;     
      DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ResourcesMatchingSearchCount = 1;
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome UpdateResource(string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as EligibilityResponse;
      var ResourceEntity = LoadCurrentResourceEntity(Resource.Id);
      var ResourceHistoryEntity = new Res_EligibilityResponse_History();  
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_EligibilityResponse_History_List.Add(ResourceHistoryEntity); 
      this.ResetResourceEntity(ResourceEntity);
      this.PopulateResourceEntity(ResourceEntity, ResourceVersion, ResourceTyped, FhirRequestUri);            
      this.Save();            
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ResourcesMatchingSearchCount = 1;
      return DatabaseOperationOutcome;
    }

    public void UpdateResouceAsDeleted(string FhirResourceId, string ResourceVersion)
    {
      var ResourceEntity = this.LoadCurrentResourceEntity(FhirResourceId);
      var ResourceHistoryEntity = new Res_EligibilityResponse_History();
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_EligibilityResponse_History_List.Add(ResourceHistoryEntity);
      this.ResetResourceEntity(ResourceEntity);
      ResourceEntity.IsDeleted = true;
      ResourceEntity.versionId = ResourceVersion;
      this.Save();      
    }

    public IDatabaseOperationOutcome GetResourceByFhirIDAndVersionNumber(string FhirResourceId, string ResourceVersionNumber)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      var ResourceHistoryEntity = DbGet<Res_EligibilityResponse_History>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
      if (ResourceHistoryEntity != null)
      {
        DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceHistoryEntity);
      }
      else
      {
        var ResourceEntity = DbGet<Res_EligibilityResponse>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
        if (ResourceEntity != null)
          DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);        
      }
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome GetResourceByFhirID(string FhirResourceId, bool WithXml = false)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      Blaze.Common.BusinessEntities.Dto.DtoResource DtoResource = null;
      if (WithXml)
      {        
        DtoResource = DbGetALL<Res_EligibilityResponse>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated, Xml = x.XmlBlob }).SingleOrDefault();       
      }
      else
      {
        DtoResource = DbGetALL<Res_EligibilityResponse>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated }).SingleOrDefault();        
      }
      DatabaseOperationOutcome.ResourceMatchingSearch = DtoResource;
      return DatabaseOperationOutcome;
    }

    private Res_EligibilityResponse LoadCurrentResourceEntity(string FhirId)
    {

      var IncludeList = new List<Expression<Func<Res_EligibilityResponse, object>>>();
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.profile_List);
      IncludeList.Add(x => x.security_List);
      IncludeList.Add(x => x.tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<Res_EligibilityResponse>(x => x.FhirId == FhirId, IncludeList);

      return ResourceEntity;
    }


    private void ResetResourceEntity(Res_EligibilityResponse ResourceEntity)
    {
      ResourceEntity.created_DateTimeOffset = null;      
      ResourceEntity.disposition_String = null;      
      ResourceEntity.organizationidentifier_Code = null;      
      ResourceEntity.organizationidentifier_System = null;      
      ResourceEntity.organizationreference_FhirId = null;      
      ResourceEntity.organizationreference_Type = null;      
      ResourceEntity.organizationreference_Url = null;      
      ResourceEntity.organizationreference_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.outcome_Code = null;      
      ResourceEntity.outcome_System = null;      
      ResourceEntity.requestidentifier_Code = null;      
      ResourceEntity.requestidentifier_System = null;      
      ResourceEntity.requestorganizationidentifier_Code = null;      
      ResourceEntity.requestorganizationidentifier_System = null;      
      ResourceEntity.requestorganizationreference_FhirId = null;      
      ResourceEntity.requestorganizationreference_Type = null;      
      ResourceEntity.requestorganizationreference_Url = null;      
      ResourceEntity.requestorganizationreference_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.requestprovideridentifier_Code = null;      
      ResourceEntity.requestprovideridentifier_System = null;      
      ResourceEntity.requestproviderreference_FhirId = null;      
      ResourceEntity.requestproviderreference_Type = null;      
      ResourceEntity.requestproviderreference_Url = null;      
      ResourceEntity.requestproviderreference_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.requestreference_FhirId = null;      
      ResourceEntity.requestreference_Type = null;      
      ResourceEntity.requestreference_Url = null;      
      ResourceEntity.requestreference_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_EligibilityResponse_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_EligibilityResponse_Index_profile.RemoveRange(ResourceEntity.profile_List);            
      _Context.Res_EligibilityResponse_Index_security.RemoveRange(ResourceEntity.security_List);            
      _Context.Res_EligibilityResponse_Index_tag.RemoveRange(ResourceEntity.tag_List);            
 
    }

    private void PopulateResourceEntity(Res_EligibilityResponse ResourseEntity, string ResourceVersion, EligibilityResponse ResourceTyped, IDtoFhirRequestUri FhirRequestUri)
    {
       IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Created != null)
      {
        if (ResourceTyped.CreatedElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.CreatedElement) as DateIndex;
          if (Index != null)
          {
            ResourseEntity.created_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Disposition != null)
      {
        if (ResourceTyped.DispositionElement is Hl7.Fhir.Model.FhirString)
        {
          var Index = new StringIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.DispositionElement) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.disposition_String = Index.String;
          }
        }
      }

      if (ResourceTyped.Organization != null)
      {
        if (ResourceTyped.Organization is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Organization) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.organizationidentifier_Code = Index.Code;
            ResourseEntity.organizationidentifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Organization != null)
      {
        if (ResourceTyped.Organization is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Organization, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.organizationreference_Type = Index.Type;
            ResourseEntity.organizationreference_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.organizationreference_Url = Index.Url;
            }
            else
            {
              ResourseEntity.organizationreference_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Outcome != null)
      {
        if (ResourceTyped.OutcomeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.RemittanceOutcome>)
        {
          var Index = new TokenIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.OutcomeElement) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.outcome_Code = Index.Code;
            ResourseEntity.outcome_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Request != null)
      {
        if (ResourceTyped.Request is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Request) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.requestidentifier_Code = Index.Code;
            ResourseEntity.requestidentifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.RequestOrganization != null)
      {
        if (ResourceTyped.RequestOrganization is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.RequestOrganization) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.requestorganizationidentifier_Code = Index.Code;
            ResourseEntity.requestorganizationidentifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.RequestOrganization != null)
      {
        if (ResourceTyped.RequestOrganization is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.RequestOrganization, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.requestorganizationreference_Type = Index.Type;
            ResourseEntity.requestorganizationreference_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.requestorganizationreference_Url = Index.Url;
            }
            else
            {
              ResourseEntity.requestorganizationreference_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.RequestProvider != null)
      {
        if (ResourceTyped.RequestProvider is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.RequestProvider) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.requestprovideridentifier_Code = Index.Code;
            ResourseEntity.requestprovideridentifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.RequestProvider != null)
      {
        if (ResourceTyped.RequestProvider is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.RequestProvider, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.requestproviderreference_Type = Index.Type;
            ResourseEntity.requestproviderreference_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.requestproviderreference_Url = Index.Url;
            }
            else
            {
              ResourseEntity.requestproviderreference_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Request != null)
      {
        if (ResourceTyped.Request is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Request, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.requestreference_Type = Index.Type;
            ResourseEntity.requestreference_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.requestreference_Url = Index.Url;
            }
            else
            {
              ResourseEntity.requestreference_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_EligibilityResponse_Index_identifier();
            Index = IndexSettingSupport.SetIndex(Index, item3) as Res_EligibilityResponse_Index_identifier;
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
              var Index = new Res_EligibilityResponse_Index_profile();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_EligibilityResponse_Index_profile;
              ResourseEntity.profile_List.Add(Index);
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
              var Index = new Res_EligibilityResponse_Index_security();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_EligibilityResponse_Index_security;
              ResourseEntity.security_List.Add(Index);
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
              var Index = new Res_EligibilityResponse_Index_tag();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_EligibilityResponse_Index_tag;
              ResourseEntity.tag_List.Add(Index);
            }
          }
        }
      }


      

    }


  }
} 
