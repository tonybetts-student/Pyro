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
  public partial class OrganizationRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Organization, new() 
    where ResourceHistoryType :Res_Organization_History, new()
  {
    public OrganizationRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Organization_History_List.Add(ResourceHistoryEntity);
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
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.name_List);
      IncludeList.Add(x => x.type_List);
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
      ResourceEntity.name_String = null;      
      ResourceEntity.partof_VersionId = null;      
      ResourceEntity.partof_FhirId = null;      
      ResourceEntity.partof_Type = null;      
      ResourceEntity.partof_Url = null;      
      ResourceEntity.partof_ServiceRootURL_StoreID = null;      
      ResourceEntity.phonetic_String = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Organization_Index_address.RemoveRange(ResourceEntity.address_List);            
      _Context.Res_Organization_Index_address_city.RemoveRange(ResourceEntity.address_city_List);            
      _Context.Res_Organization_Index_address_country.RemoveRange(ResourceEntity.address_country_List);            
      _Context.Res_Organization_Index_address_postalcode.RemoveRange(ResourceEntity.address_postalcode_List);            
      _Context.Res_Organization_Index_address_state.RemoveRange(ResourceEntity.address_state_List);            
      _Context.Res_Organization_Index_address_use.RemoveRange(ResourceEntity.address_use_List);            
      _Context.Res_Organization_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_Organization_Index_name.RemoveRange(ResourceEntity.name_List);            
      _Context.Res_Organization_Index_type.RemoveRange(ResourceEntity.type_List);            
      _Context.Res_Organization_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Organization_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Organization_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Organization;
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

      if (ResourceTyped.Name != null)
      {
        if (ResourceTyped.NameElement is Hl7.Fhir.Model.FhirString)
        {
          var Index = new StringIndex();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.NameElement, Index) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.name_String = Index.String;
          }
        }
      }

      if (ResourceTyped.PartOf != null)
      {
        if (ResourceTyped.PartOf is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.PartOf, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.partof_Type = Index.Type;
            ResourseEntity.partof_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.partof_Url = Index.Url;
            }
            else
            {
              ResourseEntity.partof_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Name != null)
      {
        if (ResourceTyped.NameElement is Hl7.Fhir.Model.FhirString)
        {
          var Index = new StringIndex();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.NameElement, Index) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.phonetic_String = Index.String;
          }
        }
      }

      if (ResourceTyped.Address != null)
      {
        foreach (var item2 in ResourceTyped.Address)
        {
          var Index = new Res_Organization_Index_address();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item2, Index) as Res_Organization_Index_address;
          ResourseEntity.address_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Address)
      {
        if (item1.City != null)
        {
          if (item1.CityElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_Organization_Index_address_city();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.CityElement, Index) as Res_Organization_Index_address_city;
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
            var Index = new Res_Organization_Index_address_country();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.CountryElement, Index) as Res_Organization_Index_address_country;
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
            var Index = new Res_Organization_Index_address_postalcode();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.PostalCodeElement, Index) as Res_Organization_Index_address_postalcode;
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
            var Index = new Res_Organization_Index_address_state();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item1.StateElement, Index) as Res_Organization_Index_address_state;
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
            var Index = new Res_Organization_Index_address_use();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.UseElement, Index) as Res_Organization_Index_address_use;
            ResourseEntity.address_use_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_Organization_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Organization_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Alias != null)
      {
        foreach (var item3 in ResourceTyped.AliasElement)
        {
          if (item3 is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_Organization_Index_name();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item3, Index) as Res_Organization_Index_name;
            ResourseEntity.name_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Type != null)
      {
        foreach (var item3 in ResourceTyped.Type.Coding)
        {
          var Index = new Res_Organization_Index_type();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Organization_Index_type;
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
              var Index = new Res_Organization_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Organization_Index__profile;
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
              var Index = new Res_Organization_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Organization_Index__security;
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
              var Index = new Res_Organization_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Organization_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

