﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_ExpansionProfile : ResourceIndexBase
  {
    public int Res_ExpansionProfileID {get; set;}
    public DateTimeOffset? date_DateTimeOffset {get; set;}
    public string description_String {get; set;}
    public string identifier_Code {get; set;}
    public string identifier_System {get; set;}
    public string name_String {get; set;}
    public string publisher_String {get; set;}
    public string status_Code {get; set;}
    public string status_System {get; set;}
    public string url_Uri {get; set;}
    public string version_Code {get; set;}
    public string version_System {get; set;}
    public ICollection<Res_ExpansionProfile_Index_meta_profile> meta_profile_List { get; set; }
    public ICollection<Res_ExpansionProfile_Index_meta_security> meta_security_List { get; set; }
    public ICollection<Res_ExpansionProfile_Index_meta_tag> meta_tag_List { get; set; }
    public ICollection<Res_ExpansionProfile_History> Res_ExpansionProfile_History_List { get; set; }
   
    public Res_ExpansionProfile()
    {
      this.meta_tag_List = new HashSet<Res_ExpansionProfile_Index_meta_tag>();
      this.meta_security_List = new HashSet<Res_ExpansionProfile_Index_meta_security>();
      this.meta_profile_List = new HashSet<Res_ExpansionProfile_Index_meta_profile>();
      this.Res_ExpansionProfile_History_List = new HashSet<Res_ExpansionProfile_History>();
    }
  }
}

