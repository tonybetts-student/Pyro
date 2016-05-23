﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Library : ResourceIndexBase
  {
    public int Res_LibraryID {get; set;}
    public string description_String {get; set;}
    public string status_Code {get; set;}
    public string status_System {get; set;}
    public string title_String {get; set;}
    public string version_String {get; set;}
    public ICollection<Res_Library_Index_meta_profile> meta_profile_List { get; set; }
    public ICollection<Res_Library_Index_meta_security> meta_security_List { get; set; }
    public ICollection<Res_Library_Index_meta_tag> meta_tag_List { get; set; }
    public ICollection<Res_Library_History> Res_Library_History_List { get; set; }
    public ICollection<Res_Library_Index_identifier> identifier_List { get; set; }
    public ICollection<Res_Library_Index_topic> topic_List { get; set; }
   
    public Res_Library()
    {
      this.identifier_List = new HashSet<Res_Library_Index_identifier>();
      this.topic_List = new HashSet<Res_Library_Index_topic>();
      this.meta_tag_List = new HashSet<Res_Library_Index_meta_tag>();
      this.meta_security_List = new HashSet<Res_Library_Index_meta_security>();
      this.meta_profile_List = new HashSet<Res_Library_Index_meta_profile>();
      this.Res_Library_History_List = new HashSet<Res_Library_History>();
    }
  }
}

