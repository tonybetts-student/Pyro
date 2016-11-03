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
  public partial class ProvenanceRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Provenance, new() 
    where ResourceHistoryType :Res_Provenance_History, new()
  {
    public ProvenanceRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Provenance_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.agent_List);
      IncludeList.Add(x => x.entity_List);
      IncludeList.Add(x => x.entity_type_List);
      IncludeList.Add(x => x.patient_List);
      IncludeList.Add(x => x.sig_List);
      IncludeList.Add(x => x.target_List);
      IncludeList.Add(x => x.userid_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.end_DateTimeOffset = null;      
      ResourceEntity.location_VersionId = null;      
      ResourceEntity.location_FhirId = null;      
      ResourceEntity.location_Type = null;      
      ResourceEntity.location_Url = null;      
      ResourceEntity.location_ServiceRootURL_StoreID = null;      
      ResourceEntity.start_DateTimeOffset = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Provenance_Index_agent.RemoveRange(ResourceEntity.agent_List);            
      _Context.Res_Provenance_Index_entity.RemoveRange(ResourceEntity.entity_List);            
      _Context.Res_Provenance_Index_entity_type.RemoveRange(ResourceEntity.entity_type_List);            
      _Context.Res_Provenance_Index_patient.RemoveRange(ResourceEntity.patient_List);            
      _Context.Res_Provenance_Index_sig.RemoveRange(ResourceEntity.sig_List);            
      _Context.Res_Provenance_Index_target.RemoveRange(ResourceEntity.target_List);            
      _Context.Res_Provenance_Index_userid.RemoveRange(ResourceEntity.userid_List);            
      _Context.Res_Provenance_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Provenance_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Provenance_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Provenance;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Period != null)
      {
        if (ResourceTyped.Period.End != null)
        {
          if (ResourceTyped.Period.EndElement is Hl7.Fhir.Model.FhirDateTime)
          {
            var Index = new DateTimeIndex();
            Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.Period.EndElement, Index) as DateTimeIndex;
            if (Index != null)
            {
              ResourseEntity.end_DateTimeOffset = Index.DateTimeOffset;
            }
          }
        }
      }

      if (ResourceTyped.Location != null)
      {
        if (ResourceTyped.Location is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Location, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.location_Type = Index.Type;
            ResourseEntity.location_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.location_Url = Index.Url;
            }
            else
            {
              ResourseEntity.location_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Period != null)
      {
        if (ResourceTyped.Period.Start != null)
        {
          if (ResourceTyped.Period.StartElement is Hl7.Fhir.Model.FhirDateTime)
          {
            var Index = new DateTimeIndex();
            Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.Period.StartElement, Index) as DateTimeIndex;
            if (Index != null)
            {
              ResourseEntity.start_DateTimeOffset = Index.DateTimeOffset;
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.Actor != null)
        {
          if (item1.Actor is ResourceReference)
          {
            var Index = new Res_Provenance_Index_agent();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Actor, Index, FhirRequestUri, this) as Res_Provenance_Index_agent;
            if (Index != null)
            {
              ResourseEntity.agent_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Entity)
      {
        if (item1.Reference != null)
        {
          if (item1.ReferenceElement is Hl7.Fhir.Model.FhirUri)
          {
            var Index = new Res_Provenance_Index_entity();
            Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item1.ReferenceElement, Index) as Res_Provenance_Index_entity;
            ResourseEntity.entity_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Entity)
      {
        if (item1.Type != null)
        {
          if (item1.Type is Hl7.Fhir.Model.Coding)
          {
            var Index = new Res_Provenance_Index_entity_type();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.Type, Index) as Res_Provenance_Index_entity_type;
            ResourseEntity.entity_type_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Target != null)
      {
        foreach (var item in ResourceTyped.Target)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Provenance_Index_patient();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Provenance_Index_patient;
            if (Index != null)
            {
              ResourseEntity.patient_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Signature)
      {
        if (item1.Type != null)
        {
          foreach (var item4 in item1.Type)
          {
            if (item4 is Hl7.Fhir.Model.Coding)
            {
              var Index = new Res_Provenance_Index_sig();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Provenance_Index_sig;
              ResourseEntity.sig_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Target != null)
      {
        foreach (var item in ResourceTyped.Target)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Provenance_Index_target();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Provenance_Index_target;
            if (Index != null)
            {
              ResourseEntity.target_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.UserId != null)
        {
          if (item1.UserId is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_Provenance_Index_userid();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.UserId, Index) as Res_Provenance_Index_userid;
            ResourseEntity.userid_List.Add(Index);
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
              var Index = new Res_Provenance_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Provenance_Index__profile;
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
              var Index = new Res_Provenance_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Provenance_Index__security;
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
              var Index = new Res_Provenance_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Provenance_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

