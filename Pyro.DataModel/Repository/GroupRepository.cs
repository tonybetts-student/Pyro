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
  public partial class GroupRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Group, new() 
    where ResourceHistoryType :Res_Group_History, new()
  {
    public GroupRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Group_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.characteristic_List);
      IncludeList.Add(x => x.code_List);
      IncludeList.Add(x => x.exclude_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.member_List);
      IncludeList.Add(x => x.value_List);
      IncludeList.Add(x => x.value_List);
      IncludeList.Add(x => x.value_List);
      IncludeList.Add(x => x.value_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.actual_Code = null;      
      ResourceEntity.actual_System = null;      
      ResourceEntity.type_Code = null;      
      ResourceEntity.type_System = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Group_Index_characteristic.RemoveRange(ResourceEntity.characteristic_List);            
      _Context.Res_Group_Index_code.RemoveRange(ResourceEntity.code_List);            
      _Context.Res_Group_Index_exclude.RemoveRange(ResourceEntity.exclude_List);            
      _Context.Res_Group_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_Group_Index_member.RemoveRange(ResourceEntity.member_List);            
      _Context.Res_Group_Index_value.RemoveRange(ResourceEntity.value_List);            
      _Context.Res_Group_Index_value.RemoveRange(ResourceEntity.value_List);            
      _Context.Res_Group_Index_value.RemoveRange(ResourceEntity.value_List);            
      _Context.Res_Group_Index_value.RemoveRange(ResourceEntity.value_List);            
      _Context.Res_Group_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Group_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Group_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Group;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Actual != null)
      {
        if (ResourceTyped.ActualElement is Hl7.Fhir.Model.FhirBoolean)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.ActualElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.actual_Code = Index.Code;
            ResourseEntity.actual_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Type != null)
      {
        if (ResourceTyped.TypeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Group.GroupType>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.TypeElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.type_Code = Index.Code;
            ResourseEntity.type_System = Index.System;
          }
        }
      }

      foreach (var item1 in ResourceTyped.Characteristic)
      {
        if (item1.Code != null)
        {
          foreach (var item4 in item1.Code.Coding)
          {
            var Index = new Res_Group_Index_characteristic();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Group_Index_characteristic;
            ResourseEntity.characteristic_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Code != null)
      {
        foreach (var item3 in ResourceTyped.Code.Coding)
        {
          var Index = new Res_Group_Index_code();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Group_Index_code;
          ResourseEntity.code_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Characteristic)
      {
        if (item1.Exclude != null)
        {
          if (item1.ExcludeElement is Hl7.Fhir.Model.FhirBoolean)
          {
            var Index = new Res_Group_Index_exclude();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.ExcludeElement, Index) as Res_Group_Index_exclude;
            ResourseEntity.exclude_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_Group_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Group_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Member)
      {
        if (item1.Entity != null)
        {
          if (item1.Entity is ResourceReference)
          {
            var Index = new Res_Group_Index_member();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Entity, Index, FhirRequestUri, this) as Res_Group_Index_member;
            if (Index != null)
            {
              ResourseEntity.member_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Characteristic)
      {
        if (item1.Value != null)
        {
          if (item1.Value is CodeableConcept)
          {
            CodeableConcept CodeableConcept = item1.Value as CodeableConcept;
            foreach (var item4 in CodeableConcept.Coding)
            {
              var Index = new Res_Group_Index_value();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Group_Index_value;
              ResourseEntity.value_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Characteristic)
      {
        if (item1.Value != null)
        {
          if (item1.Value is Hl7.Fhir.Model.FhirBoolean)
          {
            var Index = new Res_Group_Index_value();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.Value, Index) as Res_Group_Index_value;
            ResourseEntity.value_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Characteristic)
      {
        if (item1.Value != null)
        {
          if (item1.Value is Hl7.Fhir.Model.Quantity)
          {
            var Index = new Res_Group_Index_value();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.Value, Index) as Res_Group_Index_value;
            ResourseEntity.value_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Characteristic)
      {
        if (item1.Value != null)
        {
          if (item1.Value is Hl7.Fhir.Model.Range)
          {
            var Index = new Res_Group_Index_value();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.Value, Index) as Res_Group_Index_value;
            ResourseEntity.value_List.Add(Index);
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
              var Index = new Res_Group_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Group_Index__profile;
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
              var Index = new Res_Group_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Group_Index__security;
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
              var Index = new Res_Group_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Group_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

