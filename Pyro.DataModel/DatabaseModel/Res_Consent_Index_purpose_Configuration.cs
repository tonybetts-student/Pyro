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

namespace Pyro.DataModel.DatabaseModel
{
  public class Res_Consent_Index_purpose_Configuration : EntityTypeConfiguration<Res_Consent_Index_purpose>
  {

    public Res_Consent_Index_purpose_Configuration()
    {
      HasKey(x => x.Res_Consent_Index_purposeID).Property(x => x.Res_Consent_Index_purposeID).IsRequired();
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
      HasRequired(x => x.Res_Consent).WithMany(x => x.purpose_List).WillCascadeOnDelete(true);
    }
  }
}