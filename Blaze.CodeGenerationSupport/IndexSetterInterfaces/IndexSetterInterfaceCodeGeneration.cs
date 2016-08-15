﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blaze.Common.Enum;
using Blaze.CodeGenerationSupport.FhirApiIntrospection;
using Hl7.Fhir.Model;

namespace Blaze.CodeGenerationSupport.IndexSetterInterfaces
{
  public class IndexSetterInterfaceCodeGeneration
  {
    public List<IndexSetterInterfaceCodeGenModel> Generate()
    {
      List<string> ResourceList = Hl7.Fhir.Model.ModelInfo.SupportedResources;
      List<FhirApiSearchParameterInfo> _SearchParametersList = FhirApiSearchParameterInfoFactory.GetApiSearchParameterInfo();
      HashSet<string> TypeUnquieDic = new HashSet<string>();
      List<string> TypeAnalysisFullList = new List<string>();
      List<IndexSetterInterfaceCodeGenModel> FinalResultList = new List<IndexSetterInterfaceCodeGenModel>();

      var DateListFinal = new IndexSetterInterfaceCodeGenModel();
      DateListFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.DateIndex]);
      DateListFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(DateListFinal);

      var DatePeriodListFinal = new IndexSetterInterfaceCodeGenModel();
      DatePeriodListFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.DatePeriodIndex]);
      DatePeriodListFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(DatePeriodListFinal);

      var NumberFinal = new IndexSetterInterfaceCodeGenModel();
      NumberFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.NumberIndex]);
      NumberFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(NumberFinal);

      var QuantityListFinal = new IndexSetterInterfaceCodeGenModel();
      QuantityListFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.QuantityIndex]);
      QuantityListFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(QuantityListFinal);

      var QuantityRangeListFinal = new IndexSetterInterfaceCodeGenModel();
      QuantityRangeListFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.QuantityRangeIndex]);
      QuantityRangeListFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(QuantityRangeListFinal);

      var ReferenceListFinal = new IndexSetterInterfaceCodeGenModel();
      ReferenceListFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.ReferenceIndex]);
      ReferenceListFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(ReferenceListFinal);

      var StringListFinal = new IndexSetterInterfaceCodeGenModel();
      StringListFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.StringIndex]);
      StringListFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(StringListFinal);

      var TokenListFinal = new IndexSetterInterfaceCodeGenModel();
      TokenListFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.TokenIndex]);
      TokenListFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(TokenListFinal);

      var UriListFinal = new IndexSetterInterfaceCodeGenModel();
      UriListFinal.ClassName = ConstructInterfaceClassName(DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.UriIndex]);
      UriListFinal.ImplementsInterface = DatabaseModelInfo.IndexSetterBaseInterfaceName;
      FinalResultList.Add(UriListFinal);

      CustomTokenInterfaceMethodForCodeTType(TokenListFinal);

      foreach (var ResourceName in ResourceList)
      {
        List<FhirApiSearchParameterInfo> SearchParametersForResource = (from x in _SearchParametersList
                                                                        where x.Resource == ResourceName
                                                                        select x).ToList();

        FhirApiSearchParameterInfoFactory.FHIRApiCorrectionsForRepository(SearchParametersForResource);
        foreach (FhirApiSearchParameterInfo Parameter in SearchParametersForResource)
        {

          TypeAnalysisFullList.Add(string.Format("{0}, {1}, {2}, {3}", Parameter.Resource, Parameter.SearchParamType, Parameter.TargetFhirLogicalType, Parameter.SearchPath));
          string Key = string.Format("{0}, {1}", Parameter.SearchParamType, ConstructInterfaceFhirType(Parameter.TargetFhirLogicalType.Name));
          if (TypeUnquieDic.Add(Key))
          {
            IndexSetterInterfaceMethod MethodInfo = new IndexSetterInterfaceMethod();
            MethodInfo.IndexTypeString = DatabaseModelInfo.GetServerSearchIndexTypeString(Parameter);
            MethodInfo.IndexType = DatabaseEnum.StringToBlazeIndexTypeDictonary[MethodInfo.IndexTypeString];
            MethodInfo.FhirType = ConstructInterfaceFhirType(Parameter.TargetFhirLogicalType.Name);


            switch (MethodInfo.IndexType)
            {
              case DatabaseEnum.BlazeIndexType.DateIndex:
                DateListFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              case DatabaseEnum.BlazeIndexType.DatePeriodIndex:
                DatePeriodListFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              case DatabaseEnum.BlazeIndexType.NumberIndex:
                NumberFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              case DatabaseEnum.BlazeIndexType.QuantityIndex:
                QuantityListFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              case DatabaseEnum.BlazeIndexType.QuantityRangeIndex:
                QuantityRangeListFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              case DatabaseEnum.BlazeIndexType.ReferenceIndex:
                ReferenceListFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              case DatabaseEnum.BlazeIndexType.StringIndex:
                StringListFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              case DatabaseEnum.BlazeIndexType.TokenIndex:
                TokenListFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              case DatabaseEnum.BlazeIndexType.UriIndex:
                UriListFinal.IndexSetterMethodList.Add(MethodInfo);
                break;
              default:
                throw new System.ComponentModel.InvalidEnumArgumentException(MethodInfo.IndexType.ToString(), (int)MethodInfo.IndexType, typeof(DatabaseEnum.BlazeIndexType));
            }
          }
        }
      }

      //Debug file output
      //----------------------------------------------------------------------------------------------
      //System.IO.File.AppendAllLines(@"C:\temp\BlaseDebugTypeUnquieInfo.csv", TypeUnquieDic);
      //System.IO.File.AppendAllLines(@"C:\temp\BlaseDebugTypeFullInfo.csv", TypeAnalysisFullList);
      //----------------------------------------------------------------------------------------------




      return FinalResultList;

    }

    private static void CustomTokenInterfaceMethodForCodeTType(IndexSetterInterfaceCodeGenModel TokenListFinal)
    {
      IndexSetterInterfaceMethod CustomTokenCodeMethodInfo = new IndexSetterInterfaceMethod();
      CustomTokenCodeMethodInfo.CustomeIndexMethod = string.Format("TokenIndex SetCodeT(Element Element, TokenIndex TokenIndex);");
      CustomTokenCodeMethodInfo.IndexTypeString = DatabaseEnum.BlazeIndexTypeToStringDictonary[DatabaseEnum.BlazeIndexType.TokenIndex];
      CustomTokenCodeMethodInfo.IndexType = DatabaseEnum.StringToBlazeIndexTypeDictonary[CustomTokenCodeMethodInfo.IndexTypeString];
      TokenListFinal.IndexSetterMethodList.Add(CustomTokenCodeMethodInfo);
    }

    private string ConstructInterfaceClassName(string IndexTypeName)
    {
      return string.Format("I{0}Setter", IndexTypeName);
    }

    private string ConstructInterfaceFhirType(string FhirTypeName)
    {
      if (FhirTypeName.Contains("Code`1"))
      {
        //Here we need to capture Fhir types of Code<???> and return as just Code, also needs to be uppercase Code not code.
        //example: Hl7.Fhir.Model.Code`1[Hl7.Fhir.Model.AllergyIntolerance+AllergyIntoleranceCategory]        
        return typeof(Code).Name;
      }
      else
      {
        var SplitOnDot = FhirTypeName.Split('.');
        return SplitOnDot[SplitOnDot.Length - 1];
      }
    }

  }
}
