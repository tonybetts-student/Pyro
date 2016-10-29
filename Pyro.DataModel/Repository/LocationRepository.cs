﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pyro.DataModel.DatabaseModel;
using Pyro.DataModel.DatabaseModel.Base;
using Pyro.DataModel.Support;
using Pyro.DataModel.IndexSetter;
using Pyro.DataModel.Search;
using Hl7.Fhir.Model;
using Pyro.Common.BusinessEntities.Search;
using Pyro.Common.Interfaces;
using Pyro.Common.Interfaces.Repositories;
using Pyro.Common.Interfaces.UriSupport;
using Hl7.Fhir.Introspection;

namespace Pyro.DataModel.Repository
{
  public partial class LocationRepository : CommonRepository, IResourceRepository
  {
    public LocationRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    public IDatabaseOperationOutcome GetResourceBySearch(DtoSearchParameters DtoSearchParameters)
    {
      var Predicate = PredicateGenerator<Res_Location>(DtoSearchParameters);
      int TotalRecordCount = DbGetALLCount<Res_Location>(Predicate);
      var Query = DbGetAll<Res_Location>(Predicate);

      //Todo: Sort not implemented just defaulting to last update order
      Query = Query.OrderBy(x => x.lastUpdated);      
      int ClaculatedPageRequired = PaginationSupport.CalculatePageRequired(DtoSearchParameters.RequiredPageNumber, _NumberOfRecordsPerPage, TotalRecordCount);
      
      Query = Query.Paging(ClaculatedPageRequired, _NumberOfRecordsPerPage);
      var DtoResourceList = new List<Common.BusinessEntities.Dto.DtoResource>();
      Query.ToList().ForEach(x => DtoResourceList.Add(IndexSettingSupport.SetDtoResource(x)));

      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = false;
      DatabaseOperationOutcome.PagesTotal = PaginationSupport.CalculateTotalPages(_NumberOfRecordsPerPage, TotalRecordCount); ;
      DatabaseOperationOutcome.PageRequested = ClaculatedPageRequired;
      DatabaseOperationOutcome.ReturnedResourceCount = TotalRecordCount;
      DatabaseOperationOutcome.ReturnedResourceList = DtoResourceList;


      return DatabaseOperationOutcome;  
    }

    public IDatabaseOperationOutcome AddResource(Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Location;
      var ResourceEntity = new Res_Location();
      this.PopulateResourceEntity(ResourceEntity, "1", ResourceTyped, FhirRequestUri);
      this.DbAddEntity<Res_Location>(ResourceEntity);
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;     
      DatabaseOperationOutcome.ReturnedResource = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ReturnedResourceCount = 1;
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome UpdateResource(string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Location;
      var ResourceEntity = LoadCurrentResourceEntity(Resource.Id);
      var ResourceHistoryEntity = new Res_Location_History();  
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_Location_History_List.Add(ResourceHistoryEntity); 
      this.ResetResourceEntity(ResourceEntity);
      this.PopulateResourceEntity(ResourceEntity, ResourceVersion, ResourceTyped, FhirRequestUri);            
      this.Save();            
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      DatabaseOperationOutcome.ReturnedResource = IndexSettingSupport.SetDtoResource(ResourceEntity);
      DatabaseOperationOutcome.ReturnedResourceCount = 1;
      return DatabaseOperationOutcome;
    }

    public void UpdateResouceAsDeleted(string FhirResourceId, string ResourceVersion)
    {
      var ResourceEntity = this.LoadCurrentResourceEntity(FhirResourceId);
      var ResourceHistoryEntity = new Res_Location_History();
      IndexSettingSupport.SetHistoryResourceEntity(ResourceEntity, ResourceHistoryEntity);
      ResourceEntity.Res_Location_History_List.Add(ResourceHistoryEntity);
      this.ResetResourceEntity(ResourceEntity);
      ResourceEntity.IsDeleted = true;
      ResourceEntity.versionId = ResourceVersion;
      ResourceEntity.XmlBlob = string.Empty;
      this.Save();      
    }

    public IDatabaseOperationOutcome GetResourceByFhirIDAndVersionNumber(string FhirResourceId, string ResourceVersionNumber)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      var ResourceHistoryEntity = DbGet<Res_Location_History>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
      if (ResourceHistoryEntity != null)
      {
        DatabaseOperationOutcome.ReturnedResource = IndexSettingSupport.SetDtoResource(ResourceHistoryEntity);
      }
      else
      {
        var ResourceEntity = DbGet<Res_Location>(x => x.FhirId == FhirResourceId && x.versionId == ResourceVersionNumber);
        if (ResourceEntity != null)
          DatabaseOperationOutcome.ReturnedResource = IndexSettingSupport.SetDtoResource(ResourceEntity);        
      }
      return DatabaseOperationOutcome;
    }

    public IDatabaseOperationOutcome GetResourceByFhirID(string FhirResourceId, bool WithXml = false)
    {
      IDatabaseOperationOutcome DatabaseOperationOutcome = new DatabaseOperationOutcome();
      DatabaseOperationOutcome.SingleResourceRead = true;
      Pyro.Common.BusinessEntities.Dto.DtoResource DtoResource = null;
      if (WithXml)
      {        
        DtoResource = DbGetAll<Res_Location>(x => x.FhirId == FhirResourceId).Select(x => new Pyro.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated, Xml = x.XmlBlob }).SingleOrDefault();       
      }
      else
      {
        DtoResource = DbGetAll<Res_Location>(x => x.FhirId == FhirResourceId).Select(x => new Pyro.Common.BusinessEntities.Dto.DtoResource { FhirId = x.FhirId, IsDeleted = x.IsDeleted, IsCurrent = true, Version = x.versionId, Received = x.lastUpdated }).SingleOrDefault();        
      }
      DatabaseOperationOutcome.ReturnedResource = DtoResource;
      return DatabaseOperationOutcome;
    }

    private Res_Location LoadCurrentResourceEntity(string FhirId)
    {

      var IncludeList = new List<Expression<Func<Res_Location, object>>>();
      IncludeList.Add(x => x.address_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.name_List);
      IncludeList.Add(x => x.type_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<Res_Location>(x => x.FhirId == FhirId, IncludeList);

      return ResourceEntity;
    }


    private void ResetResourceEntity(Res_Location ResourceEntity)
    {
      ResourceEntity.address_city_String = null;      
      ResourceEntity.address_country_String = null;      
      ResourceEntity.address_postalcode_String = null;      
      ResourceEntity.address_state_String = null;      
      ResourceEntity.address_use_Code = null;      
      ResourceEntity.address_use_System = null;      
      ResourceEntity.name_String = null;      
      ResourceEntity.near_Code = null;      
      ResourceEntity.near_System = null;      
      ResourceEntity.near_distance_Comparator = null;      
      ResourceEntity.near_distance_Quantity = null;      
      ResourceEntity.near_distance_System = null;      
      ResourceEntity.near_distance_Code = null;      
      ResourceEntity.near_distance_Unit = null;      
      ResourceEntity.organization_VersionId = null;      
      ResourceEntity.organization_FhirId = null;      
      ResourceEntity.organization_Type = null;      
      ResourceEntity.organization_Url = null;      
      ResourceEntity.organization_ServiceRootURL_StoreID = null;      
      ResourceEntity.partof_VersionId = null;      
      ResourceEntity.partof_FhirId = null;      
      ResourceEntity.partof_Type = null;      
      ResourceEntity.partof_Url = null;      
      ResourceEntity.partof_ServiceRootURL_StoreID = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Location_Index_address.RemoveRange(ResourceEntity.address_List);            
      _Context.Res_Location_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_Location_Index_name.RemoveRange(ResourceEntity.name_List);            
      _Context.Res_Location_Index_type.RemoveRange(ResourceEntity.type_List);            
      _Context.Res_Location_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Location_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Location_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    private void PopulateResourceEntity(Res_Location ResourseEntity, string ResourceVersion, Location ResourceTyped, IDtoFhirRequestUri FhirRequestUri)
    {
       IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Address != null)
      {
        if (ResourceTyped.Address.City != null)
        {
          if (ResourceTyped.Address.CityElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Address.CityElement, Index) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.address_city_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.Address != null)
      {
        if (ResourceTyped.Address.Country != null)
        {
          if (ResourceTyped.Address.CountryElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Address.CountryElement, Index) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.address_country_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.Address != null)
      {
        if (ResourceTyped.Address.PostalCode != null)
        {
          if (ResourceTyped.Address.PostalCodeElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Address.PostalCodeElement, Index) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.address_postalcode_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.Address != null)
      {
        if (ResourceTyped.Address.State != null)
        {
          if (ResourceTyped.Address.StateElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Address.StateElement, Index) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.address_state_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.Address != null)
      {
        if (ResourceTyped.Address.Use != null)
        {
          if (ResourceTyped.Address.UseElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Address.AddressUse>)
          {
            var Index = new TokenIndex();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Address.UseElement, Index) as TokenIndex;
            if (Index != null)
            {
              ResourseEntity.address_use_Code = Index.Code;
              ResourseEntity.address_use_System = Index.System;
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
            ResourseEntity.name_String = Index.String;
          }
        }
      }

      if (ResourceTyped.Position != null)
      {
        if (ResourceTyped.Position is Hl7.Fhir.Model.Location.PositionComponent)
        {
          var Index = new TokenIndex();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Position, Index) as TokenIndex;
          if (Index != null)
          {
            ResourseEntity.near_Code = Index.Code;
            ResourseEntity.near_System = Index.System;
          }
        }
      }

      if (ResourceTyped.Position != null)
      {
        if (ResourceTyped.Position is Hl7.Fhir.Model.Location.PositionComponent)
        {
          var Index = new QuantityIndex();
          Index = IndexSetterFactory.Create(typeof(QuantityIndex)).Set(ResourceTyped.Position, Index) as QuantityIndex;
          if (Index != null)
          {
            ResourseEntity.near_distance_Comparator = Index.Comparator;
            ResourseEntity.near_distance_Quantity = Index.Quantity;
            ResourseEntity.near_distance_System = Index.System;
            ResourseEntity.near_distance_Code = Index.Code;
            ResourseEntity.near_distance_Unit = Index.Unit;
          }
        }
      }

      if (ResourceTyped.ManagingOrganization != null)
      {
        if (ResourceTyped.ManagingOrganization is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.ManagingOrganization, Index, FhirRequestUri, this) as ReferenceIndex;
          if (Index != null)
          {
            ResourseEntity.organization_Type = Index.Type;
            ResourseEntity.organization_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.organization_Url = Index.Url;
            }
            else
            {
              ResourseEntity.organization_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
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

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Location.LocationStatus>)
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

      if (ResourceTyped.Address != null)
      {
        var Index = new Res_Location_Index_address();
        Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Address, Index) as Res_Location_Index_address;
        ResourseEntity.address_List.Add(Index);
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_Location_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Location_Index_identifier;
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
            var Index = new Res_Location_Index_name();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(item3, Index) as Res_Location_Index_name;
            ResourseEntity.name_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Type != null)
      {
        foreach (var item3 in ResourceTyped.Type.Coding)
        {
          var Index = new Res_Location_Index_type();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Location_Index_type;
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
              var Index = new Res_Location_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Location_Index__profile;
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
              var Index = new Res_Location_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Location_Index__security;
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
              var Index = new Res_Location_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Location_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      

    }


  }
} 
