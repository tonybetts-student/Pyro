﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Pyro.DataModel.DatabaseModel
{

  public class Res_DocumentReference_Index_setting : TokenIndex
  {
    public int Res_DocumentReference_Index_settingID {get; set;}
    public virtual Res_DocumentReference Res_DocumentReference { get; set; }
   
    public Res_DocumentReference_Index_setting()
    {
    }
  }
}
