﻿using System;
using System.Collections.Generic;
using FhirModel = Hl7.Fhir.Model;

namespace Pyro.Common.PyroHealthFhirResource.CompartmentDefinitions
{
  public class PyroCompartmentDefinitionEncounter : IPyroCompartmentDefinitionEncounter
  {    
    private readonly CodeSystems.IPyroFhirServer IPyroFhirServerCodeSystem;
  
    public PyroCompartmentDefinitionEncounter(CodeSystems.IPyroFhirServer IPyroFhirServerCodeSystem)
    {      
      this.IPyroFhirServerCodeSystem = IPyroFhirServerCodeSystem;
    }

    private static string ResourceId = "pyro-encounter";
    public string GetResourceId()
    {
      return ResourceId;
    }
   
    public string GetCanonicalUrl()
    {
      return Elements.PyroHealthCanonicalUrl.CanonicalUrlBuild($"CompartmentDefinition/{GetResourceId()}");
    }

    //If you wish to update the Compartments in the server you need to not only chnage this datetime to update the 
    //CompartmentDefinition resource in the servers but also the Task that sets the CompartmentDefinition Compartments as Active
    //That Task is foud here: Pyro.Common.PyroHealthFhirResource.Tasks.SetCompartmentDefinitions
    public DateTimeOffset MasterLastUpdated => new DateTimeOffset(2018, 09, 17, 14, 00, 00, new TimeSpan(8, 0, 0));

    public FhirModel.CompartmentDefinition GetResource()
    { 
      var ResourceBase = Common.Tools.FhirResourceSerializationSupport.DeSerializeFromXml(CommonResource.PyroCompartmentDefinitionEncounter);
      FhirModel.CompartmentDefinition Resource = ResourceBase as FhirModel.CompartmentDefinition;
      Resource.Status = FhirModel.PublicationStatus.Active;
      Resource.Id = GetResourceId();
      IPyroFhirServerCodeSystem.SetProtectedMetaTag(Resource);
      Resource.Meta.LastUpdated = MasterLastUpdated;
      Resource.Url = GetCanonicalUrl();
      Resource.Contact = new List<FhirModel.ContactDetail>();
      Resource.Contact.Add(PyroHealthFhirResource.Elements.PyroHealthContactDetailAngusMillar.GetContactDetail());
      //The rest of the Resource content comes form the base xml file read in.
      return Resource;
    }
    
  }
}
