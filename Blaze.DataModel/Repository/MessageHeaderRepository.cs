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
  public partial class MessageHeaderRepository : CommonRepository, IResourceRepository
  {

    public MessageHeaderRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    public IDatabaseOperationOutcome AddResource(Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as MessageHeader;
      var ResourceEntity = new Res_MessageHeader();
      this.PopulateResourceEntity(ResourceEntity, "1", ResourceTyped, FhirRequestUri);
      this.DbAddEntity<Res_MessageHeader>(ResourceEntity);
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;     
      DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ResourcesMatchingSearchCount = 1;
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome UpdateResource(string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as MessageHeader;
      var ResourceEntity = LoadCurrentResourceEntity(Resource.Id);
      var ResourceHistoryEntity = new Res_MessageHeader_History();  
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_MessageHeader_History_List.Add(ResourceHistoryEntity); 
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
      var ResourceHistoryEntity = new Res_MessageHeader_History();
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_MessageHeader_History_List.Add(ResourceHistoryEntity);
      this.ResetResourceEntity(ResourceEntity);
      ResourceEntity.IsDeleted = true;
      ResourceEntity.versionId = ResourceVersion;
      this.Save();      
    }

    public IDatabaseOperationOutcome GetResourceByFhirIDAndVersionNumber(string FhirResourceId, string ResourceVersionNumber)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      var ResourceHistoryEntity = DbGet<Res_MessageHeader_History>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
      if (ResourceHistoryEntity != null)
      {
        DatabaseOperationOutcome.ResourceMatchingSearch = IndexSettingSupport.SetDtoResource(ResourceHistoryEntity);
      }
      else
      {
        var ResourceEntity = DbGet<Res_MessageHeader>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
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
        DtoResource = DbGetALL<Res_MessageHeader>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated, Xml = x.XmlBlob }).SingleOrDefault();       
      }
      else
      {
        DtoResource = DbGetALL<Res_MessageHeader>(x => x.FhirId == FhirResourceId).Select(x => new Blaze.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated }).SingleOrDefault();        
      }
      DatabaseOperationOutcome.ResourceMatchingSearch = DtoResource;
      return DatabaseOperationOutcome;
    }

    private Res_MessageHeader LoadCurrentResourceEntity(string FhirId)
    {

      var IncludeList = new List<Expression<Func<Res_MessageHeader, object>>>();
      IncludeList.Add(x => x.data_List);
      IncludeList.Add(x => x.destination_List);
      IncludeList.Add(x => x.destination_uri_List);
      IncludeList.Add(x => x.target_List);
      IncludeList.Add(x => x.profile_List);
      IncludeList.Add(x => x.security_List);
      IncludeList.Add(x => x.tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<Res_MessageHeader>(x => x.FhirId == FhirId, IncludeList);

      return ResourceEntity;
    }


    private void ResetResourceEntity(Res_MessageHeader ResourceEntity)
    {
      ResourceEntity.author_FhirId = null;      
      ResourceEntity.author_Type = null;      
      ResourceEntity.author_Url = null;      
      ResourceEntity.author_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.code_Code = null;      
      ResourceEntity.code_System = null;      
      ResourceEntity.enterer_FhirId = null;      
      ResourceEntity.enterer_Type = null;      
      ResourceEntity.enterer_Url = null;      
      ResourceEntity.enterer_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.event_Code = null;      
      ResourceEntity.event_System = null;      
      ResourceEntity.receiver_FhirId = null;      
      ResourceEntity.receiver_Type = null;      
      ResourceEntity.receiver_Url = null;      
      ResourceEntity.receiver_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.response_id_Code = null;      
      ResourceEntity.response_id_System = null;      
      ResourceEntity.responsible_FhirId = null;      
      ResourceEntity.responsible_Type = null;      
      ResourceEntity.responsible_Url = null;      
      ResourceEntity.responsible_Url_Blaze_RootUrlStoreID = null;      
      ResourceEntity.source_String = null;      
      ResourceEntity.source_uri_Uri = null;      
      ResourceEntity.timestamp_DateTimeOffset = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_MessageHeader_Index_data.RemoveRange(ResourceEntity.data_List);            
      _Context.Res_MessageHeader_Index_destination.RemoveRange(ResourceEntity.destination_List);            
      _Context.Res_MessageHeader_Index_destination_uri.RemoveRange(ResourceEntity.destination_uri_List);            
      _Context.Res_MessageHeader_Index_target.RemoveRange(ResourceEntity.target_List);            
      _Context.Res_MessageHeader_Index_profile.RemoveRange(ResourceEntity.profile_List);            
      _Context.Res_MessageHeader_Index_security.RemoveRange(ResourceEntity.security_List);            
      _Context.Res_MessageHeader_Index_tag.RemoveRange(ResourceEntity.tag_List);            
 
    }

    private void PopulateResourceEntity(Res_MessageHeader ResourseEntity, string ResourceVersion, MessageHeader ResourceTyped, IDtoFhirRequestUri FhirRequestUri)
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

      if (ResourceTyped.Response != null)
      {
        if (ResourceTyped.Response.Code != null)
        {
          if (ResourceTyped.Response.CodeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.MessageHeader.ResponseType>)
          {
            var Index = new TokenIndex();
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Response.CodeElement) as TokenIndex;
            if (Index != null)
            {
              ResourseEntity.code_Code = Index.Code;
              ResourseEntity.code_System = Index.System;
            }
          }
        }
      }

      if (ResourceTyped.Enterer != null)
      {
        if (ResourceTyped.Enterer is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Enterer, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.enterer_Type = Index.Type;
            ResourseEntity.enterer_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.enterer_Url = Index.Url;
            }
            else
            {
              ResourseEntity.enterer_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Event != null)
      {
        if (ResourceTyped.Event is Hl7.Fhir.Model.Coding)
        {
          var Index = new TokenIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Event) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.event_Code = Index.Code;
            ResourseEntity.event_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Receiver != null)
      {
        if (ResourceTyped.Receiver is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Receiver, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.receiver_Type = Index.Type;
            ResourseEntity.receiver_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.receiver_Url = Index.Url;
            }
            else
            {
              ResourseEntity.receiver_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Response != null)
      {
        if (ResourceTyped.Response.Identifier != null)
        {
          if (ResourceTyped.Response.IdentifierElement is Hl7.Fhir.Model.Id)
          {
            var Index = new TokenIndex();
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Response.IdentifierElement) as TokenIndex;
            if (Index != null)
            {
              ResourseEntity.response_id_Code = Index.Code;
              ResourseEntity.response_id_System = Index.System;
            }
          }
        }
      }

      if (ResourceTyped.Responsible != null)
      {
        if (ResourceTyped.Responsible is ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Responsible, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.responsible_Type = Index.Type;
            ResourseEntity.responsible_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.responsible_Url = Index.Url;
            }
            else
            {
              ResourseEntity.responsible_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Source != null)
      {
        if (ResourceTyped.Source.Name != null)
        {
          if (ResourceTyped.Source.NameElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Source.NameElement) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.source_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.Source != null)
      {
        if (ResourceTyped.Source.Endpoint != null)
        {
          if (ResourceTyped.Source.EndpointElement is Hl7.Fhir.Model.FhirUri)
          {
            var Index = new UriIndex();
            Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.Source.EndpointElement) as UriIndex;
            if (Index != null)
            {
              ResourseEntity.source_uri_Uri = Index.Uri;
            }
          }
        }
      }

      if (ResourceTyped.Timestamp != null)
      {
        if (ResourceTyped.TimestampElement is Hl7.Fhir.Model.Instant)
        {
          var Index = new DateIndex();
          Index = IndexSettingSupport.SetIndex(Index, ResourceTyped.TimestampElement) as DateIndex;
          if (Index != null)
          {
            ResourseEntity.timestamp_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Data != null)
      {
        foreach (var item in ResourceTyped.Data)
        {
          if (item is ResourceReference)
          {
            var Index = new Res_MessageHeader_Index_data();
            IndexSettingSupport.SetIndex(Index, item, FhirRequestUri, this);
            if (Index != null)
            {
              ResourseEntity.data_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Destination)
      {
        if (item1.Name != null)
        {
          if (item1.NameElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new Res_MessageHeader_Index_destination();
            Index = IndexSettingSupport.SetIndex(Index, item1.NameElement) as Res_MessageHeader_Index_destination;
            ResourseEntity.destination_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Destination)
      {
        if (item1.Endpoint != null)
        {
          if (item1.EndpointElement is Hl7.Fhir.Model.FhirUri)
          {
            var Index = new Res_MessageHeader_Index_destination_uri();
            Index = IndexSettingSupport.SetIndex(Index, item1.EndpointElement) as Res_MessageHeader_Index_destination_uri;
            ResourseEntity.destination_uri_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Destination)
      {
        if (item1.Target != null)
        {
          if (item1.Target is ResourceReference)
          {
            var Index = new Res_MessageHeader_Index_target();
            IndexSettingSupport.SetIndex(Index, item1.Target, FhirRequestUri, this);
            if (Index != null)
            {
              ResourseEntity.target_List.Add(Index);
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
              var Index = new Res_MessageHeader_Index_profile();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_MessageHeader_Index_profile;
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
              var Index = new Res_MessageHeader_Index_security();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_MessageHeader_Index_security;
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
              var Index = new Res_MessageHeader_Index_tag();
              Index = IndexSettingSupport.SetIndex(Index, item4) as Res_MessageHeader_Index_tag;
              ResourseEntity.tag_List.Add(Index);
            }
          }
        }
      }


      

    }


  }
} 
