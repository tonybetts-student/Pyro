﻿using System;
using Pyro.DataLayer.DbModel.EntityBase;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Pyro.Common.BusinessEntities.Dto;
using System.Collections.Generic;

namespace Pyro.DataLayer.IndexSetter
{
  public static class StringSetter<ResourceIndexType> where ResourceIndexType : ResourceIndexBase, new()
  {
    private static List<ResourceIndexType> ResourceIndexList;
    private static int ServiceSearchParameterId;  
    private const string ItemDelimeter = " ";

    public static IList<ResourceIndexType> Set(IElementNavigator oElement, DtoServiceSearchParameterLight SearchParameter)      
    {
      ResourceIndexList = new List<ResourceIndexType>();
      ServiceSearchParameterId = SearchParameter.Id;

      if (oElement is Hl7.Fhir.FhirPath.PocoNavigator Poco && Poco.FhirValue != null)
      {        
        if (Poco.FhirValue is FhirString FhirString)
        {
          SetFhirString(FhirString);
        }
        else if (Poco.FhirValue is Address address)
        {
          SetAddress(address);          
        }
        else if (Poco.FhirValue is HumanName HumanName)
        {
          SetHumanName(HumanName);         
        }
        else if (Poco.FhirValue is Markdown Markdown)
        {
          SetMarkdown(Markdown);          
        }
        else if (Poco.FhirValue is Annotation Annotation)
        {
          SetAnnotation(Annotation);          
        }
        else
        {
          throw new FormatException($"Unkown FhirType: '{oElement.Type}' for SearchParameterType: '{SearchParameter.Type}'");
        }
      }
      else if (oElement.Value is Hl7.FhirPath.ConstantValue ConstantValue)
      {
        var ResourceIndex = new ResourceIndexType();
        ResourceIndex.String = ConstantValue.Type.ToString();
        ResourceIndexList.Add(ResourceIndex);
      }
      else if (oElement.Value is bool Bool)
      {
        var ResourceIndex = new ResourceIndexType();
        ResourceIndex.String = Bool.ToString();
        ResourceIndexList.Add(ResourceIndex);
      }
      else
      {
        throw new FormatException($"Unkown FhirType: '{oElement.Type}' for SearchParameterType: '{SearchParameter.Type}'");
      }
      ResourceIndexList.ForEach(x => x.ServiceSearchParameterId = ServiceSearchParameterId);
      return ResourceIndexList;
    }

    private static void SetFhirString(FhirString FhirString)      
    {
      if (!string.IsNullOrWhiteSpace(FhirString.Value))
      {
        var ResourceIndex = new ResourceIndexType();
        ResourceIndex.String = FhirString.Value;
        ResourceIndexList.Add(ResourceIndex);
      }      
    }
    private static void SetAnnotation(Annotation Annotation)
    {
      if (!string.IsNullOrWhiteSpace(Annotation.Text))
      {
        var ResourceIndex = new ResourceIndexType();
        ResourceIndex.String = Pyro.Common.Tools.StringSupport.ToLowerAndRemoveDiacritics(Annotation.Text.Trim());
        ResourceIndexList.Add(ResourceIndex);
      }      
    }
    private static void SetMarkdown(Markdown Markdown)
    {
      if (!string.IsNullOrWhiteSpace(Markdown.Value))
      {
        var ResourceIndex = new ResourceIndexType();
        ResourceIndex.String = Pyro.Common.Tools.StringSupport.ToLowerAndRemoveDiacritics(Markdown.Value.Trim());
        ResourceIndexList.Add(ResourceIndex);
      }      
    }
    private static void SetHumanName(HumanName HumanName)       
    {
      string FullName = string.Empty;
      foreach (var Given in HumanName.Given)
      {
        FullName += Given + ItemDelimeter;
      }

      if (!string.IsNullOrWhiteSpace(HumanName.Family))
      {
        FullName += HumanName.Family + ItemDelimeter;
      }

      if (FullName != string.Empty)
      {
        var ResourceIndex = new ResourceIndexType();
        ResourceIndex.String = Pyro.Common.Tools.StringSupport.ToLowerAndRemoveDiacritics(FullName.Trim());
        ResourceIndexList.Add(ResourceIndex);
      }      
    }

    private static void SetAddress(Address Address)
    {
      string FullAdddress = string.Empty;
      foreach (var Line in Address.Line)
      {
        FullAdddress += Line + ItemDelimeter;
      }
      if (!string.IsNullOrWhiteSpace(Address.City))
      {
        FullAdddress += Address.City + ItemDelimeter;
      }
      if (!string.IsNullOrWhiteSpace(Address.PostalCode))
      {
        FullAdddress += Address.PostalCode + ItemDelimeter;
      }
      if (!string.IsNullOrWhiteSpace(Address.State))
      {
        FullAdddress += Address.State + ItemDelimeter;
      }
      if (!string.IsNullOrWhiteSpace(Address.Country))
      {
        FullAdddress += Address.Country + ItemDelimeter;
      }
      if (FullAdddress != string.Empty)
      {
        var ResourceIndex = new ResourceIndexType();
        ResourceIndex.String = Pyro.Common.Tools.StringSupport.ToLowerAndRemoveDiacritics(FullAdddress.Trim());
        ResourceIndexList.Add(ResourceIndex);
      }      
    }
  }
}