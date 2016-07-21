﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure.Annotations;

//This is an Auto generated file do not change it's contents!!.

namespace Blaze.DataModel.DatabaseModel
{
  public class Res_QuestionnaireResponse_Configuration : EntityTypeConfiguration<Res_QuestionnaireResponse>
  {

    public Res_QuestionnaireResponse_Configuration()
    {
      HasKey(x => x.Res_QuestionnaireResponseID).Property(x => x.Res_QuestionnaireResponseID).IsRequired();
      Property(x => x.IsDeleted).IsRequired();
      Property(x => x.FhirId).IsRequired().HasMaxLength(500).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_FhirId") { IsUnique = true }));
      Property(x => x.lastUpdated).IsRequired();
      Property(x => x.versionId).IsRequired();
      Property(x => x.XmlBlob).IsRequired();
      Property(x => x.author_VersionId).IsOptional();
      Property(x => x.author_FhirId).IsOptional();
      Property(x => x.author_Type).IsOptional();
      HasOptional(x => x.author_Url);
      HasOptional<Blaze_RootUrlStore>(x => x.author_Url).WithMany().HasForeignKey(x => x.author_Url_Blaze_RootUrlStoreID);
      Property(x => x.authored_DateTimeOffset).IsOptional();
      Property(x => x.encounter_VersionId).IsOptional();
      Property(x => x.encounter_FhirId).IsOptional();
      Property(x => x.encounter_Type).IsOptional();
      HasOptional(x => x.encounter_Url);
      HasOptional<Blaze_RootUrlStore>(x => x.encounter_Url).WithMany().HasForeignKey(x => x.encounter_Url_Blaze_RootUrlStoreID);
      Property(x => x.patient_VersionId).IsOptional();
      Property(x => x.patient_FhirId).IsOptional();
      Property(x => x.patient_Type).IsOptional();
      HasOptional(x => x.patient_Url);
      HasOptional<Blaze_RootUrlStore>(x => x.patient_Url).WithMany().HasForeignKey(x => x.patient_Url_Blaze_RootUrlStoreID);
      Property(x => x.questionnaire_VersionId).IsOptional();
      Property(x => x.questionnaire_FhirId).IsOptional();
      Property(x => x.questionnaire_Type).IsOptional();
      HasOptional(x => x.questionnaire_Url);
      HasOptional<Blaze_RootUrlStore>(x => x.questionnaire_Url).WithMany().HasForeignKey(x => x.questionnaire_Url_Blaze_RootUrlStoreID);
      Property(x => x.source_VersionId).IsOptional();
      Property(x => x.source_FhirId).IsOptional();
      Property(x => x.source_Type).IsOptional();
      HasOptional(x => x.source_Url);
      HasOptional<Blaze_RootUrlStore>(x => x.source_Url).WithMany().HasForeignKey(x => x.source_Url_Blaze_RootUrlStoreID);
      Property(x => x.status_Code).IsOptional();
      Property(x => x.status_System).IsOptional();
      Property(x => x.subject_VersionId).IsOptional();
      Property(x => x.subject_FhirId).IsOptional();
      Property(x => x.subject_Type).IsOptional();
      HasOptional(x => x.subject_Url);
      HasOptional<Blaze_RootUrlStore>(x => x.subject_Url).WithMany().HasForeignKey(x => x.subject_Url_Blaze_RootUrlStoreID);
    }
  }
}