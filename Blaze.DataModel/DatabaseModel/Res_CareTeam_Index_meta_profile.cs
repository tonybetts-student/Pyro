﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_CareTeam_Index_meta_profile : UriIndex
  {
    public int Res_CareTeam_Index_meta_profileID {get; set;}
    public virtual Res_CareTeam Res_CareTeam { get; set; }
   
    public Res_CareTeam_Index_meta_profile()
    {
    }
  }
}

