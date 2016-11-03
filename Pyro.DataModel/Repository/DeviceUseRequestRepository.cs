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
  public partial class DeviceUseRequestRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_DeviceUseRequest, new() 
    where ResourceHistoryType :Res_DeviceUseRequest_History, new()
  {
    public DeviceUseRequestRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_DeviceUseRequest_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.based_on_List);
      IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.definition_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.replaces_List);
      IncludeList.Add(x => x.stage_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.author_date_DateTimeOffset = null;      
      ResourceEntity.device_VersionId = null;      
      ResourceEntity.device_FhirId = null;      
      ResourceEntity.device_Type = null;      
      ResourceEntity.device_Url = null;      
      ResourceEntity.device_ServiceRootURL_StoreID = null;      
      ResourceEntity.encounter_VersionId = null;      
      ResourceEntity.encounter_FhirId = null;      
      ResourceEntity.encounter_Type = null;      
      ResourceEntity.encounter_Url = null;      
      ResourceEntity.encounter_ServiceRootURL_StoreID = null;      
      ResourceEntity.event_date_DateTimeOffsetLow = null;      
      ResourceEntity.event_date_DateTimeOffsetHigh = null;      
      ResourceEntity.event_date_DateTimeOffsetLow = null;      
      ResourceEntity.event_date_DateTimeOffsetHigh = null;      
      ResourceEntity.filler_VersionId = null;      
      ResourceEntity.filler_FhirId = null;      
      ResourceEntity.filler_Type = null;      
      ResourceEntity.filler_Url = null;      
      ResourceEntity.filler_ServiceRootURL_StoreID = null;      
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.requester_VersionId = null;      
      ResourceEntity.requester_FhirId = null;      
      ResourceEntity.requester_Type = null;      
      ResourceEntity.requester_Url = null;      
      ResourceEntity.requester_ServiceRootURL_StoreID = null;      
      ResourceEntity.requisition_Code = null;      
      ResourceEntity.requisition_System = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.subject_VersionId = null;      
      ResourceEntity.subject_FhirId = null;      
      ResourceEntity.subject_Type = null;      
      ResourceEntity.subject_Url = null;      
      ResourceEntity.subject_ServiceRootURL_StoreID = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_DeviceUseRequest_Index_based_on.RemoveRange(ResourceEntity.based_on_List);            
      _Context.Res_DeviceUseRequest_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_DeviceUseRequest_Index_definition.RemoveRange(ResourceEntity.definition_List);            
      _Context.Res_DeviceUseRequest_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_DeviceUseRequest_Index_replaces.RemoveRange(ResourceEntity.replaces_List);            
      _Context.Res_DeviceUseRequest_Index_stage.RemoveRange(ResourceEntity.stage_List);            
      _Context.Res_DeviceUseRequest_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_DeviceUseRequest_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_DeviceUseRequest_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as DeviceUseRequest;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Authored != null)
      {
        if (ResourceTyped.AuthoredElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.AuthoredElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.author_date_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Device != null)
      {
        if (ResourceTyped.Device is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Device, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.device_Type = Index.Type;
            ResourseEntity.device_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.device_Url = Index.Url;
            }
            else
            {
              ResourseEntity.device_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
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
            ResourseEntity.encounter_Type = Index.Type;
            ResourseEntity.encounter_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.encounter_Url = Index.Url;
            }
            else
            {
              ResourseEntity.encounter_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Occurrence != null)
      {
        if (ResourceTyped.Occurrence is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimePeriodIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimePeriodIndex)).Set(ResourceTyped.Occurrence, Index) as DateTimePeriodIndex;
          if (Index != null)
          {
            ResourseEntity.event_date_DateTimeOffsetLow = Index.DateTimeOffsetLow;
            ResourseEntity.event_date_DateTimeOffsetHigh = Index.DateTimeOffsetHigh;
          }
        }
      }

      if (ResourceTyped.Occurrence != null)
      {
        if (ResourceTyped.Occurrence is Hl7.Fhir.Model.Period)
        {
          var Index = new DateTimePeriodIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimePeriodIndex)).Set(ResourceTyped.Occurrence, Index) as DateTimePeriodIndex;
          if (Index != null)
          {
            ResourseEntity.event_date_DateTimeOffsetLow = Index.DateTimeOffsetLow;
            ResourseEntity.event_date_DateTimeOffsetHigh = Index.DateTimeOffsetHigh;
          }
        }
      }

      if (ResourceTyped.Performer != null)
      {
        if (ResourceTyped.Performer is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Performer, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.filler_Type = Index.Type;
            ResourseEntity.filler_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.filler_Url = Index.Url;
            }
            else
            {
              ResourseEntity.filler_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
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

      if (ResourceTyped.Requester != null)
      {
        if (ResourceTyped.Requester is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Requester, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.requester_Type = Index.Type;
            ResourseEntity.requester_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.requester_Url = Index.Url;
            }
            else
            {
              ResourseEntity.requester_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Requisition != null)
      {
        if (ResourceTyped.Requisition is Hl7.Fhir.Model.Identifier)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Requisition, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.requisition_Code = Index.Code;
            ResourseEntity.requisition_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.RequestStatus>)
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
            var Index = new Res_DeviceUseRequest_Index_based_on();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_DeviceUseRequest_Index_based_on;
            if (Index != null)
            {
              ResourseEntity.based_on_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Device != null)
      {
        if (ResourceTyped.Device is CodeableConcept)
        {
          CodeableConcept CodeableConcept = ResourceTyped.Device as CodeableConcept;
          foreach (var item3 in CodeableConcept.Coding)
          {
            var Index = new Res_DeviceUseRequest_Index_code();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_DeviceUseRequest_Index_code;
            ResourseEntity.code_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Definition != null)
      {
        foreach (var item in ResourceTyped.Definition)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_DeviceUseRequest_Index_definition();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_DeviceUseRequest_Index_definition;
            if (Index != null)
            {
              ResourseEntity.definition_List.Add(Index);
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
            var Index = new Res_DeviceUseRequest_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_DeviceUseRequest_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Replaces != null)
      {
        foreach (var item in ResourceTyped.Replaces)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_DeviceUseRequest_Index_replaces();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_DeviceUseRequest_Index_replaces;
            if (Index != null)
            {
              ResourseEntity.replaces_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Stage != null)
      {
        foreach (var item3 in ResourceTyped.Stage.Coding)
        {
          var Index = new Res_DeviceUseRequest_Index_stage();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_DeviceUseRequest_Index_stage;
          ResourseEntity.stage_List.Add(Index);
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
              var Index = new Res_DeviceUseRequest_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_DeviceUseRequest_Index__profile;
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
              var Index = new Res_DeviceUseRequest_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_DeviceUseRequest_Index__security;
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
              var Index = new Res_DeviceUseRequest_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_DeviceUseRequest_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

