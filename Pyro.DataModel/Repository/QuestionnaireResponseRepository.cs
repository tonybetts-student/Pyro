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
  public partial class QuestionnaireResponseRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_QuestionnaireResponse, new() 
    where ResourceHistoryType :Res_QuestionnaireResponse_History, new()
  {
    public QuestionnaireResponseRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_QuestionnaireResponse_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.based_on_List);
      IncludeList.Add(x => x.parent_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.author_VersionId = null;      
      ResourceEntity.author_FhirId = null;      
      ResourceEntity.author_Type = null;      
      ResourceEntity.author_Url = null;      
      ResourceEntity.author_ServiceRootURL_StoreID = null;      
      ResourceEntity.authored_DateTimeOffset = null;      
      ResourceEntity.context_VersionId = null;      
      ResourceEntity.context_FhirId = null;      
      ResourceEntity.context_Type = null;      
      ResourceEntity.context_Url = null;      
      ResourceEntity.context_ServiceRootURL_StoreID = null;      
      ResourceEntity.identifier_Code = null;      
      ResourceEntity.identifier_System = null;      
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.questionnaire_VersionId = null;      
      ResourceEntity.questionnaire_FhirId = null;      
      ResourceEntity.questionnaire_Type = null;      
      ResourceEntity.questionnaire_Url = null;      
      ResourceEntity.questionnaire_ServiceRootURL_StoreID = null;      
      ResourceEntity.source_VersionId = null;      
      ResourceEntity.source_FhirId = null;      
      ResourceEntity.source_Type = null;      
      ResourceEntity.source_Url = null;      
      ResourceEntity.source_ServiceRootURL_StoreID = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.subject_VersionId = null;      
      ResourceEntity.subject_FhirId = null;      
      ResourceEntity.subject_Type = null;      
      ResourceEntity.subject_Url = null;      
      ResourceEntity.subject_ServiceRootURL_StoreID = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_QuestionnaireResponse_Index_based_on.RemoveRange(ResourceEntity.based_on_List);            
      _Context.Res_QuestionnaireResponse_Index_parent.RemoveRange(ResourceEntity.parent_List);            
      _Context.Res_QuestionnaireResponse_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_QuestionnaireResponse_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_QuestionnaireResponse_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as QuestionnaireResponse;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Author != null)
      {
        if (ResourceTyped.Author is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Author, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.author_Type = Index.Type;
            ResourseEntity.author_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.author_Url = Index.Url;
            }
            else
            {
              ResourseEntity.author_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Authored != null)
      {
        if (ResourceTyped.AuthoredElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.AuthoredElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.authored_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Context != null)
      {
        if (ResourceTyped.Context is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Context, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.context_Type = Index.Type;
            ResourseEntity.context_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.context_Url = Index.Url;
            }
            else
            {
              ResourseEntity.context_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        if (ResourceTyped.Identifier is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Identifier, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.identifier_Code = Index.Code;
            ResourseEntity.identifier_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Subject != null)
      {
        if (ResourceTyped.Subject is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Subject, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.patient_Type = Index.Type;
            ResourseEntity.patient_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.patient_Url = Index.Url;
            }
            else
            {
              ResourseEntity.patient_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Questionnaire != null)
      {
        if (ResourceTyped.Questionnaire is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Questionnaire, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.questionnaire_Type = Index.Type;
            ResourseEntity.questionnaire_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.questionnaire_Url = Index.Url;
            }
            else
            {
              ResourseEntity.questionnaire_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Source != null)
      {
        if (ResourceTyped.Source is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Source, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.source_Type = Index.Type;
            ResourseEntity.source_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.source_Url = Index.Url;
            }
            else
            {
              ResourseEntity.source_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.QuestionnaireResponse.QuestionnaireResponseStatus>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.StatusElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.status_Code = Index.Code;
            ResourseEntity.status_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Subject != null)
      {
        if (ResourceTyped.Subject is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Subject, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.subject_Type = Index.Type;
            ResourseEntity.subject_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.subject_Url = Index.Url;
            }
            else
            {
              ResourseEntity.subject_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.BasedOn != null)
      {
        foreach (var item in ResourceTyped.BasedOn)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_QuestionnaireResponse_Index_based_on();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_QuestionnaireResponse_Index_based_on;
            if (Index != null)
            {
              ResourseEntity.based_on_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Parent != null)
      {
        foreach (var item in ResourceTyped.Parent)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_QuestionnaireResponse_Index_parent();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_QuestionnaireResponse_Index_parent;
            if (Index != null)
            {
              ResourseEntity.parent_List.Add(Index);
            }
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
              var Index = new Res_QuestionnaireResponse_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_QuestionnaireResponse_Index__profile;
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
              var Index = new Res_QuestionnaireResponse_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_QuestionnaireResponse_Index__security;
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
              var Index = new Res_QuestionnaireResponse_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_QuestionnaireResponse_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

