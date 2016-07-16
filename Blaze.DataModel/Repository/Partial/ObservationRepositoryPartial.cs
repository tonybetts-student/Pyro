﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
  public partial class ObservationRepository : CommonRepository, IResourceRepository
  {

    private void PopulateResourceEntity(Res_Observation ResourseEntity, string ResourceVersion, Observation ResourceTyped, IDtoFhirRequestUri FhirRequestUri)
    {
      IndexSettingSupport.SetResourceBaseAddOrUpdate(ResourceTyped, ResourseEntity, ResourceVersion, false);

      if (ResourceTyped.Effective != null)
      {
        if (ResourceTyped.Effective is FhirDateTime)
        {
          var Index = Blaze.DataModel.IndexSetter.IndexSetterFactory.Create(typeof(DateIndex)).Set(ResourceTyped.Effective) as DateIndex;
          //var Index = IndexSettingSupport.SetIndex<DateIndex>(new DateIndex(), ResourceTyped.Effective);
          if (Index != null)
          {
            ResourseEntity.date_DateTimeOffset = Index.DateTimeOffset;
          }
        }
      }

      if (ResourceTyped.Device != null)
      {
        {
          var Index = IndexSettingSupport.SetIndex<ReferenceIndex>(new ReferenceIndex(), ResourceTyped.Device, FhirRequestUri, this);
          if (Index != null)
          {
            ResourseEntity.device_Type = Index.Type;
            ResourseEntity.device_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.device_Url = Index.Url;
            }
            else
            {
              ResourseEntity.device_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Encounter != null)
      {
        {
          var Index = IndexSettingSupport.SetIndex<ReferenceIndex>(new ReferenceIndex(), ResourceTyped.Encounter, FhirRequestUri, this);
          if (Index != null)
          {
            ResourseEntity.encounter_Type = Index.Type;
            ResourseEntity.encounter_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.encounter_Url = Index.Url;
            }
            else
            {
              ResourseEntity.encounter_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Subject != null)
      {
        {
          var Index = IndexSettingSupport.SetIndex<ReferenceIndex>(new ReferenceIndex(), ResourceTyped.Subject, FhirRequestUri, this);
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

      if (ResourceTyped.Specimen != null)
      {
        {
          var Index = IndexSettingSupport.SetIndex<ReferenceIndex>(new ReferenceIndex(), ResourceTyped.Specimen, FhirRequestUri, this);
          if (Index != null)
          {
            ResourseEntity.specimen_Type = Index.Type;
            ResourseEntity.specimen_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.specimen_Url = Index.Url;
            }
            else
            {
              ResourseEntity.specimen_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Status != null)
      {
        var Index = IndexSettingSupport.SetIndex<TokenIndex>(new TokenIndex(), ResourceTyped.StatusElement);
        if (Index != null)
        {
          ResourseEntity.status_Code = Index.Code;
          ResourseEntity.status_System = Index.System;
        }
      }

      if (ResourceTyped.Subject != null)
      {
        {
          var Index = IndexSettingSupport.SetIndex<ReferenceIndex>(new ReferenceIndex(), ResourceTyped.Subject, FhirRequestUri, this);
          if (Index != null)
          {
            ResourseEntity.subject_Type = Index.Type;
            ResourseEntity.subject_FhirId = Index.FhirId;
            if (Index.Url != null)
            {
              ResourseEntity.subject_Url = Index.Url;
            }
            else
            {
              ResourseEntity.subject_Url_Blaze_RootUrlStoreID = Index.Url_Blaze_RootUrlStoreID;
            }
          }
        }
      }

      if (ResourceTyped.Value != null)
      {
        var Index = IndexSettingSupport.SetIndex<DateIndex>(new DateIndex(), ResourceTyped.Value);
        if (Index != null)
        {
          ResourseEntity.value_date_DateTimeOffset = Index.DateTimeOffset;
        }
      }

      if (ResourceTyped.Value != null)
      {
        var Index = IndexSettingSupport.SetIndex<QuantityIndex>(new QuantityIndex(), ResourceTyped.Value);
        if (Index != null)
        {
          ResourseEntity.value_quantity_Code = Index.Code;
          ResourseEntity.value_quantity_System = Index.System;
          ResourseEntity.value_quantity_Quantity = Index.Quantity;
        }
      }

      if (ResourceTyped.Value != null)
      {
        var Index = IndexSettingSupport.SetIndex<StringIndex>(new StringIndex(), ResourceTyped.Value);
        if (Index != null)
        {
          ResourseEntity.value_string_String = Index.String;
        }
      }

      if (ResourceTyped.Category != null)
      {
        foreach (var item3 in ResourceTyped.Category.Coding)
        {
          var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_category(), item3) as Res_Observation_Index_category;
          ResourseEntity.category_List.Add(Index);
        }
      }

      if (ResourceTyped.Code != null)
      {
        foreach (var item3 in ResourceTyped.Code.Coding)
        {
          var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_code(), item3) as Res_Observation_Index_code;
          ResourseEntity.code_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.Code != null)
        {
          foreach (var item4 in item1.Code.Coding)
          {
            var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_component_code(), item4) as Res_Observation_Index_component_code;
            ResourseEntity.component_code_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.DataAbsentReason != null)
        {
          foreach (var item4 in item1.DataAbsentReason.Coding)
          {
            var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_component_data_absent_reason(), item4) as Res_Observation_Index_component_data_absent_reason;
            ResourseEntity.component_data_absent_reason_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.Value != null)
        {
          if (item1.Value is CodeableConcept)
          {
            CodeableConcept CodeableConcept = item1.Value as CodeableConcept;
            foreach (var item4 in CodeableConcept.Coding)
            {
              var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_component_value_concept(), item4) as Res_Observation_Index_component_value_concept;
              ResourseEntity.component_value_concept_List.Add(Index);
            }
          }
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.Value != null)
        {
          var Index = IndexSettingSupport.SetIndex<QuantityIndex>(new Res_Observation_Index_component_value_quantity(), item1.Value) as Res_Observation_Index_component_value_quantity;
          ResourseEntity.component_value_quantity_List.Add(Index);
        }
      }

      foreach (var item1 in ResourceTyped.Component)
      {
        if (item1.Value != null)
        {
          var Index = IndexSettingSupport.SetIndex<StringIndex>(new Res_Observation_Index_component_value_string(), item1.Value) as Res_Observation_Index_component_value_string;
          ResourseEntity.component_value_string_List.Add(Index);
        }
      }

      if (ResourceTyped.DataAbsentReason != null)
      {
        foreach (var item3 in ResourceTyped.DataAbsentReason.Coding)
        {
          var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_data_absent_reason(), item3) as Res_Observation_Index_data_absent_reason;
          ResourseEntity.data_absent_reason_List.Add(Index);
        }
      }

      if (ResourceTyped.Identifier != null)
      {
        foreach (var item3 in ResourceTyped.Identifier)
        {
          var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_identifier(), item3) as Res_Observation_Index_identifier;
          ResourseEntity.identifier_List.Add(Index);
        }
      }

      if (ResourceTyped.Performer != null)
      {
        foreach (var item in ResourceTyped.Performer)
        {
          var Index = IndexSettingSupport.SetIndex<ReferenceIndex>(new Res_Observation_Index_performer(), item, FhirRequestUri, this) as Res_Observation_Index_performer;
          if (Index != null)
          {
            ResourseEntity.performer_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Related)
      {
        if (item1.Target != null)
        {
          var Index = IndexSettingSupport.SetIndex<ReferenceIndex>(new Res_Observation_Index_related_target(), item1.Target, FhirRequestUri, this) as Res_Observation_Index_related_target;
          if (Index != null)
          {
            ResourseEntity.related_target_List.Add(Index);
          }
        }
      }

      foreach (var item1 in ResourceTyped.Related)
      {
        if (item1.Type != null)
        {
          var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_related_type(), item1.TypeElement) as Res_Observation_Index_related_type;
          ResourseEntity.related_type_List.Add(Index);
        }
      }

      if (ResourceTyped.Value != null)
      {
        if (ResourceTyped.Value is CodeableConcept)
        {
          CodeableConcept CodeableConcept = ResourceTyped.Value as CodeableConcept;
          foreach (var item3 in CodeableConcept.Coding)
          {
            var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_value_concept(), item3) as Res_Observation_Index_value_concept;
            ResourseEntity.value_concept_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Profile != null)
        {
          foreach (var item4 in ResourceTyped.Meta.ProfileElement)
          {
            var Index = IndexSettingSupport.SetIndex<UriIndex>(new Res_Observation_Index_profile(), item4) as Res_Observation_Index_profile;
            ResourseEntity.profile_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Security != null)
        {
          foreach (var item4 in ResourceTyped.Meta.Security)
          {
            var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_security(), item4) as Res_Observation_Index_security;
            ResourseEntity.security_List.Add(Index);
          }
        }
      }

      if (ResourceTyped.Meta != null)
      {
        if (ResourceTyped.Meta.Tag != null)
        {
          foreach (var item4 in ResourceTyped.Meta.Tag)
          {
            var Index = IndexSettingSupport.SetIndex<TokenIndex>(new Res_Observation_Index_tag(), item4) as Res_Observation_Index_tag;
            ResourseEntity.tag_List.Add(Index);
          }
        }
      }




    }

  }
}
