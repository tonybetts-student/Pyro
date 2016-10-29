﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pyro.DataModel.DatabaseModel;
using Pyro.DataModel.DatabaseModel.Base;
using Pyro.DataModel.Support;
using Pyro.DataModel.IndexSetter;
using Pyro.DataModel.Search;
using Hl7.Fhir.Model;
using Pyro.Common.BusinessEntities.Search;
using Pyro.Common.Interfaces;
using Pyro.Common.Interfaces.Repositories;
using Pyro.Common.Interfaces.UriSupport;
using Hl7.Fhir.Introspection;

namespace Pyro.DataModel.Repository
{
  public partial class AuditEventRepository : CommonRepository, IResourceRepository
  {
    public AuditEventRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    public IDatabaseOperationOutcome GetResourceBySearch(DtoSearchParameters DtoSearchParameters)
    {
      var Predicate = PredicateGenerator<Res_AuditEvent>(DtoSearchParameters);
      int TotalRecordCount = DbGetALLCount<Res_AuditEvent>(Predicate);
      var Query = DbGetAll<Res_AuditEvent>(Predicate);

      //Todo: Sort not implemented just defaulting to last update order
      Query = Query.OrderBy(x => x.lastUpdated);      
      int ClaculatedPageRequired = PaginationSupport.CalculatePageRequired(DtoSearchParameters.RequiredPageNumber, _NumberOfRecordsPerPage, TotalRecordCount);
      
      Query = Query.Paging(ClaculatedPageRequired, _NumberOfRecordsPerPage);
      var DtoResourceList = new List<Common.BusinessEntities.Dto.DtoResource>();
      Query.ToList().ForEach(x => DtoResourceList.Add(IndexSettingSupport.SetDtoResource(x)));

      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = false;
      DatabaseOperationOutcome.PagesTotal = PaginationSupport.CalculateTotalPages(_NumberOfRecordsPerPage, TotalRecordCount); ;
      DatabaseOperationOutcome.PageRequested = ClaculatedPageRequired;
      DatabaseOperationOutcome.ReturnedResourceCount = TotalRecordCount;
      DatabaseOperationOutcome.ReturnedResourceList = DtoResourceList;


      return DatabaseOperationOutcome;  
    }

    public IDatabaseOperationOutcome AddResource(Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as AuditEvent;
      var ResourceEntity = new Res_AuditEvent();
      this.PopulateResourceEntity(ResourceEntity, "1", ResourceTyped, FhirRequestUri);
      this.DbAddEntity<Res_AuditEvent>(ResourceEntity);
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;     
      DatabaseOperationOutcome.ReturnedResource = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ReturnedResourceCount = 1;
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome UpdateResource(string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as AuditEvent;
      var ResourceEntity = LoadCurrentResourceEntity(Resource.Id);
      var ResourceHistoryEntity = new Res_AuditEvent_History();  
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_AuditEvent_History_List.Add(ResourceHistoryEntity); 
      this.ResetResourceEntity(ResourceEntity);
      this.PopulateResourceEntity(ResourceEntity, ResourceVersion, ResourceTyped, FhirRequestUri);            
      this.Save();            
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      DatabaseOperationOutcome.ReturnedResource = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ReturnedResourceCount = 1;
      return DatabaseOperationOutcome;
    }

    public void UpdateResouceAsDeleted(string FhirResourceId, string ResourceVersion)
    {
      var ResourceEntity = this.LoadCurrentResourceEntity(FhirResourceId);
      var ResourceHistoryEntity = new Res_AuditEvent_History();
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_AuditEvent_History_List.Add(ResourceHistoryEntity);
      this.ResetResourceEntity(ResourceEntity);
      ResourceEntity.IsDeleted = true;
      ResourceEntity.versionId = ResourceVersion;
      ResourceEntity.XmlBlob = string.Empty;
      this.Save();      
    }

    public IDatabaseOperationOutcome GetResourceByFhirIDAndVersionNumber(string FhirResourceId, string ResourceVersionNumber)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      var ResourceHistoryEntity = DbGet<Res_AuditEvent_History>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
      if (ResourceHistoryEntity != null)
      {
        DatabaseOperationOutcome.ReturnedResource = IndexSettingSupport.SetDtoResource(ResourceHistoryEntity);
      }
      else
      {
        var ResourceEntity = DbGet<Res_AuditEvent>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
        if (ResourceEntity != null)
          DatabaseOperationOutcome.ReturnedResource = IndexSettingSupport.SetDtoResource(ResourceEntity);        
      }
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome GetResourceByFhirID(string FhirResourceId, bool WithXml = false)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      Pyro.Common.BusinessEntities.Dto.DtoResource DtoResource = null;
      if (WithXml)
      {        
        DtoResource = DbGetAll<Res_AuditEvent>(x => x.FhirId == FhirResourceId).Select(x => new Pyro.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated, Xml = x.XmlBlob }).SingleOrDefault();       
      }
      else
      {
        DtoResource = DbGetAll<Res_AuditEvent>(x => x.FhirId == FhirResourceId).Select(x => new Pyro.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated }).SingleOrDefault();        
      }
      DatabaseOperationOutcome.ReturnedResource = DtoResource;
      return DatabaseOperationOutcome;
    }

    private Res_AuditEvent LoadCurrentResourceEntity(string FhirId)
    {

      var IncludeList = new List<Expression<Func<Res_AuditEvent, object>>>();
      IncludeList.Add(x => x.address_List);
      IncludeList.Add(x => x.agent_List);
      IncludeList.Add(x => x.agent_name_List);
      IncludeList.Add(x => x.altid_List);
      IncludeList.Add(x => x.entity_List);
      IncludeList.Add(x => x.entity_id_List);
      IncludeList.Add(x => x.entity_name_List);
      IncludeList.Add(x => x.entity_type_List);
      IncludeList.Add(x => x.patient_List);
      IncludeList.Add(x => x.patient_List);
      IncludeList.Add(x => x.policy_List);
      IncludeList.Add(x => x.role_List);
      IncludeList.Add(x => x.subtype_List);
      IncludeList.Add(x => x.user_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<Res_AuditEvent>(x => x.FhirId == FhirId, IncludeList);

      return ResourceEntity;
    }


    private void ResetResourceEntity(Res_AuditEvent ResourceEntity)
    {
      ResourceEntity.action_Code = null;      
      ResourceEntity.action_System = null;      
      ResourceEntity.date_DateTimeOffset = null;      
      ResourceEntity.outcome_Code = null;      
      ResourceEntity.outcome_System = null;      
      ResourceEntity.site_Code = null;      
      ResourceEntity.site_System = null;      
      ResourceEntity.source_Code = null;      
      ResourceEntity.source_System = null;      
      ResourceEntity.type_Code = null;      
      ResourceEntity.type_System = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_AuditEvent_Index_address.RemoveRange(ResourceEntity.address_List);            
      _Context.Res_AuditEvent_Index_agent.RemoveRange(ResourceEntity.agent_List);            
      _Context.Res_AuditEvent_Index_agent_name.RemoveRange(ResourceEntity.agent_name_List);            
      _Context.Res_AuditEvent_Index_altid.RemoveRange(ResourceEntity.altid_List);            
      _Context.Res_AuditEvent_Index_entity.RemoveRange(ResourceEntity.entity_List);            
      _Context.Res_AuditEvent_Index_entity_id.RemoveRange(ResourceEntity.entity_id_List);            
      _Context.Res_AuditEvent_Index_entity_name.RemoveRange(ResourceEntity.entity_name_List);            
      _Context.Res_AuditEvent_Index_entity_type.RemoveRange(ResourceEntity.entity_type_List);            
      _Context.Res_AuditEvent_Index_patient.RemoveRange(ResourceEntity.patient_List);            
      _Context.Res_AuditEvent_Index_patient.RemoveRange(ResourceEntity.patient_List);            
      _Context.Res_AuditEvent_Index_policy.RemoveRange(ResourceEntity.policy_List);            
      _Context.Res_AuditEvent_Index_role.RemoveRange(ResourceEntity.role_List);            
      _Context.Res_AuditEvent_Index_subtype.RemoveRange(ResourceEntity.subtype_List);            
      _Context.Res_AuditEvent_Index_user.RemoveRange(ResourceEntity.user_List);            
      _Context.Res_AuditEvent_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_AuditEvent_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_AuditEvent_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    private void PopulateResourceEntity(Res_AuditEvent ResourseEntity, string ResourceVersion, AuditEvent ResourceTyped, IDtoFhirRequestUri FhirRequestUri)
    {
       IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Action != null)
      {
        if (ResourceTyped.ActionElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AuditEvent.AuditEventAction>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.ActionElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.action_Code = Index.Code;
            ResourseEntity.action_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Recorded != null)
      {
        if (ResourceTyped.RecordedElement is Hl7.Fhir.Model.Instant)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.RecordedElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.date_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Outcome != null)
      {
        if (ResourceTyped.OutcomeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AuditEvent.AuditEventOutcome>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.OutcomeElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.outcome_Code = Index.Code;
            ResourseEntity.outcome_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Source != null)
      {
        if (ResourceTyped.Source.Site != null)
        {
          if (ResourceTyped.Source.SiteElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new TokenIndex();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Source.SiteElement, Index) as TokenIndex;
            if (Index != null)
            {
              ResourseEntity.site_Code = Index.Code;
              ResourseEntity.site_System = Index.System;
            }
          }
        }
      }

      if (ResourceTyped.Source != null)
      {
        if (ResourceTyped.Source.Identifier != null)
        {
          if (ResourceTyped.Source.Identifier is Hl7.Fhir.Model.Identifier)
          {
            var Index = new TokenIndex();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Source.Identifier, Index) as TokenIndex;
            if (Index != null)
            {
              ResourseEntity.source_Code = Index.Code;
              ResourseEntity.source_System = Index.System;
            }
          }
        }
      }

      if (ResourceTyped.Type != null)
      {
        if (ResourceTyped.Type is Hl7.Fhir.Model.Coding)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Type, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.type_Code = Index.Code;
            ResourseEntity.type_System = Index.System;
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.Network != null)
        {
          if (item1.Network.Address != null)
          {
            if (item1.Network.AddressElement is Hl7.Fhir.Model.FhirString)
            {
              var Index = new Res_AuditEvent_Index_address();
              Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.Network.AddressElement, Index) as Res_AuditEvent_Index_address;
              ResourseEntity.address_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.Reference != null)
        {
          if (item1.Reference is ResourceReference)
          {
            var Index = new Res_AuditEvent_Index_agent();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Reference, Index, FhirRequestUri, this) as Res_AuditEvent_Index_agent;
            if (Index != null)
            {
              ResourseEntity.agent_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.Name != null)
        {
          if (item1.NameElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_AuditEvent_Index_agent_name();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.NameElement, Index) as Res_AuditEvent_Index_agent_name;
            ResourseEntity.agent_name_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.AltId != null)
        {
          if (item1.AltIdElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_AuditEvent_Index_altid();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.AltIdElement, Index) as Res_AuditEvent_Index_altid;
            ResourseEntity.altid_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Entity)
      {
        if (item1.Reference != null)
        {
          if (item1.Reference is ResourceReference)
          {
            var Index = new Res_AuditEvent_Index_entity();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Reference, Index, FhirRequestUri, this) as Res_AuditEvent_Index_entity;
            if (Index != null)
            {
              ResourseEntity.entity_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Entity)
      {
        if (item1.Identifier != null)
        {
          if (item1.Identifier is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_AuditEvent_Index_entity_id();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.Identifier, Index) as Res_AuditEvent_Index_entity_id;
            ResourseEntity.entity_id_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Entity)
      {
        if (item1.Name != null)
        {
          if (item1.NameElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_AuditEvent_Index_entity_name();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.NameElement, Index) as Res_AuditEvent_Index_entity_name;
            ResourseEntity.entity_name_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Entity)
      {
        if (item1.Type != null)
        {
          if (item1.Type is Hl7.Fhir.Model.Coding)
          {
            var Index = new Res_AuditEvent_Index_entity_type();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.Type, Index) as Res_AuditEvent_Index_entity_type;
            ResourseEntity.entity_type_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.Reference != null)
        {
          if (item1.Reference is ResourceReference)
          {
            var Index = new Res_AuditEvent_Index_patient();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Reference, Index, FhirRequestUri, this) as Res_AuditEvent_Index_patient;
            if (Index != null)
            {
              ResourseEntity.patient_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Entity)
      {
        if (item1.Reference != null)
        {
          if (item1.Reference is ResourceReference)
          {
            var Index = new Res_AuditEvent_Index_patient();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Reference, Index, FhirRequestUri, this) as Res_AuditEvent_Index_patient;
            if (Index != null)
            {
              ResourseEntity.patient_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.Policy != null)
        {
          foreach (var item4 in item1.PolicyElement)
          {
            if (item4 is Hl7.Fhir.Model.FhirUri)
            {
              var Index = new Res_AuditEvent_Index_policy();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_AuditEvent_Index_policy;
              ResourseEntity.policy_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Entity)
      {
        if (item1.Role != null)
        {
          if (item1.Role is Hl7.Fhir.Model.Coding)
          {
            var Index = new Res_AuditEvent_Index_role();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.Role, Index) as Res_AuditEvent_Index_role;
            ResourseEntity.role_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Subtype != null)
      {
        foreach (var item3 in ResourceTyped.Subtype)
        {
          if (item3 is Hl7.Fhir.Model.Coding)
          {
            var Index = new Res_AuditEvent_Index_subtype();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_AuditEvent_Index_subtype;
            ResourseEntity.subtype_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Agent)
      {
        if (item1.UserId != null)
        {
          if (item1.UserId is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_AuditEvent_Index_user();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.UserId, Index) as Res_AuditEvent_Index_user;
            ResourseEntity.user_List.Add(Index);
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
              var Index = new Res_AuditEvent_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_AuditEvent_Index__profile;
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
              var Index = new Res_AuditEvent_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_AuditEvent_Index__security;
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
              var Index = new Res_AuditEvent_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_AuditEvent_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      

    }


  }
} 
