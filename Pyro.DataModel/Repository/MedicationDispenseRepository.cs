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
  public partial class MedicationDispenseRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_MedicationDispense, new() 
    where ResourceHistoryType :Res_MedicationDispense_History, new()
  {
    public MedicationDispenseRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_MedicationDispense_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.prescription_List);
      IncludeList.Add(x => x.receiver_List);
      IncludeList.Add(x => x.responsibleparty_List);
      IncludeList.Add(x => x.type_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.destination_VersionId = null;      
      ResourceEntity.destination_FhirId = null;      
      ResourceEntity.destination_Type = null;      
      ResourceEntity.destination_Url = null;      
      ResourceEntity.destination_ServiceRootURL_StoreID = null;      
      ResourceEntity.dispenser_VersionId = null;      
      ResourceEntity.dispenser_FhirId = null;      
      ResourceEntity.dispenser_Type = null;      
      ResourceEntity.dispenser_Url = null;      
      ResourceEntity.dispenser_ServiceRootURL_StoreID = null;      
      ResourceEntity.identifier_Code = null;      
      ResourceEntity.identifier_System = null;      
      ResourceEntity.medication_VersionId = null;      
      ResourceEntity.medication_FhirId = null;      
      ResourceEntity.medication_Type = null;      
      ResourceEntity.medication_Url = null;      
      ResourceEntity.medication_ServiceRootURL_StoreID = null;      
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.whenhandedover_DateTimeOffset = null;      
      ResourceEntity.whenprepared_DateTimeOffset = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_MedicationDispense_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_MedicationDispense_Index_prescription.RemoveRange(ResourceEntity.prescription_List);            
      _Context.Res_MedicationDispense_Index_receiver.RemoveRange(ResourceEntity.receiver_List);            
      _Context.Res_MedicationDispense_Index_responsibleparty.RemoveRange(ResourceEntity.responsibleparty_List);            
      _Context.Res_MedicationDispense_Index_type.RemoveRange(ResourceEntity.type_List);            
      _Context.Res_MedicationDispense_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_MedicationDispense_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_MedicationDispense_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as MedicationDispense;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Destination != null)
      {
        if (ResourceTyped.Destination is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Destination, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.destination_Type = Index.Type;
            ResourseEntity.destination_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.destination_Url = Index.Url;
            }
            else
            {
              ResourseEntity.destination_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Dispenser != null)
      {
        if (ResourceTyped.Dispenser is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Dispenser, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.dispenser_Type = Index.Type;
            ResourseEntity.dispenser_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.dispenser_Url = Index.Url;
            }
            else
            {
              ResourseEntity.dispenser_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
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

      if (ResourceTyped.Medication != null)
      {
        if (ResourceTyped.Medication is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Medication, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.medication_Type = Index.Type;
            ResourseEntity.medication_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.medication_Url = Index.Url;
            }
            else
            {
              ResourseEntity.medication_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
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

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.MedicationDispense.MedicationDispenseStatus>)
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

      if (ResourceTyped.WhenHandedOver != null)
      {
        if (ResourceTyped.WhenHandedOverElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.WhenHandedOverElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.whenhandedover_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.WhenPrepared != null)
      {
        if (ResourceTyped.WhenPreparedElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.WhenPreparedElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.whenprepared_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Medication != null)
      {
        if (ResourceTyped.Medication is CodeableConcept)
        {
          CodeableConcept CodeableConcept = ResourceTyped.Medication as CodeableConcept;
          foreach (var item3 in CodeableConcept.Coding)
          {
            var Index = new Res_MedicationDispense_Index_code();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_MedicationDispense_Index_code;
            ResourseEntity.code_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.AuthorizingPrescription != null)
      {
        foreach (var item in ResourceTyped.AuthorizingPrescription)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_MedicationDispense_Index_prescription();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_MedicationDispense_Index_prescription;
            if (Index != null)
            {
              ResourseEntity.prescription_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Receiver != null)
      {
        foreach (var item in ResourceTyped.Receiver)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_MedicationDispense_Index_receiver();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_MedicationDispense_Index_receiver;
            if (Index != null)
            {
              ResourseEntity.receiver_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Substitution != null)
      {
        if (ResourceTyped.Substitution.ResponsibleParty != null)
        {
          foreach (var item in ResourceTyped.Substitution.ResponsibleParty)
          {
            if (item is ResourceReference)
            {
              var Index = new Res_MedicationDispense_Index_responsibleparty();
              Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_MedicationDispense_Index_responsibleparty;
              if (Index != null)
              {
                ResourseEntity.responsibleparty_List.Add(Index);
              }
            }
          }
        }
      }

      if (ResourceTyped.Type != null)
      {
        foreach (var item3 in ResourceTyped.Type.Coding)
        {
          var Index = new Res_MedicationDispense_Index_type();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_MedicationDispense_Index_type;
          ResourseEntity.type_List.Add(Index);
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
              var Index = new Res_MedicationDispense_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_MedicationDispense_Index__profile;
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
              var Index = new Res_MedicationDispense_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_MedicationDispense_Index__security;
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
              var Index = new Res_MedicationDispense_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_MedicationDispense_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

