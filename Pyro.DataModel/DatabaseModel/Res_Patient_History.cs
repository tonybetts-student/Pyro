﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_Patient_History : ResourceIndexBase
  {
    public int Res_Patient_HistoryID {get; set;}
    public virtual Res_Patient Res_Patient { get; set; }
   
    public Res_Patient_History()
    {
    }
  }
}
