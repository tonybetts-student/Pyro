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
  public class Res_DeviceMetric_Index_meta_profile_Configuration : EntityTypeConfiguration<Res_DeviceMetric_Index_meta_profile>
  {

    public Res_DeviceMetric_Index_meta_profile_Configuration()
    {
      HasKey(x => x.Res_DeviceMetric_Index_meta_profileID).Property(x => x.Res_DeviceMetric_Index_meta_profileID).IsRequired();
      HasRequired(x => x.Res_DeviceMetric).WithMany(x => x.meta_profile_List).WillCascadeOnDelete(true);
      Property(x => x.Uri).IsRequired();
    }
  }
}
