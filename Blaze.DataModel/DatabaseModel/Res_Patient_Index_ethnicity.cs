﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Patient_Index_ethnicity : TokenIndex
  {
    public int Res_Patient_Index_ethnicityID {get; set;}
    public virtual Res_Patient Res_Patient { get; set; }
   
    public Res_Patient_Index_ethnicity()
    {
    }
  }
}

