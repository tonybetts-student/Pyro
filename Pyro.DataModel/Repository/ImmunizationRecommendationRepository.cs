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
  public partial class ImmunizationRecommendationRepository<ResourceType, ResourceHistoryType> : CommonResourceRepository<ResourceType, ResourceHistoryType>, IResourceRepository 
    where ResourceType : Res_ImmunizationRecommendation, new() 
    where ResourceHistoryType :Res_ImmunizationRecommendation_History, new()
  {
    public ImmunizationRecommendationRepository(DataModel.DatabaseModel.DatabaseContext Context) : base(Context) { }

    protected override void AddResourceHistoryEntityToResourceEntity(ResourceType ResourceEntity, ResourceHistoryType ResourceHistoryEntity)
    {
      ResourceEntity.Res_ImmunizationRecommendation_History_List.Add(ResourceHistoryEntity);
    }
    
    protected override ResourceType LoadCurrentResourceEntity(string FhirId)
    {
      var IncludeList = new List<Expression<Func<ResourceType, object>>>();
         IncludeList.Add(x => x.date_List);
      IncludeList.Add(x => x.dose_number_List);
      IncludeList.Add(x => x.dose_sequence_List);
      IncludeList.Add(x => x.identifier_List);
      IncludeList.Add(x => x.information_List);
      IncludeList.Add(x => x.status_List);
      IncludeList.Add(x => x.support_List);
      IncludeList.Add(x => x.vaccine_type_List);
      IncludeList.Add(x => x._profile_List);
      IncludeList.Add(x => x._security_List);
      IncludeList.Add(x => x._tag_List);
    
      var ResourceEntity = DbQueryEntityWithInclude<ResourceType>(x => x.FhirId == FhirId, IncludeList);
      return ResourceEntity;
    }
    
    protected override void ResetResourceEntity(ResourceType ResourceEntity)
    {
      ResourceEntity.patient_VersionId = null;      
      ResourceEntity.patient_FhirId = null;      
      ResourceEntity.patient_Type = null;      
      ResourceEntity.patient_Url = null;      
      ResourceEntity.patient_ServiceRootURL_StoreID = null;      
      ResourceEntity.XmlBlob = null;      
 
      
      _Context.Res_ImmunizationRecommendation_Index_date.RemoveRange(ResourceEntity.date_List);            
      _Context.Res_ImmunizationRecommendation_Index_dose_number.RemoveRange(ResourceEntity.dose_number_List);            
      _Context.Res_ImmunizationRecommendation_Index_dose_sequence.RemoveRange(ResourceEntity.dose_sequence_List);            
      _Context.Res_ImmunizationRecommendation_Index_identifier.RemoveRange(ResourceEntity.identifier_List);            
      _Context.Res_ImmunizationRecommendation_Index_information.RemoveRange(ResourceEntity.information_List);            
      _Context.Res_ImmunizationRecommendation_Index_status.RemoveRange(ResourceEntity.status_List);            
      _Context.Res_ImmunizationRecommendation_Index_support.RemoveRange(ResourceEntity.support_List);            
      _Context.Res_ImmunizationRecommendation_Index_vaccine_type.RemoveRange(ResourceEntity.vaccine_type_List);            
      _Context.Res_ImmunizationRecommendation_Index__profile.RemoveRange(ResourceEntity._profile_List);            
      _Context.Res_ImmunizationRecommendation_Index__security.RemoveRange(ResourceEntity._security_List);            
      _Context.Res_ImmunizationRecommendation_Index__tag.RemoveRange(ResourceEntity._tag_List);            
 
    }

    protected override void PopulateResourceEntity(ResourceType ResourceEntity, string ResourceVersion, Resource Resource, IDtoFhirRequestUri FhirRequestUri)
    {
      var ResourceTyped = Resource as ImmunizationRecommendation;
      var ResourseEntity = ResourceEntity as ResourceType;
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

          if (ResourceTyped.Patient != null)
      {
        if (ResourceTyped.Patient is Hl7.Fhir.Model.ResourceReference)
        {
          var Index = new ReferenceIndex();
          Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(ResourceTyped.Patient, Index, FhirRequestUri, this) as ReferenceIndex;
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
              ResourseEntity.patient_ServiceRootURL_StoreID = Index.ServiceRootURL_StoreID;
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Recommendation)
      {
        if (item1.Date != null)
        {
          if (item1.DateElement is Hl7.Fhir.Model.FhirDateTime)
          {
            var Index = new Res_ImmunizationRecommendation_Index_date();
            Index = IndexSetterFactory.Create(typeof(DateTimeIndex)).Set(item1.DateElement, Index) as Res_ImmunizationRecommendation_Index_date;
            ResourseEntity.date_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Recommendation)
      {
        if (item1.DoseNumber != null)
        {
          if (item1.DoseNumberElement is Hl7.Fhir.Model.PositiveInt)
          {
            var Index = new Res_ImmunizationRecommendation_Index_dose_number();
            Index = IndexSetterFactory.Create(typeof(NumberIndex)).Set(item1.DoseNumberElement, Index) as Res_ImmunizationRecommendation_Index_dose_number;
            ResourseEntity.dose_number_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Recommendation)
      {
        if (item1.Protocol != null)
        {
          if (item1.Protocol.DoseSequence != null)
          {
            if (item1.Protocol.DoseSequenceElement is Hl7.Fhir.Model.PositiveInt)
            {
              var Index = new Res_ImmunizationRecommendation_Index_dose_sequence();
              Index = IndexSetterFactory.Create(typeof(NumberIndex)).Set(item1.Protocol.DoseSequenceElement, Index) as Res_ImmunizationRecommendation_Index_dose_sequence;
              ResourseEntity.dose_sequence_List.Add(Index);
            }
          }
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          if (item3 is Hl7.Fhir.Model.Identifier)
          {
            var Index = new Res_ImmunizationRecommendation_Index_identifier();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item3, Index) as Res_ImmunizationRecommendation_Index_identifier;
            ResourseEntity.identifier_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Recommendation)
      {
        if (item1.SupportingPatientInformation != null)
        {
          foreach (var item in item1.SupportingPatientInformation)
          {
            if (item is ResourceReference)
            {
              var Index = new Res_ImmunizationRecommendation_Index_information();
              Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_ImmunizationRecommendation_Index_information;
              if (Index != null)
              {
                ResourseEntity.information_List.Add(Index);
              }
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Recommendation)
      {
        if (item1.ForecastStatus != null)
        {
          foreach (var item4 in item1.ForecastStatus.Coding)
          {
            var Index = new Res_ImmunizationRecommendation_Index_status();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_ImmunizationRecommendation_Index_status;
            ResourseEntity.status_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Recommendation)
      {
        if (item1.SupportingImmunization != null)
        {
          foreach (var item in item1.SupportingImmunization)
          {
            if (item is ResourceReference)
            {
              var Index = new Res_ImmunizationRecommendation_Index_support();
              Index = IndexSetterFactory.Create(typeof(ReferenceIndex)).Set(item, Index, FhirRequestUri, this) as Res_ImmunizationRecommendation_Index_support;
              if (Index != null)
              {
                ResourseEntity.support_List.Add(Index);
              }
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Recommendation)
      {
        if (item1.VaccineCode != null)
        {
          foreach (var item4 in item1.VaccineCode.Coding)
          {
            var Index = new Res_ImmunizationRecommendation_Index_vaccine_type();
            Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_ImmunizationRecommendation_Index_vaccine_type;
            ResourseEntity.vaccine_type_List.Add(Index);
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
              var Index = new Res_ImmunizationRecommendation_Index__profile();
              Index = IndexSetterFactory.Create(typeof(UriIndex)).Set(item4, Index) as Res_ImmunizationRecommendation_Index__profile;
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
              var Index = new Res_ImmunizationRecommendation_Index__security();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_ImmunizationRecommendation_Index__security;
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
              var Index = new Res_ImmunizationRecommendation_Index__tag();
              Index = IndexSetterFactory.Create(typeof(TokenIndex)).Set(item4, Index) as Res_ImmunizationRecommendation_Index__tag;
              ResourseEntity._tag_List.Add(Index);
            }
          }
        }
      }


      
    }

  }
} 

