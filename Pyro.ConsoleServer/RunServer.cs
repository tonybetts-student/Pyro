﻿using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyro.Web;
using Pyro.Common.Cache;

namespace Pyro.ConsoleServer
{
  class RunServer
  {
    static void Main(string[] args)
    {
      string FhirEndpoint = WebConfigProperties.ServiceBaseURL();
      Uri FhirEndpointUri = new Uri(FhirEndpoint);
      string uri = $"{FhirEndpointUri.Scheme}://{FhirEndpointUri.Authority}";

      using (WebApp.Start<StartupConsole>(uri))
      {
        Console.WriteLine("===================================================================");
        Console.WriteLine("================| Pyro FHIR Server Running |=======================");
        Console.WriteLine("===================================================================");
        Console.WriteLine("Endpoint: " + FhirEndpoint);
        Console.WriteLine("(Hit any key to stop the server)");
        Console.ReadKey();
        Console.WriteLine("===================================================================");
        Console.WriteLine("Server Stopping");
      }
    }
  }

}
