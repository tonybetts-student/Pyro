﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Order_Index_meta_profile : UriIndex
  {
    public int Res_Order_Index_meta_profileID {get; set;}
    public virtual Res_Order Res_Order { get; set; }
   
    public Res_Order_Index_meta_profile()
    {
    }
  }
}

