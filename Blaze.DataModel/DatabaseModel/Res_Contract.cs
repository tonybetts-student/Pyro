﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Contract : ResourceIndexBase
  {
    public int Res_ContractID {get; set;}
    public string identifier_Code {get; set;}
    public string identifier_System {get; set;}
    public DateTimeOffset? issued_DateTimeOffset {get; set;}
    public ICollection<Res_Contract_Index_meta_profile> meta_profile_List { get; set; }
    public ICollection<Res_Contract_Index_meta_security> meta_security_List { get; set; }
    public ICollection<Res_Contract_Index_meta_tag> meta_tag_List { get; set; }
    public ICollection<Res_Contract_History> Res_Contract_History_List { get; set; }
    public ICollection<Res_Contract_Index_agent> agent_List { get; set; }
    public ICollection<Res_Contract_Index_authority> authority_List { get; set; }
    public ICollection<Res_Contract_Index_domain> domain_List { get; set; }
    public ICollection<Res_Contract_Index_patient> patient_List { get; set; }
    public ICollection<Res_Contract_Index_signer> signer_List { get; set; }
    public ICollection<Res_Contract_Index_subject> subject_List { get; set; }
    public ICollection<Res_Contract_Index_topic> topic_List { get; set; }
    public ICollection<Res_Contract_Index_ttopic> ttopic_List { get; set; }
   
    public Res_Contract()
    {
      this.agent_List = new HashSet<Res_Contract_Index_agent>();
      this.authority_List = new HashSet<Res_Contract_Index_authority>();
      this.domain_List = new HashSet<Res_Contract_Index_domain>();
      this.patient_List = new HashSet<Res_Contract_Index_patient>();
      this.signer_List = new HashSet<Res_Contract_Index_signer>();
      this.subject_List = new HashSet<Res_Contract_Index_subject>();
      this.topic_List = new HashSet<Res_Contract_Index_topic>();
      this.ttopic_List = new HashSet<Res_Contract_Index_ttopic>();
      this.meta_tag_List = new HashSet<Res_Contract_Index_meta_tag>();
      this.meta_security_List = new HashSet<Res_Contract_Index_meta_security>();
      this.meta_profile_List = new HashSet<Res_Contract_Index_meta_profile>();
      this.Res_Contract_History_List = new HashSet<Res_Contract_History>();
    }
  }
}

