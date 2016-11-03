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
  public partial class AppointmentRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Appointment, new() 
    where ResourceHistoryType :Res_Appointment_History, new()
  {
    public AppointmentRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Appointment_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.actor_List);
      IncludeList.Add(x => x.appointment_type_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.location_List);
      IncludeList.Add(x => x.part_status_List);
      IncludeList.Add(x => x.patient_List);
      IncludeList.Add(x => x.practitioner_List);
      IncludeList.Add(x => x.service_type_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.date_DateTimeOffset = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Appointment_Index_actor.RemoveRange(ResourceEntity.actor_List);            
      _Context.Res_Appointment_Index_appointment_type.RemoveRange(ResourceEntity.appointment_type_List);            
      _Context.Res_Appointment_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_Appointment_Index_location.RemoveRange(ResourceEntity.location_List);            
      _Context.Res_Appointment_Index_part_status.RemoveRange(ResourceEntity.part_status_List);            
      _Context.Res_Appointment_Index_patient.RemoveRange(ResourceEntity.patient_List);            
      _Context.Res_Appointment_Index_practitioner.RemoveRange(ResourceEntity.practitioner_List);            
      _Context.Res_Appointment_Index_service_type.RemoveRange(ResourceEntity.service_type_List);            
      _Context.Res_Appointment_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Appointment_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Appointment_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Appointment;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Start != null)
      {
        if (ResourceTyped.StartElement is Hl7.Fhir.Model.Instant)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.StartElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.date_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Appointment.AppointmentStatus>)
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

      foreach (var item1 in ResourceTyped.Participant)
      {
        if (item1.Actor != null)
        {
          if (item1.Actor is ResourceReference)
          {
            var Index = new Res_Appointment_Index_actor();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Actor, Index, FhirRequestUri, this) as Res_Appointment_Index_actor;
            if (Index != null)
            {
              ResourseEntity.actor_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.AppointmentType != null)
      {
        foreach (var item3 in ResourceTyped.AppointmentType.Coding)
        {
          var Index = new Res_Appointment_Index_appointment_type();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Appointment_Index_appointment_type;
          ResourseEntity.appointment_type_List.Add(Index);
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_Appointment_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Appointment_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Participant)
      {
        if (item1.Actor != null)
        {
          if (item1.Actor is ResourceReference)
          {
            var Index = new Res_Appointment_Index_location();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Actor, Index, FhirRequestUri, this) as Res_Appointment_Index_location;
            if (Index != null)
            {
              ResourseEntity.location_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Participant)
      {
        if (item1.Status != null)
        {
          if (item1.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ParticipationStatus>)
          {
            var Index = new Res_Appointment_Index_part_status();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.StatusElement, Index) as Res_Appointment_Index_part_status;
            ResourseEntity.part_status_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Participant)
      {
        if (item1.Actor != null)
        {
          if (item1.Actor is ResourceReference)
          {
            var Index = new Res_Appointment_Index_patient();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Actor, Index, FhirRequestUri, this) as Res_Appointment_Index_patient;
            if (Index != null)
            {
              ResourseEntity.patient_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Participant)
      {
        if (item1.Actor != null)
        {
          if (item1.Actor is ResourceReference)
          {
            var Index = new Res_Appointment_Index_practitioner();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Actor, Index, FhirRequestUri, this) as Res_Appointment_Index_practitioner;
            if (Index != null)
            {
              ResourseEntity.practitioner_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.ServiceType != null)
      {
        foreach (var item3 in ResourceTyped.ServiceType)
        {
          if (item3 != null)
          {
            foreach (var item4 in item3.Coding)
            {
              var Index = new Res_Appointment_Index_service_type();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Appointment_Index_service_type;
              ResourseEntity.service_type_List.Add(Index);
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
              var Index = new Res_Appointment_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Appointment_Index__profile;
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
              var Index = new Res_Appointment_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Appointment_Index__security;
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
              var Index = new Res_Appointment_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Appointment_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

