﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_CarePlan_Index_careteam : ReferenceIndex
  {
    public int Res_CarePlan_Index_careteamID {get; set;}
    public virtual Res_CarePlan Res_CarePlan { get; set; }
   
    public Res_CarePlan_Index_careteam()
    {
    }
  }
}
