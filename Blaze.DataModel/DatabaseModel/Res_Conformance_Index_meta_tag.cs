﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Conformance_Index_meta_tag : TokenIndex
  {
    public int Res_Conformance_Index_meta_tagID {get; set;}
    public virtual Res_Conformance Res_Conformance { get; set; }
   
    public Res_Conformance_Index_meta_tag()
    {
    }
  }
}

