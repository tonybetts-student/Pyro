﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_RiskAssessment_Index_meta_security : TokenIndex
  {
    public int Res_RiskAssessment_Index_meta_securityID {get; set;}
    public virtual Res_RiskAssessment Res_RiskAssessment { get; set; }
   
    public Res_RiskAssessment_Index_meta_security()
    {
    }
  }
}

