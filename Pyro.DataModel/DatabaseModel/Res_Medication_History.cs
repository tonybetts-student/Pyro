﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_Medication_History : ResourceIndexBase
  {
    public int Res_Medication_HistoryID {get; set;}
    public virtual Res_Medication Res_Medication { get; set; }
   
    public Res_Medication_History()
    {
    }
  }
}
