﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_MessageHeader_Index_data : ReferenceIndex
  {
    public int Res_MessageHeader_Index_dataID {get; set;}
    public virtual Res_MessageHeader Res_MessageHeader { get; set; }
   
    public Res_MessageHeader_Index_data()
    {
    }
  }
}

