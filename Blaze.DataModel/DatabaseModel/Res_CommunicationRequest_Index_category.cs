﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_CommunicationRequest_Index_category : TokenIndex
  {
    public int Res_CommunicationRequest_Index_categoryID {get; set;}
    public virtual Res_CommunicationRequest Res_CommunicationRequest { get; set; }
   
    public Res_CommunicationRequest_Index_category()
    {
    }
  }
}

