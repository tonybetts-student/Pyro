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
  public partial class RelatedPersonRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_RelatedPerson, new() 
    where ResourceHistoryType :Res_RelatedPerson_History, new()
  {
    public RelatedPersonRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_RelatedPerson_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.address_List);
      IncludeList.Add(x => x.address_city_List);
      IncludeList.Add(x => x.address_country_List);
      IncludeList.Add(x => x.address_postalcode_List);
      IncludeList.Add(x => x.address_state_List);
      IncludeList.Add(x => x.address_use_List);
      IncludeList.Add(x => x.email_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.name_List);
      IncludeList.Add(x => x.phone_List);
      IncludeList.Add(x => x.phonetic_List);
      IncludeList.Add(x => x.telecom_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.active_Code = null;      
      ResourceEntity.active_System = null;      
      ResourceEntity.birthdate_Date = null;      
      ResourceEntity.gender_Code = null;      
      ResourceEntity.gender_System = null;      
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_RelatedPerson_Index_address.RemoveRange(ResourceEntity.address_List);            
      _Context.Res_RelatedPerson_Index_address_city.RemoveRange(ResourceEntity.address_city_List);            
      _Context.Res_RelatedPerson_Index_address_country.RemoveRange(ResourceEntity.address_country_List);            
      _Context.Res_RelatedPerson_Index_address_postalcode.RemoveRange(ResourceEntity.address_postalcode_List);            
      _Context.Res_RelatedPerson_Index_address_state.RemoveRange(ResourceEntity.address_state_List);            
      _Context.Res_RelatedPerson_Index_address_use.RemoveRange(ResourceEntity.address_use_List);            
      _Context.Res_RelatedPerson_Index_email.RemoveRange(ResourceEntity.email_List);            
      _Context.Res_RelatedPerson_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_RelatedPerson_Index_name.RemoveRange(ResourceEntity.name_List);            
      _Context.Res_RelatedPerson_Index_phone.RemoveRange(ResourceEntity.phone_List);            
      _Context.Res_RelatedPerson_Index_phonetic.RemoveRange(ResourceEntity.phonetic_List);            
      _Context.Res_RelatedPerson_Index_telecom.RemoveRange(ResourceEntity.telecom_List);            
      _Context.Res_RelatedPerson_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_RelatedPerson_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_RelatedPerson_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as RelatedPerson;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Active != null)
      {
        if (ResourceTyped.ActiveElement is Hl7.Fhir.Model.FhirBoolean)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.ActiveElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.active_Code = Index.Code;
            ResourseEntity.active_System = Index.System;
          }
        }
      }

      if (ResourceTyped.BirthDate != null)
      {
        if (ResourceTyped.BirthDateElement is Hl7.Fhir.Model.Date)
        {
          var Index = new DateIndex();
          Index = IndexSetterFactory.Create(typeof(DateIndex)).Set(ResourceTyped.BirthDateElement, Index) as DateIndex;
          if (Index != null)
          {
            ResourseEntity.birthdate_Date = Index.Date;
          }
        }
      }

      if (ResourceTyped.Gender != null)
      {
        if (ResourceTyped.GenderElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.AdministrativeGender>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.GenderElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.gender_Code = Index.Code;
            ResourseEntity.gender_System = Index.System;
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

      if (ResourceTyped.Address != null)
      {
        foreach (var item2 in ResourceTyped.Address)
        {
          var Index = new Res_RelatedPerson_Index_address();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item2, Index) as Res_RelatedPerson_Index_address;
          ResourseEntity.address_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Address)
      {
        if (item1.City != null)
        {
          if (item1.CityElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_RelatedPerson_Index_address_city();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.CityElement, Index) as Res_RelatedPerson_Index_address_city;
            ResourseEntity.address_city_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Address)
      {
        if (item1.Country != null)
        {
          if (item1.CountryElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_RelatedPerson_Index_address_country();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.CountryElement, Index) as Res_RelatedPerson_Index_address_country;
            ResourseEntity.address_country_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Address)
      {
        if (item1.PostalCode != null)
        {
          if (item1.PostalCodeElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_RelatedPerson_Index_address_postalcode();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.PostalCodeElement, Index) as Res_RelatedPerson_Index_address_postalcode;
            ResourseEntity.address_postalcode_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Address)
      {
        if (item1.State != null)
        {
          if (item1.StateElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_RelatedPerson_Index_address_state();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.StateElement, Index) as Res_RelatedPerson_Index_address_state;
            ResourseEntity.address_state_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Address)
      {
        if (item1.Use != null)
        {
          if (item1.UseElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Address.AddressUse>)
          {
            var Index = new Res_RelatedPerson_Index_address_use();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.UseElement, Index) as Res_RelatedPerson_Index_address_use;
            ResourseEntity.address_use_List.Add(Index);
          }
        }
      }

      foreach (var item2 in ResourceTyped.Telecom)
      {
        if (item2.System != null)
        {
          if ((ContactPoint.ContactPointSystem)item2.System == ContactPoint.ContactPointSystem.Email)
          {
            if (item2 is ContactPoint)
            {
              var Index = new Res_RelatedPerson_Index_email();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item2, Index) as Res_RelatedPerson_Index_email;
              ResourseEntity.email_List.Add(Index);
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
            var Index = new Res_RelatedPerson_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_RelatedPerson_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Name != null)
      {
        foreach (var item3 in ResourceTyped.Name)
        {
          var Index = new Res_RelatedPerson_Index_name();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item3, Index) as Res_RelatedPerson_Index_name;
          ResourseEntity.name_List.Add(Index);
        }
      }

      foreach (var item2 in ResourceTyped.Telecom)
      {
        if (item2.System != null)
        {
          if ((ContactPoint.ContactPointSystem)item2.System == ContactPoint.ContactPointSystem.Phone)
          {
            if (item2 is ContactPoint)
            {
              var Index = new Res_RelatedPerson_Index_phone();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item2, Index) as Res_RelatedPerson_Index_phone;
              ResourseEntity.phone_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Name != null)
      {
        foreach (var item3 in ResourceTyped.Name)
        {
          var Index = new Res_RelatedPerson_Index_phonetic();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item3, Index) as Res_RelatedPerson_Index_phonetic;
          ResourseEntity.phonetic_List.Add(Index);
        }
      }

      foreach (var item2 in ResourceTyped.Telecom)
      {
        if (item2 is ContactPoint)
        {
          var Index = new Res_RelatedPerson_Index_telecom();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item2, Index) as Res_RelatedPerson_Index_telecom;
          ResourseEntity.telecom_List.Add(Index);
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
              var Index = new Res_RelatedPerson_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_RelatedPerson_Index__profile;
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
              var Index = new Res_RelatedPerson_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_RelatedPerson_Index__security;
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
              var Index = new Res_RelatedPerson_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_RelatedPerson_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

