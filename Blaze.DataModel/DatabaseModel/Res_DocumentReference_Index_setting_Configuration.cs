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
  public class Res_DocumentReference_Index_setting_Configuration : EntityTypeConfiguration<Res_DocumentReference_Index_setting>
  {

    public Res_DocumentReference_Index_setting_Configuration()
    {
      HasKey(x => x.Res_DocumentReference_Index_settingID).Property(x => x.Res_DocumentReference_Index_settingID).IsRequired();
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
      HasRequired(x => x.Res_DocumentReference).WithMany(x => x.setting_List).WillCascadeOnDelete(true);
    }
  }
}
