﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_NamingSystem_Index_value : StringIndex
  {
    public int Res_NamingSystem_Index_valueID {get; set;}
    public virtual Res_NamingSystem Res_NamingSystem { get; set; }
   
    public Res_NamingSystem_Index_value()
    {
    }
  }
}

