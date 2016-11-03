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
  public partial class AllergyIntoleranceRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_AllergyIntolerance, new() 
    where ResourceHistoryType :Res_AllergyIntolerance_History, new()
  {
    public AllergyIntoleranceRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_AllergyIntolerance_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.manifestation_List);
      IncludeList.Add(x => x.onset_List);
      IncludeList.Add(x => x.route_List);
      IncludeList.Add(x => x.severity_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.category_Code = null;      
      ResourceEntity.category_System = null;      
      ResourceEntity.criticality_Code = null;      
      ResourceEntity.criticality_System = null;      
      ResourceEntity.date_DateTimeOffset = null;      
      ResourceEntity.last_date_DateTimeOffset = null;      
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.recorder_VersionId = null;      
      ResourceEntity.recorder_FhirId = null;      
      ResourceEntity.recorder_Type = null;      
      ResourceEntity.recorder_Url = null;      
      ResourceEntity.recorder_ServiceRootURL_StoreID = null;      
      ResourceEntity.reporter_VersionId = null;      
      ResourceEntity.reporter_FhirId = null;      
      ResourceEntity.reporter_Type = null;      
      ResourceEntity.reporter_Url = null;      
      ResourceEntity.reporter_ServiceRootURL_StoreID = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.type_Code = null;      
      ResourceEntity.type_System = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_AllergyIntolerance_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_AllergyIntolerance_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_AllergyIntolerance_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_AllergyIntolerance_Index_manifestation.RemoveRange(ResourceEntity.manifestation_List);            
      _Context.Res_AllergyIntolerance_Index_onset.RemoveRange(ResourceEntity.onset_List);            
      _Context.Res_AllergyIntolerance_Index_route.RemoveRange(ResourceEntity.route_List);            
      _Context.Res_AllergyIntolerance_Index_severity.RemoveRange(ResourceEntity.severity_List);            
      _Context.Res_AllergyIntolerance_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_AllergyIntolerance_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_AllergyIntolerance_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as AllergyIntolerance;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Category != null)
      {
        if (ResourceTyped.CategoryElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCategory>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.CategoryElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.category_Code = Index.Code;
            ResourseEntity.category_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Criticality != null)
      {
        if (ResourceTyped.CriticalityElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceCriticality>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.CriticalityElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.criticality_Code = Index.Code;
            ResourseEntity.criticality_System = Index.System;
          }
        }
      }

      if (ResourceTyped.AttestedDate != null)
      {
        if (ResourceTyped.AttestedDateElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.AttestedDateElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.date_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.LastOccurrence != null)
      {
        if (ResourceTyped.LastOccurrenceElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.LastOccurrenceElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.last_date_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Patient != null)
      {
        if (ResourceTyped.Patient is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Patient, Index, FhirRequestUri, this) as ReferenceIndex;
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

      if (ResourceTyped.Recorder != null)
      {
        if (ResourceTyped.Recorder is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Recorder, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.recorder_Type = Index.Type;
            ResourseEntity.recorder_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.recorder_Url = Index.Url;
            }
            else
            {
              ResourseEntity.recorder_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Reporter != null)
      {
        if (ResourceTyped.Reporter is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Reporter, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.reporter_Type = Index.Type;
            ResourseEntity.reporter_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.reporter_Url = Index.Url;
            }
            else
            {
              ResourseEntity.reporter_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceStatus>)
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

      if (ResourceTyped.Type != null)
      {
        if (ResourceTyped.TypeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceType>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.TypeElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.type_Code = Index.Code;
            ResourseEntity.type_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Code != null)
      {
        foreach (var item3 in ResourceTyped.Code.Coding)
        {
          var Index = new Res_AllergyIntolerance_Index_code();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_AllergyIntolerance_Index_code;
          ResourseEntity.code_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Reaction)
      {
        if (item1.Substance != null)
        {
          foreach (var item4 in item1.Substance.Coding)
          {
            var Index = new Res_AllergyIntolerance_Index_code();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_AllergyIntolerance_Index_code;
            ResourseEntity.code_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_AllergyIntolerance_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_AllergyIntolerance_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Reaction)
      {
        if (item1.Manifestation != null)
        {
          foreach (var item4 in item1.Manifestation)
          {
            if (item4 != null)
            {
              foreach (var item5 in item4.Coding)
              {
                var Index = new Res_AllergyIntolerance_Index_manifestation();
                Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item5, Index) as Res_AllergyIntolerance_Index_manifestation;
                ResourseEntity.manifestation_List.Add(Index);
              }
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Reaction)
      {
        if (item1.Onset != null)
        {
          if (item1.OnsetElement is Hl7.Fhir.Model.FhirDateTime)
          {
            var Index = new Res_AllergyIntolerance_Index_onset();
            Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(item1.OnsetElement, Index) as Res_AllergyIntolerance_Index_onset;
            ResourseEntity.onset_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Reaction)
      {
        if (item1.ExposureRoute != null)
        {
          foreach (var item4 in item1.ExposureRoute.Coding)
          {
            var Index = new Res_AllergyIntolerance_Index_route();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_AllergyIntolerance_Index_route;
            ResourseEntity.route_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Reaction)
      {
        if (item1.Severity != null)
        {
          if (item1.SeverityElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AllergyIntolerance.AllergyIntoleranceSeverity>)
          {
            var Index = new Res_AllergyIntolerance_Index_severity();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.SeverityElement, Index) as Res_AllergyIntolerance_Index_severity;
            ResourseEntity.severity_List.Add(Index);
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
              var Index = new Res_AllergyIntolerance_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_AllergyIntolerance_Index__profile;
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
              var Index = new Res_AllergyIntolerance_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_AllergyIntolerance_Index__security;
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
              var Index = new Res_AllergyIntolerance_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_AllergyIntolerance_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

