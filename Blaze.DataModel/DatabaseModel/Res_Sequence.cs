﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.DataModel.DatabaseModel.Base;

//This source file has been auto generated.

namespace Blaze.DataModel.DatabaseModel
{

  public class Res_Sequence : ResourceIndexBase
  {
    public int Res_SequenceID {get; set;}
    public decimal? end_Number {get; set;}
    public string patient_FhirId {get; set;}
    public string patient_Type {get; set;}
    public virtual Blaze_RootUrlStore patient_Url { get; set; }
    public int? patient_Url_Blaze_RootUrlStoreID { get; set; }
    public decimal? start_Number {get; set;}
    public string type_Code {get; set;}
    public string type_System {get; set;}
    public ICollection<Res_Sequence_Index_meta_profile> meta_profile_List { get; set; }
    public ICollection<Res_Sequence_Index_meta_security> meta_security_List { get; set; }
    public ICollection<Res_Sequence_Index_meta_tag> meta_tag_List { get; set; }
    public ICollection<Res_Sequence_History> Res_Sequence_History_List { get; set; }
    public ICollection<Res_Sequence_Index_chromosome> chromosome_List { get; set; }
    public ICollection<Res_Sequence_Index_species> species_List { get; set; }
   
    public Res_Sequence()
    {
      this.chromosome_List = new HashSet<Res_Sequence_Index_chromosome>();
      this.species_List = new HashSet<Res_Sequence_Index_species>();
      this.meta_tag_List = new HashSet<Res_Sequence_Index_meta_tag>();
      this.meta_security_List = new HashSet<Res_Sequence_Index_meta_security>();
      this.meta_profile_List = new HashSet<Res_Sequence_Index_meta_profile>();
      this.Res_Sequence_History_List = new HashSet<Res_Sequence_History>();
    }
  }
}

