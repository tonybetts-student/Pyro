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
  public class Res_Appointment_Index_service_type_Configuration : EntityTypeConfiguration<Res_Appointment_Index_service_type>
  {

    public Res_Appointment_Index_service_type_Configuration()
    {
      HasKey(x => x.Res_Appointment_Index_service_typeID).Property(x => x.Res_Appointment_Index_service_typeID).IsRequired();
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
      HasRequired(x => x.Res_Appointment).WithMany(x => x.service_type_List).WillCascadeOnDelete(true);
    }
  }
}
