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
  public partial class OperationDefinitionRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_OperationDefinition, new() 
    where ResourceHistoryType :Res_OperationDefinition_History, new()
  {
    public OperationDefinitionRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_OperationDefinition_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.context_List);
      IncludeList.Add(x => x.paramprofile_List);
      IncludeList.Add(x => x.type_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.base_VersionId = null;      
      ResourceEntity.base_FhirId = null;      
      ResourceEntity.base_Type = null;      
      ResourceEntity.base_Url = null;      
      ResourceEntity.base_ServiceRootURL_StoreID = null;      
      ResourceEntity.code_Code = null;      
      ResourceEntity.code_System = null;      
      ResourceEntity.date_DateTimeOffset = null;      
      ResourceEntity.instance_Code = null;      
      ResourceEntity.instance_System = null;      
      ResourceEntity.kind_Code = null;      
      ResourceEntity.kind_System = null;      
      ResourceEntity.name_String = null;      
      ResourceEntity.publisher_String = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.system_Code = null;      
      ResourceEntity.system_System = null;      
      ResourceEntity.url_Uri = null;      
      ResourceEntity.version_Code = null;      
      ResourceEntity.version_System = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_OperationDefinition_Index_context.RemoveRange(ResourceEntity.context_List);            
      _Context.Res_OperationDefinition_Index_paramprofile.RemoveRange(ResourceEntity.paramprofile_List);            
      _Context.Res_OperationDefinition_Index_type.RemoveRange(ResourceEntity.type_List);            
      _Context.Res_OperationDefinition_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_OperationDefinition_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_OperationDefinition_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as OperationDefinition;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Base != null)
      {
        if (ResourceTyped.Base is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Base, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.base_Type = Index.Type;
            ResourseEntity.base_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.base_Url = Index.Url;
            }
            else
            {
              ResourseEntity.base_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      if (ResourceTyped.Code != null)
      {
        if (ResourceTyped.CodeElement is Hl7.Fhir.Model.Code)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.CodeElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.code_Code = Index.Code;
            ResourseEntity.code_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Date != null)
      {
        if (ResourceTyped.DateElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateTimeIndex();
          Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(ResourceTyped.DateElement, Index) as DateTimeIndex;
          if (Index != null)
          {
            ResourseEntity.date_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Instance != null)
      {
        if (ResourceTyped.InstanceElement is Hl7.Fhir.Model.FhirBoolean)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.InstanceElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.instance_Code = Index.Code;
            ResourseEntity.instance_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Kind != null)
      {
        if (ResourceTyped.KindElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.OperationDefinition.OperationKind>)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.KindElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.kind_Code = Index.Code;
            ResourseEntity.kind_System = Index.System;
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

      if (ResourceTyped.Publisher != null)
      {
        if (ResourceTyped.PublisherElement is Hl7.Fhir.Model.FhirString)
        {
          var Index = new StringIndex();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.PublisherElement, Index) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.publisher_String = Index.String;
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ConformanceResourceStatus>)
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

      if (ResourceTyped.System != null)
      {
        if (ResourceTyped.SystemElement is Hl7.Fhir.Model.FhirBoolean)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.SystemElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.system_Code = Index.Code;
            ResourseEntity.system_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Url != null)
      {
        if (ResourceTyped.UrlElement is Hl7.Fhir.Model.FhirUri)
        {
          var Index = new UriIndex();
          Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(ResourceTyped.UrlElement, Index) as UriIndex;
          if (Index != null)
          {
            ResourseEntity.url_Uri = Index.Uri;
          }
        }
      }

      if (ResourceTyped.Version != null)
      {
        if (ResourceTyped.VersionElement is Hl7.Fhir.Model.FhirString)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.VersionElement, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.version_Code = Index.Code;
            ResourseEntity.version_System = Index.System;
          }
        }
      }

      if (ResourceTyped.UseContext != null)
      {
        foreach (var item3 in ResourceTyped.UseContext)
        {
          if (item3 != null)
          {
            foreach (var item4 in item3.Coding)
            {
              var Index = new Res_OperationDefinition_Index_context();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_OperationDefinition_Index_context;
              ResourseEntity.context_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Parameter)
      {
        if (item1.Profile != null)
        {
          if (item1.Profile is ResourceReference)
          {
            var Index = new Res_OperationDefinition_Index_paramprofile();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item1.Profile, Index, FhirRequestUri, this) as Res_OperationDefinition_Index_paramprofile;
            if (Index != null)
            {
              ResourseEntity.paramprofile_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Type != null)
      {
        foreach (var item3 in ResourceTyped.TypeElement)
        {
          if (item3 is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>)
          {
            var Index = new Res_OperationDefinition_Index_type();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_OperationDefinition_Index_type;
            ResourseEntity.type_List.Add(Index);
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
              var Index = new Res_OperationDefinition_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_OperationDefinition_Index__profile;
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
              var Index = new Res_OperationDefinition_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_OperationDefinition_Index__security;
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
              var Index = new Res_OperationDefinition_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_OperationDefinition_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

