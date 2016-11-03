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
  public partial class ContractRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Contract, new() 
    where ResourceHistoryType :Res_Contract_History, new()
  {
    public ContractRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Contract_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.agent_List);
      IncludeList.Add(x => x.authority_List);
      IncludeList.Add(x => x.domain_List);
      IncludeList.Add(x => x.patient_List);
      IncludeList.Add(x => x.signer_List);
      IncludeList.Add(x => x.subject_List);
      IncludeList.Add(x => x.topic_List);
      IncludeList.Add(x => x.ttopic_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.identifier_Code = null;      
      ResourceEntity.identifier_System = null;      
      ResourceEntity.issued_DateTimeOffset = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Contract_Index_agent.RemoveRange(ResourceEntity.agent_List);            
      _Context.Res_Contract_Index_authority.RemoveRange(ResourceEntity.authority_List);            
      _Context.Res_Contract_Index_domain.RemoveRange(ResourceEntity.domain_List);            
      _Context.Res_Contract_Index_patient.RemoveRange(ResourceEntity.patient_List);            
      _Context.Res_Contract_Index_signer.RemoveRange(ResourceEntity.signer_List);            
      _Context.Res_Contract_Index_subject.RemoveRange(ResourceEntity.subject_List);            
      _Context.Res_Contract_Index_topic.RemoveRange(ResourceEntity.topic_List);            
      _Context.Res_Contract_Index_ttopic.RemoveRange(ResourceEntity.ttopic_List);            
      _Context.Res_Contract_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Contract_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Contract_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Contract;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

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

      if (ResourceTyped.Issued != null)
      {
        if (ResourceTyped.IssuedElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.IssuedElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.issued_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.Actor != null)
        {
          if (item1.Actor is ResourceReference)
          {
            var Index = new Res_Contract_Index_agent();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Actor, Index, FhirRequestUri, this) as Res_Contract_Index_agent;
            if (Index != null)
            {
              ResourseEntity.agent_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Authority != null)
      {
        foreach (var item in ResourceTyped.Authority)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Contract_Index_authority();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Contract_Index_authority;
            if (Index != null)
            {
              ResourseEntity.authority_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Domain != null)
      {
        foreach (var item in ResourceTyped.Domain)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Contract_Index_domain();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Contract_Index_domain;
            if (Index != null)
            {
              ResourseEntity.domain_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Subject != null)
      {
        foreach (var item in ResourceTyped.Subject)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Contract_Index_patient();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Contract_Index_patient;
            if (Index != null)
            {
              ResourseEntity.patient_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Signer)
      {
        if (item1.Party != null)
        {
          if (item1.Party is ResourceReference)
          {
            var Index = new Res_Contract_Index_signer();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Party, Index, FhirRequestUri, this) as Res_Contract_Index_signer;
            if (Index != null)
            {
              ResourseEntity.signer_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Subject != null)
      {
        foreach (var item in ResourceTyped.Subject)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Contract_Index_subject();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Contract_Index_subject;
            if (Index != null)
            {
              ResourseEntity.subject_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Topic != null)
      {
        foreach (var item in ResourceTyped.Topic)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Contract_Index_topic();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Contract_Index_topic;
            if (Index != null)
            {
              ResourseEntity.topic_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Term)
      {
        if (item1.Topic != null)
        {
          foreach (var item in item1.Topic)
          {
            if (item is ResourceReference)
            {
              var Index = new Res_Contract_Index_ttopic();
              Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Contract_Index_ttopic;
              if (Index != null)
              {
                ResourseEntity.ttopic_List.Add(Index);
              }
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
              var Index = new Res_Contract_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Contract_Index__profile;
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
              var Index = new Res_Contract_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Contract_Index__security;
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
              var Index = new Res_Contract_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Contract_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

