﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_DeviceComponent_History : ResourceIndexBase
  {
    public int Res_DeviceComponent_HistoryID {get; set;}
    public virtual Res_DeviceComponent Res_DeviceComponent { get; set; }
   
    public Res_DeviceComponent_History()
    {
    }
  }
}

