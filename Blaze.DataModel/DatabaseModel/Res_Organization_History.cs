﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Organization_History : ResourceIndexBase
  {
    public int Res_Organization_HistoryID {get; set;}
    public virtual Res_Organization Res_Organization { get; set; }
   
    public Res_Organization_History()
    {
    }
  }
}

