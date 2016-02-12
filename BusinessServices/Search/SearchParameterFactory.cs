﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BusinessEntities;
using BusinessEntities.Search;
using Hl7.Fhir.Model;

namespace Blaze.Engine.Search
{
  public static class SearchParameterFactory
  {
    private static readonly char _ParameterNameParameterValueDilimeter = '=';
    private static readonly char _ParameterNameModifierDilimeter = ':';
    private static string _CurrentResourceName = string.Empty;
    private static string _RawSearchParameterAndValueString = string.Empty;

    public static DtoSearchParameterBase CreateSearchParameter(DtoEnums.SupportedFhirResource Resource, DtoEnums.Search.SearchParameterName SearchParameterName,
                  Tuple<string, string> Parameter,
                  SearchParamType SearchParameterType)
    {
      DtoSearchParameterBase oSearchParameter = InitalizeSearchParameter(SearchParameterType);
      string ParameterName = Parameter.Item1;
      string ParameterValue = Parameter.Item2;
      _CurrentResourceName = Resource.ToString();
      oSearchParameter.Resource = Resource;
      oSearchParameter.Name = SearchParameterName;      
      oSearchParameter.RawValue = ParameterName + _ParameterNameParameterValueDilimeter + ParameterValue;
      _RawSearchParameterAndValueString = oSearchParameter.RawValue;
      oSearchParameter.SearchParameterType = SearchParameterType;
      ParseModifier(ParameterName, oSearchParameter);
      string Value = ParsePrefix(ParameterValue, oSearchParameter);
      if (!oSearchParameter.TryParseValue(Value))
      {
        var oIssueComponent = new OperationOutcome.IssueComponent();
        oIssueComponent.Severity = OperationOutcome.IssueSeverity.Fatal;
        oIssueComponent.Code = OperationOutcome.IssueType.Exception;
        oIssueComponent.Details = new CodeableConcept("http://hl7.org/fhir/operation-outcome", "SEARCH_NONE", String.Format("Error: no processable search found for '{0}' search parameters '{1}", Resource.ToString(), oSearchParameter.RawValue));
        oIssueComponent.Details.Text = String.Format("Unable to parse the given search parameter value for parameter = value: {0}", oSearchParameter.RawValue);
        oIssueComponent.Diagnostics = oIssueComponent.Details.Text;
        var oOperationOutcome = new OperationOutcome();
        oOperationOutcome.Issue = new List<OperationOutcome.IssueComponent>() { oIssueComponent };
        throw new DtoBlazeException(System.Net.HttpStatusCode.BadRequest, oOperationOutcome, oIssueComponent.Details.Text);
      }
      return oSearchParameter;
    }

    private static void ParseModifierType(DtoSearchParameterBase SearchParameter, string value)
    {
      switch (value)
      {
        case "above":
          SearchParameter.Modifier = SearchModifierType.Above;
          break;
        case "below":
          SearchParameter.Modifier = SearchModifierType.Below;
          break;
        case "contains":
          SearchParameter.Modifier = SearchModifierType.Contains;
          break;
        case "exact":
          SearchParameter.Modifier = SearchModifierType.Exact;
          break;
        case "in":
          SearchParameter.Modifier = SearchModifierType.In;
          break;
        case "missing":
          SearchParameter.Modifier = SearchModifierType.Missing;
          break;
        case "notin":
          SearchParameter.Modifier = SearchModifierType.NotIn;
          break;
        case "text":
          SearchParameter.Modifier = SearchModifierType.Text;
          break;
        default:
          {
            if (value.Contains('[') && value.Contains(']'))
            {
              char[] delimiters = { '[', ']' };
              var TypedResourceName = value.Split(delimiters)[1];
              if (ModelInfo.IsKnownResource(TypedResourceName))
              {
                var FhirResourceTypeDictionary = DtoEnums.GetFhirResourceTypeByNameDictionary();
                if (FhirResourceTypeDictionary.ContainsKey(TypedResourceName))
                {
                  var BlazeSupportedResourceTypeDictionary = DtoEnums.GetBlazeSupportedResorceTypeByFhirResourceTypeDictionary();
                  var BlazeSupportedResourceType = FhirResourceTypeDictionary[TypedResourceName];
                  if (BlazeSupportedResourceTypeDictionary.ContainsKey(BlazeSupportedResourceType))
                  {
                    SearchParameter.Resource = BlazeSupportedResourceTypeDictionary[BlazeSupportedResourceType];
                  }
                  else
                  {
                    //The Resource stated in the Type is not a Blaze supported FHIR resource so throw an error;
                    var oIssueComponent = new OperationOutcome.IssueComponent();
                    oIssueComponent.Severity = OperationOutcome.IssueSeverity.Fatal;
                    oIssueComponent.Code = OperationOutcome.IssueType.Exception;
                    oIssueComponent.Details = new CodeableConcept("http://hl7.org/fhir/operation-outcome", "SEARCH_NONE", String.Format("Error: no processable search found for '{0}' search parameters '{1}", _CurrentResourceName, _RawSearchParameterAndValueString));
                    oIssueComponent.Details.Text = String.Format("Unable to parse the given search parameter value for parameter = value: {0}. The Resource stated in the brackets [{1}] is not a Blaze supported resource type.", _RawSearchParameterAndValueString, TypedResourceName);
                    oIssueComponent.Diagnostics = oIssueComponent.Details.Text;
                    var oOperationOutcome = new OperationOutcome();
                    oOperationOutcome.Issue = new List<OperationOutcome.IssueComponent>() { oIssueComponent };
                    throw new DtoBlazeException(System.Net.HttpStatusCode.BadRequest, oOperationOutcome, oIssueComponent.Details.Text);
                  }
                }
                else
                {
                  //The Resource stated in the Type is not a Blaze supported FHIR resource so throw an error;
                  var oIssueComponent = new OperationOutcome.IssueComponent();
                  oIssueComponent.Severity = OperationOutcome.IssueSeverity.Fatal;
                  oIssueComponent.Code = OperationOutcome.IssueType.Exception;
                  oIssueComponent.Details = new CodeableConcept("http://hl7.org/fhir/operation-outcome", "SEARCH_NONE", String.Format("Error: no processable search found for '{0}' search parameters '{1}", _CurrentResourceName, _RawSearchParameterAndValueString));
                  oIssueComponent.Details.Text = String.Format("Unable to parse the given search parameter value for parameter = value: {0}. The Resource stated in the brackets [{1}] was not able to be converted to a known blaze FHIR resource type.", _RawSearchParameterAndValueString, TypedResourceName);
                  oIssueComponent.Diagnostics = oIssueComponent.Details.Text;
                  var oOperationOutcome = new OperationOutcome();
                  oOperationOutcome.Issue = new List<OperationOutcome.IssueComponent>() { oIssueComponent };
                  throw new DtoBlazeException(System.Net.HttpStatusCode.BadRequest, oOperationOutcome, oIssueComponent.Details.Text);
                }
              }
              else
              {
                //The Resource stated in the Type is not a known FHIR resource by the FHIR API in use so throw an error;
                var oIssueComponent = new OperationOutcome.IssueComponent();
                oIssueComponent.Severity = OperationOutcome.IssueSeverity.Fatal;
                oIssueComponent.Code = OperationOutcome.IssueType.Exception;
                oIssueComponent.Details = new CodeableConcept("http://hl7.org/fhir/operation-outcome", "SEARCH_NONE", String.Format("Error: no processable search found for '{0}' search parameters '{1}", _CurrentResourceName, _RawSearchParameterAndValueString));
                oIssueComponent.Details.Text = String.Format("Unable to parse the given search parameter value for parameter = value: {0}. The Resource stated in the brackets [{1}] is not a known resource type by the FHIR API in use.", _RawSearchParameterAndValueString, TypedResourceName);
                oIssueComponent.Diagnostics = oIssueComponent.Details.Text;
                var oOperationOutcome = new OperationOutcome();
                oOperationOutcome.Issue = new List<OperationOutcome.IssueComponent>() { oIssueComponent };
                throw new DtoBlazeException(System.Net.HttpStatusCode.BadRequest, oOperationOutcome, oIssueComponent.Details.Text);
              }
              SearchParameter.Modifier = SearchModifierType.Type;
            }
            else
              SearchParameter.Modifier = SearchModifierType.None;
          }
          break;
      }
    }
    private static DtoSearchParameterBase InitalizeSearchParameter(SearchParamType SearchParameterType)
    {
      DtoSearchParameterBase oSearchParameter = null;
      switch (SearchParameterType)
      {
        case SearchParamType.Composite:
          throw new NotImplementedException("SearchParamType.Composite");
        case SearchParamType.Date:
          oSearchParameter = new DtoSearchParameterDate();
          break;
        case SearchParamType.Number:
          oSearchParameter = new DtoSearchParameterNumber();
          break;
        case SearchParamType.Quantity:
          throw new NotImplementedException("SearchParamType.Quantity");
        case SearchParamType.Reference:
          throw new NotImplementedException("SearchParamType.Reference");
        case SearchParamType.String:
          oSearchParameter = new DtoSearchParameterString();
          break;
        case SearchParamType.Token:
          oSearchParameter = new DtoSearchParameterToken();
          break;
        case SearchParamType.Uri:
          throw new NotImplementedException("SearchParamType.Uri");
        default:
          break;
      }
      return oSearchParameter;
    }
    private static void ParseModifier(string Name, DtoSearchParameterBase oSearchParameter)
    {

      if (Name.Contains(_ParameterNameModifierDilimeter))
      {
        ParseModifierType(oSearchParameter, Name.Split(_ParameterNameModifierDilimeter)[1]);
      }
      else
      {
        oSearchParameter.Modifier = SearchModifierType.None;
        oSearchParameter.TypeModifierResource = null;
      }
    }
    private static string ParsePrefix(string Value, DtoSearchParameterBase oSearchParameter)
    {
      if (oSearchParameter.SearchParameterType == SearchParamType.Date ||
        oSearchParameter.SearchParameterType == SearchParamType.Number ||
        oSearchParameter.SearchParameterType == SearchParamType.Quantity)
      {
        if (Value.Length > 2)
        {
          //Are the first two char Alpha characters 
          if (Regex.IsMatch(Value.Substring(0, 2), @"^[a-zA-Z]+$"))
          {
            var SearchPrefixTypeDictionary = DtoEnums.Search.GetSearchPrefixTypeDictionary();
            if (SearchPrefixTypeDictionary.ContainsKey(Value.Substring(0, 2)))
            {
              oSearchParameter.Prefix = SearchPrefixTypeDictionary[Value.Substring(0, 2)];
              Value = Value.Substring(2);
            }
          }
          else
          {
            oSearchParameter.Prefix = SearchPrefixType.None;
          }
        }
      }
      return Value;
    }
  }
}