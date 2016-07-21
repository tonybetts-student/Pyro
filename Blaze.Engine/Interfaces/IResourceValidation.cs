﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

namespace Blaze.Engine.Interfaces
{
  public interface IResourceValidation 
  {
    Common.Interfaces.IResourceValidationOperationOutcome Validate(Resource Resource);
  }
}