﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Questionnaire_Index_meta_profile : UriIndex
  {
    public int Res_Questionnaire_Index_meta_profileID {get; set;}
    public virtual Res_Questionnaire Res_Questionnaire { get; set; }
   
    public Res_Questionnaire_Index_meta_profile()
    {
    }
  }
}

