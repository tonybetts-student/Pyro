﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_StructureMap_History : ResourceIndexBase
  {
    public int Res_StructureMap_HistoryID {get; set;}
    public virtual Res_StructureMap Res_StructureMap { get; set; }
   
    public Res_StructureMap_History()
    {
    }
  }
}

