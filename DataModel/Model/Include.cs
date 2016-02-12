﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Model
{
  public class Include
  {
    public int Id { get; set; }
    public string System { get; set; }

    //Keyed
    public virtual Compose Compose { get; set; }
  }

  public static partial class DbInfo
  {
    public static partial class Include
    {
      public static readonly string TableNameIs = "Include";

      public static readonly string Id = "Id";
      public static readonly string System = "System";
      public static readonly string Compose_Id = "Compose_Id";
    }
  }
}