﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_HealthcareService_Index_servicetype : TokenIndex
  {
    public int Res_HealthcareService_Index_servicetypeID {get; set;}
    public virtual Res_HealthcareService Res_HealthcareService { get; set; }
   
    public Res_HealthcareService_Index_servicetype()
    {
    }
  }
}
