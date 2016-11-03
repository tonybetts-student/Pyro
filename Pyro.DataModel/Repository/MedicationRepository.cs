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
  public partial class MedicationRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Medication, new() 
    where ResourceHistoryType :Res_Medication_History, new()
  {
    public MedicationRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Medication_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.container_List);
      IncludeList.Add(x => x.form_List);
      IncludeList.Add(x => x.ingredient_List);
      IncludeList.Add(x => x.ingredient_code_List);
      IncludeList.Add(x => x.package_item_List);
      IncludeList.Add(x => x.package_item_code_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.manufacturer_VersionId = null;      
      ResourceEntity.manufacturer_FhirId = null;      
      ResourceEntity.manufacturer_Type = null;      
      ResourceEntity.manufacturer_Url = null;      
      ResourceEntity.manufacturer_ServiceRootURL_StoreID = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Medication_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_Medication_Index_container.RemoveRange(ResourceEntity.container_List);            
      _Context.Res_Medication_Index_form.RemoveRange(ResourceEntity.form_List);            
      _Context.Res_Medication_Index_ingredient.RemoveRange(ResourceEntity.ingredient_List);            
      _Context.Res_Medication_Index_ingredient_code.RemoveRange(ResourceEntity.ingredient_code_List);            
      _Context.Res_Medication_Index_package_item.RemoveRange(ResourceEntity.package_item_List);            
      _Context.Res_Medication_Index_package_item_code.RemoveRange(ResourceEntity.package_item_code_List);            
      _Context.Res_Medication_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Medication_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Medication_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Medication;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Manufacturer != null)
      {
        if (ResourceTyped.Manufacturer is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Manufacturer, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.manufacturer_Type = Index.Type;
            ResourseEntity.manufacturer_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.manufacturer_Url = Index.Url;
            }
            else
            {
              ResourseEntity.manufacturer_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Code != null)
      {
        foreach (var item3 in ResourceTyped.Code.Coding)
        {
          var Index = new Res_Medication_Index_code();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Medication_Index_code;
          ResourseEntity.code_List.Add(Index);
        }
      }

      if (ResourceTyped.Package != null)
      {
        if (ResourceTyped.Package.Container != null)
        {
          foreach (var item4 in ResourceTyped.Package.Container.Coding)
          {
            var Index = new Res_Medication_Index_container();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Medication_Index_container;
            ResourseEntity.container_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Product != null)
      {
        if (ResourceTyped.Product.Form != null)
        {
          foreach (var item4 in ResourceTyped.Product.Form.Coding)
          {
            var Index = new Res_Medication_Index_form();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Medication_Index_form;
            ResourseEntity.form_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Product != null)
      {
        foreach (var item2 in ResourceTyped.Product.Ingredient)
        {
          if (item2.Item != null)
          {
            if (item2.Item is ResourceReference)
            {
              var Index = new Res_Medication_Index_ingredient();
              Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item2.Item, Index, FhirRequestUri, this) as Res_Medication_Index_ingredient;
              if (Index != null)
              {
                ResourseEntity.ingredient_List.Add(Index);
              }
            }
          }
        }
      }

      if (ResourceTyped.Product != null)
      {
        foreach (var item2 in ResourceTyped.Product.Ingredient)
        {
          if (item2.Item != null)
          {
            if (item2.Item is CodeableConcept)
            {
              CodeableConcept CodeableConcept = item2.Item as CodeableConcept;
              foreach (var item5 in CodeableConcept.Coding)
              {
                var Index = new Res_Medication_Index_ingredient_code();
                Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item5, Index) as Res_Medication_Index_ingredient_code;
                ResourseEntity.ingredient_code_List.Add(Index);
              }
            }
          }
        }
      }

      if (ResourceTyped.Package != null)
      {
        foreach (var item2 in ResourceTyped.Package.Content)
        {
          if (item2.Item != null)
          {
            if (item2.Item is ResourceReference)
            {
              var Index = new Res_Medication_Index_package_item();
              Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item2.Item, Index, FhirRequestUri, this) as Res_Medication_Index_package_item;
              if (Index != null)
              {
                ResourseEntity.package_item_List.Add(Index);
              }
            }
          }
        }
      }

      if (ResourceTyped.Package != null)
      {
        foreach (var item2 in ResourceTyped.Package.Content)
        {
          if (item2.Item != null)
          {
            if (item2.Item is CodeableConcept)
            {
              CodeableConcept CodeableConcept = item2.Item as CodeableConcept;
              foreach (var item5 in CodeableConcept.Coding)
              {
                var Index = new Res_Medication_Index_package_item_code();
                Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item5, Index) as Res_Medication_Index_package_item_code;
                ResourseEntity.package_item_code_List.Add(Index);
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
              var Index = new Res_Medication_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Medication_Index__profile;
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
              var Index = new Res_Medication_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Medication_Index__security;
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
              var Index = new Res_Medication_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Medication_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

