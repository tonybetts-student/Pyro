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
  public partial class SubscriptionRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_Subscription, new() 
    where ResourceHistoryType :Res_Subscription_History, new()
  {
    public SubscriptionRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_Subscription_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.contact_List);
      IncludeList.Add(x => x.tag_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.criteria_String = null;      
      ResourceEntity.payload_String = null;      
      ResourceEntity.status_Code = null;      
      ResourceEntity.status_System = null;      
      ResourceEntity.type_Code = null;      
      ResourceEntity.type_System = null;      
      ResourceEntity.url_Uri = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_Subscription_Index_contact.RemoveRange(ResourceEntity.contact_List);            
      _Context.Res_Subscription_Index_tag.RemoveRange(ResourceEntity.tag_List);            
      _Context.Res_Subscription_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_Subscription_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_Subscription_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as Subscription;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Criteria != null)
      {
        if (ResourceTyped.CriteriaElement is Hl7.Fhir.Model.FhirString)
        {
          var Index = new StringIndex();
          Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.CriteriaElement, Index) as StringIndex;
          if (Index != null)
          {
            ResourseEntity.criteria_String = Index.String;
          }
        }
      }

      if (ResourceTyped.Channel != null)
      {
        if (ResourceTyped.Channel.Payload != null)
        {
          if (ResourceTyped.Channel.PayloadElement is Hl7.Fhir.Model.FhirString)
          {
            var Index = new StringIndex();
            Index = IndexSetterFactory.Create(typeof(StringIndex)).Set(ResourceTyped.Channel.PayloadElement, Index) as StringIndex;
            if (Index != null)
            {
              ResourseEntity.payload_String = Index.String;
            }
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        if (ResourceTyped.StatusElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Subscription.SubscriptionStatus>)
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

      if (ResourceTyped.Channel != null)
      {
        if (ResourceTyped.Channel.Type != null)
        {
          if (ResourceTyped.Channel.TypeElement is Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Subscription.SubscriptionChannelType>)
          {
            var Index = new TokenIndex();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(ResourceTyped.Channel.TypeElement, Index) as TokenIndex;
            if (Index != null)
            {
              ResourseEntity.type_Code = Index.Code;
              ResourseEntity.type_System = Index.System;
            }
          }
        }
      }

      if (ResourceTyped.Channel != null)
      {
        if (ResourceTyped.Channel.Endpoint != null)
        {
          if (ResourceTyped.Channel.EndpointElement is Hl7.Fhir.Model.FhirUri)
          {
            var Index = new UriIndex();
            Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(ResourceTyped.Channel.EndpointElement, Index) as UriIndex;
            if (Index != null)
            {
              ResourseEntity.url_Uri = Index.Uri;
            }
          }
        }
      }

      foreach (var item2 in ResourceTyped.Contact)
      {
        if (item2 is ContactPoint)
        {
          var Index = new Res_Subscription_Index_contact();
          Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item2, Index) as Res_Subscription_Index_contact;
          ResourseEntity.contact_List.Add(Index);
        }
      }

      if (ResourceTyped.Tag != null)
      {
        foreach (var item3 in ResourceTyped.Tag)
        {
          if (item3 is Hl7.Fhir.Model.Coding)
          {
            var Index = new Res_Subscription_Index_tag();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_Subscription_Index_tag;
            ResourseEntity.tag_List.Add(Index);
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
              var Index = new Res_Subscription_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_Subscription_Index__profile;
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
              var Index = new Res_Subscription_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Subscription_Index__security;
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
              var Index = new Res_Subscription_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_Subscription_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

