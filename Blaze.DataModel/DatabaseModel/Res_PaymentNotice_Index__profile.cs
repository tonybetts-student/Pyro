﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_PaymentNotice_Index__profile : UriIndex
  {
    public int Res_PaymentNotice_Index__profileID {get; set;}
    public virtual Res_PaymentNotice Res_PaymentNotice { get; set; }
   
    public Res_PaymentNotice_Index__profile()
    {
    }
  }
}
