﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Linkage_Index_item : ReferenceIndex
  {
    public int Res_Linkage_Index_itemID {get; set;}
    public virtual Res_Linkage Res_Linkage { get; set; }
   
    public Res_Linkage_Index_item()
    {
    }
  }
}

