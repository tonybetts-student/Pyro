﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_ValueSet_History : ResourceIndexBase
  {
    public int Res_ValueSet_HistoryID {get; set;}
    public virtual Res_ValueSet Res_ValueSet { get; set; }
   
    public Res_ValueSet_History()
    {
    }
  }
}
