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
  public class Res_Provenance_Index_patient_Configuration : EntityTypeConfiguration<Res_Provenance_Index_patient>
  {

    public Res_Provenance_Index_patient_Configuration()
    {
      HasKey(x => x.Res_Provenance_Index_patientID).Property(x => x.Res_Provenance_Index_patientID).IsRequired();
      Property(x => x.VersionId).IsOptional();
      Property(x => x.FhirId).IsRequired();
      Property(x => x.Type).IsRequired();
      HasRequired(x => x.Url);
      HasRequired<ServiceRootURL_Store>(x => x.Url).WithMany().HasForeignKey(x => x.ServiceRootURL_StoreID);
      HasRequired(x => x.Res_Provenance).WithMany(x => x.patient_List).WillCascadeOnDelete(true);
    }
  }
}
