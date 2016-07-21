﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq.Expressions;
using Blaze.DataModel.DatabaseModel;
using Blaze.DataModel.DatabaseModel.Base;
using Blaze.DataModel.Support;
using Hl7.Fhir.Model;
using Blaze.Common.BusinessEntities;
using Blaze.Common.Interfaces;
using Blaze.Common.Interfaces.Repositories;
using Blaze.Common.Interfaces.UriSupport;
using Hl7.Fhir.Introspection;

namespace Blaze.DataModel.Repository
{
  public partial class MeasureRepository : CommonRepository, IResourceRepository
  {

    public MeasureRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    public IDatabaseOperationOutcome AddResource(Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Measure;
      var ResourceEntity = new Res_Measure();
      this.PopulateResourceEntity(ResourceEntity, "1", ResourceTyped, FhirRequestUri);
      this.DbAddEntity<Res_Measure>(ResourceEntity);
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;     
      DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ResourcesMatchingSearchCount = 1;
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome UpdateResource(string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Measure;
      var ResourceEntity = LoadCurrentResourceEntity(Resource.Id);
      var ResourceHistoryEntity = new Res_Measure_History();  
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_Measure_History_List.Add(ResourceHistoryEntity); 
      this.ResetResourceEntity(ResourceEntity);
      this.PopulateResourceEntity(ResourceEntity, ResourceVersion, ResourceTyped, FhirRequestUri);            
      this.Save();            
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ResourcesMatchingSearchCount = 1;
      return DatabaseOperationOutcome;
    }

    public void UpdateResouceAsDeleted(string FhirResourceId, string ResourceVersion)
    {
      var ResourceEntity = this.LoadCurrentResourceEntity(FhirResourceId);
      var ResourceHistoryEntity = new Res_Measure_History();
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_Measure_History_List.Add(ResourceHistoryEntity);
      this.ResetResourceEntity(ResourceEntity);
      ResourceEntity.IsDeleted = true;
      ResourceEntity.versionId = ResourceVersion;
      this.Save();      
    }

    public IDatabaseOperationOutcome GetResourceByFhirIDAndVersionNumber(string FhirResourceId, string ResourceVersionNumber)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      var ResourceHistoryEntity = DbGet<Res_Measure_History>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
      if (ResourceHistoryEntity != null)
      {
        DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceHistoryEntity);
      }
      else
      {
        var ResourceEntity = DbGet<Res_Measure>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
        if (ResourceEntity != null)
          DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);        
      }
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome GetResourceByFhirID(string FhirResourceId, bool WithXml = false)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      Blaze.Common.BusinessEntities.Dto.DtoResource DtoResource = null;
      if (WithXml)
      {        
        DtoResource = DbGetALL<Res_Measure>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated, Xml = x.XmlBlob }).SingleOrDefault();       
      }
      else
      {
        DtoResource = DbGetALL<Res_Measure>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated }).SingleOrDefault();        
      }
      DatabaseOperationOutcome.ResourceMatchingSearch = DtoResource;
      return DatabaseOperationOutcome;
    }

    private Res_Measure LoadCurrentResourceEntity(string FhirId)
    {

      var IncludeList = new List<Expression<Func<Res_Measure, object>>>();
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.topic_List);
      IncludeList.Add(x => x.profile_List);
      IncludeList.Add(x => x.security_List);
      IncludeList.Add(x => x.tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<Res_Measure>(x => x.FhirId == FhirId, IncludeList);

      return ResourceEntity;
    }


    private void ResetResourceEntity(Res_Measure ResourceEntity)
    {
      ResourceEntity.description_String = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.title_String = null;      
      ResourceEntity.version_String = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Measure_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_Measure_Index_topic.RemoveRange(ResourceEntity.topic_List);            
      _Context.Res_Measure_Index_profile.RemoveRange(ResourceEntity.profile_List);            
      _Context.Res_Measure_Index_security.RemoveRange(ResourceEntity.security_List);            
      _Context.Res_Measure_Index_tag.RemoveRange(ResourceEntity.tag_List);            
 
    }

    private void PopulateResourceEntity(Res_Measure ResourseEntity, string ResourceVersion, Measure ResourceTyped, IDtoFhirRequestUri FhirRequestUri)
    {
       IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.ModuleMetadata != null)
      {
        if (ResourceTyped.ModuleMetadata.Description != null)
        {
          if (ResourceTyped.ModuleMetadata.DescriptionElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.ModuleMetadata.DescriptionElement) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.description_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.ModuleMetadata != null)
      {
        if (ResourceTyped.ModuleMetadata.Status != null)
        {
          if (ResourceTyped.ModuleMetadata.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ModuleMetadata.ModuleMetadataStatus>)
          {
            var Index = new TokenIndex();
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.ModuleMetadata.StatusElement) as TokenIndex;
            if (Index != null)
            {
              ResourseEntity.status_Code = Index.Code;
              ResourseEntity.status_System = Index.System;
            }
          }
        }
      }

      if (ResourceTyped.ModuleMetadata != null)
      {
        if (ResourceTyped.ModuleMetadata.Title != null)
        {
          if (ResourceTyped.ModuleMetadata.TitleElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.ModuleMetadata.TitleElement) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.title_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.ModuleMetadata != null)
      {
        if (ResourceTyped.ModuleMetadata.Version != null)
        {
          if (ResourceTyped.ModuleMetadata.VersionElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.ModuleMetadata.VersionElement) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.version_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.ModuleMetadata != null)
      {
        if (ResourceTyped.ModuleMetadata.Identifier != null)
        {
          foreach (var item4 in ResourceTyped.ModuleMetadata.Identifier)
          {
            if (item4 is Hl7.Fhir.Model.Identifier)
            {
              var Index = new Res_Measure_Index_identifier();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_Measure_Index_identifier;
              ResourseEntity.identifier_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.ModuleMetadata != null)
      {
        if (ResourceTyped.ModuleMetadata.Topic != null)
        {
          foreach (var item4 in ResourceTyped.ModuleMetadata.Topic)
          {
            if (item4 != null)
            {
              foreach (var item5 in item4.Coding)
              {
                var Index = new Res_Measure_Index_topic();
                Index = IndexSettingSupport.SetIndex(Index, item5) as Res_Measure_Index_topic;
                ResourseEntity.topic_List.Add(Index);
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
              var Index = new Res_Measure_Index_profile();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_Measure_Index_profile;
              ResourseEntity.profile_List.Add(Index);
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
              var Index = new Res_Measure_Index_security();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_Measure_Index_security;
              ResourseEntity.security_List.Add(Index);
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
              var Index = new Res_Measure_Index_tag();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_Measure_Index_tag;
              ResourseEntity.tag_List.Add(Index);
            }
          }
        }
      }


      

    }


  }
} 
