﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_DeviceUseRequest_Index_based_on : ReferenceIndex
  {
    public int Res_DeviceUseRequest_Index_based_onID {get; set;}
    public virtual Res_DeviceUseRequest Res_DeviceUseRequest { get; set; }
   
    public Res_DeviceUseRequest_Index_based_on()
    {
    }
  }
}
