﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_ProcedureRequest : ResourceIndexBase
  {
    public int Res_ProcedureRequestID {get; set;}
    public string encounter_VersionId {get; set;}
    public string encounter_FhirId {get; set;}
    public string encounter_Type {get; set;}
    public virtual Blaze_RootUrlStore encounter_Url { get; set; }
    public int? encounter_Url_Blaze_RootUrlStoreID { get; set; }
    public string orderer_VersionId {get; set;}
    public string orderer_FhirId {get; set;}
    public string orderer_Type {get; set;}
    public virtual Blaze_RootUrlStore orderer_Url { get; set; }
    public int? orderer_Url_Blaze_RootUrlStoreID { get; set; }
    public string patient_VersionId {get; set;}
    public string patient_FhirId {get; set;}
    public string patient_Type {get; set;}
    public virtual Blaze_RootUrlStore patient_Url { get; set; }
    public int? patient_Url_Blaze_RootUrlStoreID { get; set; }
    public string performer_VersionId {get; set;}
    public string performer_FhirId {get; set;}
    public string performer_Type {get; set;}
    public virtual Blaze_RootUrlStore performer_Url { get; set; }
    public int? performer_Url_Blaze_RootUrlStoreID { get; set; }
    public string subject_VersionId {get; set;}
    public string subject_FhirId {get; set;}
    public string subject_Type {get; set;}
    public virtual Blaze_RootUrlStore subject_Url { get; set; }
    public int? subject_Url_Blaze_RootUrlStoreID { get; set; }
    public ICollection<Res_ProcedureRequest_History> Res_ProcedureRequest_History_List { get; set; }
    public ICollection<Res_ProcedureRequest_Index_identifier> identifier_List { get; set; }
    public ICollection<Res_ProcedureRequest_Index_profile> profile_List { get; set; }
    public ICollection<Res_ProcedureRequest_Index_security> security_List { get; set; }
    public ICollection<Res_ProcedureRequest_Index_tag> tag_List { get; set; }
   
    public Res_ProcedureRequest()
    {
      this.identifier_List = new HashSet<Res_ProcedureRequest_Index_identifier>();
      this.profile_List = new HashSet<Res_ProcedureRequest_Index_profile>();
      this.security_List = new HashSet<Res_ProcedureRequest_Index_security>();
      this.tag_List = new HashSet<Res_ProcedureRequest_Index_tag>();
      this.Res_ProcedureRequest_History_List = new HashSet<Res_ProcedureRequest_History>();
    }
  }
}
