﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Provenance : ResourceIndexBase
  {
    public int Res_ProvenanceID {get; set;}
    public DateTimeOffset? end_DateTimeOffset {get; set;}
    public string location_FhirId {get; set;}
    public string location_Type {get; set;}
    public virtual Blaze_RootUrlStore location_Url { get; set; }
    public int? location_Url_Blaze_RootUrlStoreID { get; set; }
    public DateTimeOffset? start_DateTimeOffset {get; set;}
    public ICollection<Res_Provenance_Index_meta_profile> meta_profile_List { get; set; }
    public ICollection<Res_Provenance_Index_meta_security> meta_security_List { get; set; }
    public ICollection<Res_Provenance_Index_meta_tag> meta_tag_List { get; set; }
    public ICollection<Res_Provenance_History> Res_Provenance_History_List { get; set; }
    public ICollection<Res_Provenance_Index_agent> agent_List { get; set; }
    public ICollection<Res_Provenance_Index_entity> entity_List { get; set; }
    public ICollection<Res_Provenance_Index_entity_type> entity_type_List { get; set; }
    public ICollection<Res_Provenance_Index_patient> patient_List { get; set; }
    public ICollection<Res_Provenance_Index_sig> sig_List { get; set; }
    public ICollection<Res_Provenance_Index_target> target_List { get; set; }
    public ICollection<Res_Provenance_Index_userid> userid_List { get; set; }
   
    public Res_Provenance()
    {
      this.agent_List = new HashSet<Res_Provenance_Index_agent>();
      this.entity_List = new HashSet<Res_Provenance_Index_entity>();
      this.entity_type_List = new HashSet<Res_Provenance_Index_entity_type>();
      this.patient_List = new HashSet<Res_Provenance_Index_patient>();
      this.sig_List = new HashSet<Res_Provenance_Index_sig>();
      this.target_List = new HashSet<Res_Provenance_Index_target>();
      this.userid_List = new HashSet<Res_Provenance_Index_userid>();
      this.meta_tag_List = new HashSet<Res_Provenance_Index_meta_tag>();
      this.meta_security_List = new HashSet<Res_Provenance_Index_meta_security>();
      this.meta_profile_List = new HashSet<Res_Provenance_Index_meta_profile>();
      this.Res_Provenance_History_List = new HashSet<Res_Provenance_History>();
    }
  }
}

