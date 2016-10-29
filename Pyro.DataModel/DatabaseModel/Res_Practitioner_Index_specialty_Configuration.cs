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
  public class Res_Practitioner_Index_specialty_Configuration : EntityTypeConfiguration<Res_Practitioner_Index_specialty>
  {

    public Res_Practitioner_Index_specialty_Configuration()
    {
      HasKey(x => x.Res_Practitioner_Index_specialtyID).Property(x => x.Res_Practitioner_Index_specialtyID).IsRequired();
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
      HasRequired(x => x.Res_Practitioner).WithMany(x => x.specialty_List).WillCascadeOnDelete(true);
    }
  }
}