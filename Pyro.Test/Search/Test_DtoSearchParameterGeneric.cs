﻿using System;
using NUnit.Framework;
using Pyro.Common.Tools;
using Hl7.Fhir.Model;
using NUnit.Framework.Constraints;
using Pyro.Common.Search;
using Pyro.Common.Interfaces.Dto;

namespace Pyro.Test.IndexSetters
{
  [TestFixture]
  [Category("Search")]
  class Test_DtoSearchParameterGeneric
  {
    [Test]
    public void Test_Simple()
    {
      //Arrange
      string UrlSearchParametersString = "?family=Millar&given=Angus";
      var DtoSearchParameterGeneric = new SearchParameterGeneric();

      //Act
      ISearchParameterGeneric Result = DtoSearchParameterGeneric.Parse(UrlSearchParametersString);

      //Assert
      Assert.AreEqual(2, Result.ParameterList.Count);
      Assert.AreEqual("Millar", Result.ParameterList[0].Item2);
      Assert.AreEqual("Angus", Result.ParameterList[1].Item2);
      Assert.IsNull(Result.Count);
      Assert.IsNull(Result.SummaryType);
    }

    [Test]
    public void Test_Include()
    {
      //Arrange
      string UrlSearchParametersString = "?_count=5&_count=10&family=Millar,Darmody1&family=Millar2&given=Angus&_include=Patient%3Aorganization";
      var DtoSearchParameterGeneric = new SearchParameterGeneric();

      //Act
      ISearchParameterGeneric Result = DtoSearchParameterGeneric.Parse(UrlSearchParametersString);

      //Assert
      Assert.AreEqual(3, Result.ParameterList.Count);
      Assert.AreEqual("Millar,Darmody1", Result.ParameterList[0].Item2);
      Assert.AreEqual("Millar2", Result.ParameterList[1].Item2);
      Assert.AreEqual(10, Result.Count.Value);
      Assert.IsNull(Result.SummaryType);
      Assert.AreEqual(1, Result.Include.Count);
      Assert.AreEqual("Patient:organization", Result.Include[0].Item2);
    }



  }
}
