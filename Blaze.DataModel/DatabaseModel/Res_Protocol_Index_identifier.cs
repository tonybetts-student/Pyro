﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Protocol_Index_identifier : TokenIndex
  {
    public int Res_Protocol_Index_identifierID {get; set;}
    public virtual Res_Protocol Res_Protocol { get; set; }
   
    public Res_Protocol_Index_identifier()
    {
    }
  }
}

