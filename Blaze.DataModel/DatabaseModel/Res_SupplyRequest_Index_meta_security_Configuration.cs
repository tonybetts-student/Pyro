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
  public class Res_SupplyRequest_Index_meta_security_Configuration : EntityTypeConfiguration<Res_SupplyRequest_Index_meta_security>
  {

    public Res_SupplyRequest_Index_meta_security_Configuration()
    {
      HasKey(x => x.Res_SupplyRequest_Index_meta_securityID).Property(x => x.Res_SupplyRequest_Index_meta_securityID).IsRequired();
      HasRequired(x => x.Res_SupplyRequest).WithMany(x => x.meta_security_List).WillCascadeOnDelete(true);
      Property(x => x.Code).IsRequired();
      Property(x => x.System).IsOptional();
    }
  }
}
