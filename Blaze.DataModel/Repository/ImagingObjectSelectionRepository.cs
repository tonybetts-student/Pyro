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
  public partial class ImagingObjectSelectionRepository : CommonRepository, IResourceRepository
  {

    public ImagingObjectSelectionRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    public IDatabaseOperationOutcome AddResource(Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as ImagingObjectSelection;
      var ResourceEntity = new Res_ImagingObjectSelection();
      this.PopulateResourceEntity(ResourceEntity, "1", ResourceTyped, FhirRequestUri);
      this.DbAddEntity<Res_ImagingObjectSelection>(ResourceEntity);
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;     
      DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ResourcesMatchingSearchCount = 1;
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome UpdateResource(string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as ImagingObjectSelection;
      var ResourceEntity = LoadCurrentResourceEntity(Resource.Id);
      var ResourceHistoryEntity = new Res_ImagingObjectSelection_History();  
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_ImagingObjectSelection_History_List.Add(ResourceHistoryEntity); 
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
      var ResourceHistoryEntity = new Res_ImagingObjectSelection_History();
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_ImagingObjectSelection_History_List.Add(ResourceHistoryEntity);
      this.ResetResourceEntity(ResourceEntity);
      ResourceEntity.IsDeleted = true;
      ResourceEntity.versionId = ResourceVersion;
      this.Save();      
    }

    public IDatabaseOperationOutcome GetResourceByFhirIDAndVersionNumber(string FhirResourceId, string ResourceVersionNumber)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      var ResourceHistoryEntity = DbGet<Res_ImagingObjectSelection_History>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
      if (ResourceHistoryEntity != null)
      {
        DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceHistoryEntity);
      }
      else
      {
        var ResourceEntity = DbGet<Res_ImagingObjectSelection>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
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
        DtoResource = DbGetALL<Res_ImagingObjectSelection>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated, Xml = x.XmlBlob }).SingleOrDefault();       
      }
      else
      {
        DtoResource = DbGetALL<Res_ImagingObjectSelection>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated }).SingleOrDefault();        
      }
      DatabaseOperationOutcome.ResourceMatchingSearch = DtoResource;
      return DatabaseOperationOutcome;
    }

    private Res_ImagingObjectSelection LoadCurrentResourceEntity(string FhirId)
    {

      var IncludeList = new List<Expression<Func<Res_ImagingObjectSelection, object>>>();
      IncludeList.Add(x => x.selected_study_List);
      IncludeList.Add(x => x.title_List);
      IncludeList.Add(x => x.profile_List);
      IncludeList.Add(x => x.security_List);
      IncludeList.Add(x => x.tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<Res_ImagingObjectSelection>(x => x.FhirId == FhirId, IncludeList);

      return ResourceEntity;
    }


    private void ResetResourceEntity(Res_ImagingObjectSelection ResourceEntity)
    {
      ResourceEntity.author_FhirId = null;      
      ResourceEntity.author_Type = null;      
      ResourceEntity.author_Url = null;      
      ResourceEntity.author_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.authoring_time_DateTimeOffset = null;      
      ResourceEntity.identifier_Uri = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_ImagingObjectSelection_Index_selected_study.RemoveRange(ResourceEntity.selected_study_List);            
      _Context.Res_ImagingObjectSelection_Index_title.RemoveRange(ResourceEntity.title_List);            
      _Context.Res_ImagingObjectSelection_Index_profile.RemoveRange(ResourceEntity.profile_List);            
      _Context.Res_ImagingObjectSelection_Index_security.RemoveRange(ResourceEntity.security_List);            
      _Context.Res_ImagingObjectSelection_Index_tag.RemoveRange(ResourceEntity.tag_List);            
 
    }

    private void PopulateResourceEntity(Res_ImagingObjectSelection ResourseEntity, string ResourceVersion, ImagingObjectSelection ResourceTyped, IDtoFhirRequestUri FhirRequestUri)
    {
       IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Author != null)
      {
        if (ResourceTyped.Author is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Author, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.author_Type = Index.Type;
            ResourseEntity.author_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.author_Url = Index.Url;
            }
            else
            {
              ResourseEntity.author_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.AuthoringTime != null)
      {
        if (ResourceTyped.AuthoringTimeElement is Hl7.Fhir.Model.FhirDateTime)
        {
          var Index = new DateIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.AuthoringTimeElement) as DateIndex;
          if (Index != null)
          {
            ResourseEntity.authoring_time_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Uid != null)
      {
        if (ResourceTyped.UidElement is Hl7.Fhir.Model.Oid)
        {
          var Index = new UriIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.UidElement) as UriIndex;
          if (Index != null)
          {
            ResourseEntity.identifier_Uri = Index.Uri;
          }
        }
      }

      if (ResourceTyped.Patient != null)
      {
        if (ResourceTyped.Patient is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Patient, FhirRequestUri, this) as ReferenceIndex;
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
              ResourseEntity.patient_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Study)
      {
        if (item1.Uid != null)
        {
          if (item1.UidElement is Hl7.Fhir.Model.Oid)
          {
            var Index = new Res_ImagingObjectSelection_Index_selected_study();
            Index = IndexSettingSupport.SetIndex(Index, item1.UidElement) as Res_ImagingObjectSelection_Index_selected_study;
            ResourseEntity.selected_study_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Title != null)
      {
        foreach (var item3 in ResourceTyped.Title.Coding)
        {
          var Index = new Res_ImagingObjectSelection_Index_title();
          Index = IndexSettingSupport.SetIndex(Index, item3) as Res_ImagingObjectSelection_Index_title;
          ResourseEntity.title_List.Add(Index);
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
              var Index = new Res_ImagingObjectSelection_Index_profile();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_ImagingObjectSelection_Index_profile;
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
              var Index = new Res_ImagingObjectSelection_Index_security();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_ImagingObjectSelection_Index_security;
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
              var Index = new Res_ImagingObjectSelection_Index_tag();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_ImagingObjectSelection_Index_tag;
              ResourseEntity.tag_List.Add(Index);
            }
          }
        }
      }


      

    }


  }
} 
