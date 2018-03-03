﻿using Pyro.Common.CompositionRoot;
using Pyro.Common.Tools.UriSupport;

namespace Pyro.WebApi.CompositionRoot
{
  public class PyroFhirUriFactory : IPyroFhirUriFactory
  {
    private readonly SimpleInjector.Container Container;

    public PyroFhirUriFactory(SimpleInjector.Container Container)
    {
      this.Container = Container;
    }

    public IPyroFhirUri CreateFhirRequestUri()
    {
      return Container.GetInstance<IPyroFhirUri>();
    }
  }
}