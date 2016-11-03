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
  public partial class SubstanceRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Substance, new() 
    where ResourceHistoryType :Res_Substance_History, new()
  {
    public SubstanceRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Substance_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.category_List);
      IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.container_identifier_List);
      IncludeList.Add(x => x.expiry_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.quantity_List);
      IncludeList.Add(x => x.substance_reference_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Substance_Index_category.RemoveRange(ResourceEntity.category_List);            
      _Context.Res_Substance_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_Substance_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_Substance_Index_container_identifier.RemoveRange(ResourceEntity.container_identifier_List);            
      _Context.Res_Substance_Index_expiry.RemoveRange(ResourceEntity.expiry_List);            
      _Context.Res_Substance_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_Substance_Index_quantity.RemoveRange(ResourceEntity.quantity_List);            
      _Context.Res_Substance_Index_substance_reference.RemoveRange(ResourceEntity.substance_reference_List);            
      _Context.Res_Substance_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Substance_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Substance_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Substance;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Category != null)
      {
        foreach (var item3 in ResourceTyped.Category)
        {
          if (item3 != null)
          {
            foreach (var item4 in item3.Coding)
            {
              var Index = new Res_Substance_Index_category();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Substance_Index_category;
              ResourseEntity.category_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Code != null)
      {
        foreach (var item3 in ResourceTyped.Code.Coding)
        {
          var Index = new Res_Substance_Index_code();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Substance_Index_code;
          ResourseEntity.code_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Ingredient)
      {
        if (item1.Substance != null)
        {
          if (item1.Substance is CodeableConcept)
          {
            CodeableConcept CodeableConcept = item1.Substance as CodeableConcept;
            foreach (var item4 in CodeableConcept.Coding)
            {
              var Index = new Res_Substance_Index_code();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Substance_Index_code;
              ResourseEntity.code_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Instance)
      {
        if (item1.Identifier != null)
        {
          if (item1.Identifier is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_Substance_Index_container_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.Identifier, Index) as Res_Substance_Index_container_identifier;
            ResourseEntity.container_identifier_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Instance)
      {
        if (item1.Expiry != null)
        {
          if (item1.ExpiryElement is Hl7.Fhir.Model.FhirDateTime)
          {
            var Index = new Res_Substance_Index_expiry();
            Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(item1.ExpiryElement, Index) as Res_Substance_Index_expiry;
            ResourseEntity.expiry_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_Substance_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Substance_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Instance)
      {
        if (item1.Quantity != null)
        {
          if (item1.Quantity is Hl7.Fhir.Model.SimpleQuantity)
          {
            var Index = new Res_Substance_Index_quantity();
            Index = IndexSetterFactory.Create(typeof(QuantityIndex)).Set(item1.Quantity, Index) as Res_Substance_Index_quantity;
            ResourseEntity.quantity_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Ingredient)
      {
        if (item1.Substance != null)
        {
          if (item1.Substance is ResourceReference)
          {
            var Index = new Res_Substance_Index_substance_reference();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Substance, Index, FhirRequestUri, this) as Res_Substance_Index_substance_reference;
            if (Index != null)
            {
              ResourseEntity.substance_reference_List.Add(Index);
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
              var Index = new Res_Substance_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Substance_Index__profile;
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
              var Index = new Res_Substance_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Substance_Index__security;
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
              var Index = new Res_Substance_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Substance_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

