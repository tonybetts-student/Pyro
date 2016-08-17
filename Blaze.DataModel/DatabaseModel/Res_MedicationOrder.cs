﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_MedicationOrder : ResourceIndexBase
  {
    public int Res_MedicationOrderID {get; set;}
    public DateTimeOffset? datewritten_DateTimeOffset {get; set;}
    public string encounter_VersionId {get; set;}
    public string encounter_FhirId {get; set;}
    public string encounter_Type {get; set;}
    public virtual ServiceRootURL_Store encounter_Url { get; set; }
    public int? encounter_ServiceRootURL_StoreID { get; set; }
    public string medication_VersionId {get; set;}
    public string medication_FhirId {get; set;}
    public string medication_Type {get; set;}
    public virtual ServiceRootURL_Store medication_Url { get; set; }
    public int? medication_ServiceRootURL_StoreID { get; set; }
    public string patient_VersionId {get; set;}
    public string patient_FhirId {get; set;}
    public string patient_Type {get; set;}
    public virtual ServiceRootURL_Store patient_Url { get; set; }
    public int? patient_ServiceRootURL_StoreID { get; set; }
    public string prescriber_VersionId {get; set;}
    public string prescriber_FhirId {get; set;}
    public string prescriber_Type {get; set;}
    public virtual ServiceRootURL_Store prescriber_Url { get; set; }
    public int? prescriber_ServiceRootURL_StoreID { get; set; }
    public string status_Code {get; set;}
    public string status_System {get; set;}
    public ICollection<Res_MedicationOrder_History> Res_MedicationOrder_History_List { get; set; }
    public ICollection<Res_MedicationOrder_Index_code> code_List { get; set; }
    public ICollection<Res_MedicationOrder_Index_identifier> identifier_List { get; set; }
    public ICollection<Res_MedicationOrder_Index_profile> profile_List { get; set; }
    public ICollection<Res_MedicationOrder_Index_security> security_List { get; set; }
    public ICollection<Res_MedicationOrder_Index_tag> tag_List { get; set; }
   
    public Res_MedicationOrder()
    {
      this.code_List = new HashSet<Res_MedicationOrder_Index_code>();
      this.identifier_List = new HashSet<Res_MedicationOrder_Index_identifier>();
      this.profile_List = new HashSet<Res_MedicationOrder_Index_profile>();
      this.security_List = new HashSet<Res_MedicationOrder_Index_security>();
      this.tag_List = new HashSet<Res_MedicationOrder_Index_tag>();
      this.Res_MedicationOrder_History_List = new HashSet<Res_MedicationOrder_History>();
    }
  }
}

