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
  public partial class ConformanceRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Conformance, new() 
    where ResourceHistoryType :Res_Conformance_History, new()
  {
    public ConformanceRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Conformance_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.context_List);
      IncludeList.Add(x => x.event_List);
      IncludeList.Add(x => x.format_List);
      IncludeList.Add(x => x.mode_List);
      IncludeList.Add(x => x.resource_List);
      IncludeList.Add(x => x.resourceprofile_List);
      IncludeList.Add(x => x.securityservice_List);
      IncludeList.Add(x => x.supported_profile_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.date_DateTimeOffset = null;      
      ResourceEntity.description_String = null;      
      ResourceEntity.fhirversion_Code = null;      
      ResourceEntity.fhirversion_System = null;      
      ResourceEntity.name_String = null;      
      ResourceEntity.publisher_String = null;      
      ResourceEntity.software_String = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.url_Uri = null;      
      ResourceEntity.version_Code = null;      
      ResourceEntity.version_System = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Conformance_Index_context.RemoveRange(ResourceEntity.context_List);            
      _Context.Res_Conformance_Index_event.RemoveRange(ResourceEntity.event_List);            
      _Context.Res_Conformance_Index_format.RemoveRange(ResourceEntity.format_List);            
      _Context.Res_Conformance_Index_mode.RemoveRange(ResourceEntity.mode_List);            
      _Context.Res_Conformance_Index_resource.RemoveRange(ResourceEntity.resource_List);            
      _Context.Res_Conformance_Index_resourceprofile.RemoveRange(ResourceEntity.resourceprofile_List);            
      _Context.Res_Conformance_Index_securityservice.RemoveRange(ResourceEntity.securityservice_List);            
      _Context.Res_Conformance_Index_supported_profile.RemoveRange(ResourceEntity.supported_profile_List);            
      _Context.Res_Conformance_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Conformance_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Conformance_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Conformance;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

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

      if (ResourceTyped.Description != null)
      {
        if (ResourceTyped.Description is Hl7.Fhir.Model.Markdown)
        {
          var Index = new StringIndex();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Description, Index) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.description_String = Index.String;
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
            ResourseEntity.fhirversion_Code = Index.Code;
            ResourseEntity.fhirversion_System = Index.System;
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

      if (ResourceTyped.Software != null)
      {
        if (ResourceTyped.Software.Name != null)
        {
          if (ResourceTyped.Software.NameElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Software.NameElement, Index) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.software_String = Index.String;
            }
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
              var Index = new Res_Conformance_Index_context();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Conformance_Index_context;
              ResourseEntity.context_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Messaging)
      {
        foreach (var item2 in item1.Event)
        {
          if (item2.Code != null)
          {
            if (item2.Code is Hl7.Fhir.Model.Coding)
            {
              var Index = new Res_Conformance_Index_event();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item2.Code, Index) as Res_Conformance_Index_event;
              ResourseEntity.event_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Format != null)
      {
        foreach (var item3 in ResourceTyped.FormatElement)
        {
          if (item3 is Hl7.Fhir.Model.Code)
          {
            var Index = new Res_Conformance_Index_format();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Conformance_Index_format;
            ResourseEntity.format_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Rest)
      {
        if (item1.Mode != null)
        {
          if (item1.ModeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode>)
          {
            var Index = new Res_Conformance_Index_mode();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item1.ModeElement, Index) as Res_Conformance_Index_mode;
            ResourseEntity.mode_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Rest)
      {
        foreach (var item2 in item1.Resource)
        {
          if (item2.Type != null)
          {
            if (item2.TypeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>)
            {
              var Index = new Res_Conformance_Index_resource();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item2.TypeElement, Index) as Res_Conformance_Index_resource;
              ResourseEntity.resource_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Rest)
      {
        foreach (var item2 in item1.Resource)
        {
          if (item2.Profile != null)
          {
            if (item2.Profile is ResourceReference)
            {
              var Index = new Res_Conformance_Index_resourceprofile();
              Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item2.Profile, Index, FhirRequestUri, this) as Res_Conformance_Index_resourceprofile;
              if (Index != null)
              {
                ResourseEntity.resourceprofile_List.Add(Index);
              }
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Rest)
      {
        if (item1.Security != null)
        {
          if (item1.Security.Service != null)
          {
            foreach (var item5 in item1.Security.Service)
            {
              if (item5 != null)
              {
                foreach (var item6 in item5.Coding)
                {
                  var Index = new Res_Conformance_Index_securityservice();
                  Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item6, Index) as Res_Conformance_Index_securityservice;
                  ResourseEntity.securityservice_List.Add(Index);
                }
              }
            }
          }
        }
      }

      if (ResourceTyped.Profile != null)
      {
        foreach (var item in ResourceTyped.Profile)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_Conformance_Index_supported_profile();
            Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_Conformance_Index_supported_profile;
            if (Index != null)
            {
              ResourseEntity.supported_profile_List.Add(Index);
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
              var Index = new Res_Conformance_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Conformance_Index__profile;
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
              var Index = new Res_Conformance_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Conformance_Index__security;
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
              var Index = new Res_Conformance_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Conformance_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

