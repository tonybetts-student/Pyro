﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_MedicationStatement_Index__profile : UriIndex
  {
    public int Res_MedicationStatement_Index__profileID {get; set;}
    public virtual Res_MedicationStatement Res_MedicationStatement { get; set; }
   
    public Res_MedicationStatement_Index__profile()
    {
    }
  }
}
