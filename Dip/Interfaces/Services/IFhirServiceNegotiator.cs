﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dip.Interfaces.Services
{
  public interface IFhirServiceNegotiator
  {
    IBaseResourceServices GetService(string type);
  }
}