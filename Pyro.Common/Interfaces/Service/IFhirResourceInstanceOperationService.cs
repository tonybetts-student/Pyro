﻿using Hl7.Fhir.Model;
using Pyro.Common.Service;
using Pyro.Common.RequestMetadata;

namespace Pyro.Common.Interfaces.Service
{
  public interface IFhirResourceInstanceOperationService
  {
    IResourceServiceOutcome Process(string OperationName, Resource Resource, IRequestMeta RequestMeta);
  }
}