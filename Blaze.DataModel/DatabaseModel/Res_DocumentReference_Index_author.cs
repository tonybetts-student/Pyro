﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_DocumentReference_Index_author : ReferenceIndex
  {
    public int Res_DocumentReference_Index_authorID {get; set;}
    public virtual Res_DocumentReference Res_DocumentReference { get; set; }
   
    public Res_DocumentReference_Index_author()
    {
    }
  }
}

