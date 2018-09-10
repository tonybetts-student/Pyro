﻿using System;
using LinqKit;
using Pyro.DataLayer.DbModel.EntityBase;
using Pyro.Common.Search;
using Pyro.Common.Tools;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Pyro.Common.Search.SearchParameterEntity;

namespace Pyro.DataLayer.Search.Predicate
{
  public static class DateTimePeriodPredicateBuilder<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType>
      where ResCurrentType : ResourceCurrentBase<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType>
      where ResIndexStringType : ResourceIndexString<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType>
      where ResIndexTokenType : ResourceIndexToken<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType>
      where ResIndexUriType : ResourceIndexUri<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType>
      where ResIndexReferenceType : ResourceIndexReference<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType>
      where ResIndexQuantityType : ResourceIndexQuantity<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType>
      where ResIndexDateTimeType : ResourceIndexDateTime<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType>
      
  {
    public static ExpressionStarter<ResCurrentType> Build(ResourceSearchExpressionTrees<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType> Search, ExpressionStarter<ResCurrentType> NewPredicate, SearchParameterBase SearchItem)
    {
      if (SearchItem is SearchParameterDateTime)
      {
        var SearchTypeDateTime = SearchItem as SearchParameterDateTime;
        foreach (var SearchValue in SearchTypeDateTime.ValueList)
        {
          if (SearchTypeDateTime.Modifier.HasValue == false)
          {
            if (SearchValue.Prefix.HasValue == false)
            {
              NewPredicate = CollectionEqualToPredicate(Search, NewPredicate, SearchTypeDateTime, SearchValue);
            }
            else
            {
              switch (SearchValue.Prefix)
              {
                case SearchParameter.SearchComparator.Eq:
                  NewPredicate = CollectionEqualToPredicate(Search, NewPredicate, SearchTypeDateTime, SearchValue);
                  break;
                case SearchParameter.SearchComparator.Ne:
                  NewPredicate = CollectionNotEqualToPredicate(Search, NewPredicate, SearchTypeDateTime, SearchValue);
                  break;
                case SearchParameter.SearchComparator.Gt:
                  NewPredicate = NewPredicate.Or(Search.DateTimePeriodCollectionGreaterThan(SearchTypeDateTime.Id, SearchValue.Value, FhirDateTimeSupport.CalculateHighDateTimeForRange(SearchValue.Value, SearchValue.Precision)));
                  break;
                case SearchParameter.SearchComparator.Lt:
                  NewPredicate = NewPredicate.Or(Search.DateTimePeriodCollectionLessThan(SearchTypeDateTime.Id, SearchValue.Value, FhirDateTimeSupport.CalculateHighDateTimeForRange(SearchValue.Value, SearchValue.Precision)));
                  break;
                case SearchParameter.SearchComparator.Ge:
                  NewPredicate = NewPredicate.Or(Search.DateTimePeriodCollectionGreaterThanOrEqualTo(SearchTypeDateTime.Id, SearchValue.Value, FhirDateTimeSupport.CalculateHighDateTimeForRange(SearchValue.Value, SearchValue.Precision)));
                  break;
                case SearchParameter.SearchComparator.Le:
                  NewPredicate = NewPredicate.Or(Search.DateTimePeriodCollectionLessThanOrEqualTo(SearchTypeDateTime.Id, SearchValue.Value, FhirDateTimeSupport.CalculateHighDateTimeForRange(SearchValue.Value, SearchValue.Precision)));
                  break;
                case SearchParameter.SearchComparator.Sa:
                  throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime.");
                case SearchParameter.SearchComparator.Eb:
                  throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime.");
                case SearchParameter.SearchComparator.Ap:
                  throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime.");
                default:
                  throw new System.ComponentModel.InvalidEnumArgumentException(SearchValue.Prefix.Value.GetLiteral(), (int)SearchValue.Prefix, typeof(SearchParameter.SearchComparator));
              }
            }
          }
          else
          {
            switch (SearchTypeDateTime.Modifier.Value)
            {
              case SearchParameter.SearchModifierCode.Missing:
                if (SearchValue.Prefix.HasValue == false)
                {
                  if (SearchValue.IsMissing)
                  {
                    NewPredicate = NewPredicate.Or(Search.SearchParameterIsNull<ResIndexDateTimeType>(SearchTypeDateTime.Id));
                  }
                  else
                  {
                    NewPredicate = NewPredicate.Or(Search.SearchParameterIdIsNotNull<ResIndexDateTimeType>(SearchTypeDateTime.Id));
                  }
                }
                else
                {
                  switch (SearchValue.Prefix)
                  {
                    case SearchParameter.SearchComparator.Eq:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    case SearchParameter.SearchComparator.Ne:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    case SearchParameter.SearchComparator.Gt:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    case SearchParameter.SearchComparator.Lt:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    case SearchParameter.SearchComparator.Ge:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    case SearchParameter.SearchComparator.Le:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    case SearchParameter.SearchComparator.Sa:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    case SearchParameter.SearchComparator.Eb:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    case SearchParameter.SearchComparator.Ap:
                      throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of FhirDateTime with a Modifier of: {SearchTypeDateTime.Modifier.ToString()}.");
                    default:
                      throw new System.ComponentModel.InvalidEnumArgumentException(SearchValue.Prefix.Value.GetLiteral(), (int)SearchValue.Prefix, typeof(SearchParameter.SearchComparator));
                  }
                  //break;
                }
                break;
              case SearchParameter.SearchModifierCode.Exact:
                throw new FormatException($"The search modifier: {SearchTypeDateTime.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Contains:
                throw new FormatException($"The search modifier: {SearchTypeDateTime.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Text:
                throw new FormatException($"The search modifier: {SearchTypeDateTime.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Type:
                throw new FormatException($"The search modifier: {SearchTypeDateTime.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Below:
                throw new FormatException($"The search modifier: {SearchTypeDateTime.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Above:
                throw new FormatException($"The search modifier: {SearchTypeDateTime.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.In:
                throw new FormatException($"The search modifier: {SearchTypeDateTime.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.NotIn:
                throw new FormatException($"The search modifier: {SearchTypeDateTime.Modifier.ToString()} is not supported for search parameter types of date.");
              default:
                throw new System.ComponentModel.InvalidEnumArgumentException(SearchTypeDateTime.Modifier.Value.GetLiteral(), (int)SearchTypeDateTime.Modifier, typeof(SearchParameter.SearchModifierCode));
            }
          }
        }
      }

      return NewPredicate;
    }

    private static ExpressionStarter<ResCurrentType> CollectionEqualToPredicate(ResourceSearchExpressionTrees<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType> Search, ExpressionStarter<ResCurrentType> NewPredicate, SearchParameterDateTime SearchTypeDateTime, SearchParameterDateTimeValue SearchValue)
    {
      var Expression = Search.DateTimePeriodCollectionAnyEqualTo(SearchTypeDateTime.Id, SearchValue.Value, FhirDateTimeSupport.CalculateHighDateTimeForRange(SearchValue.Value, SearchValue.Precision));
      NewPredicate = NewPredicate.Or(Expression);
      return NewPredicate;

    }

    private static ExpressionStarter<ResCurrentType> CollectionNotEqualToPredicate(ResourceSearchExpressionTrees<ResCurrentType, ResIndexStringType, ResIndexTokenType, ResIndexUriType, ResIndexReferenceType, ResIndexQuantityType, ResIndexDateTimeType> Search, ExpressionStarter<ResCurrentType> NewPredicate, SearchParameterDateTime SearchTypeDateTime, SearchParameterDateTimeValue SearchValue)
    {
      var NotEqualToExpression = Search.DateTimePeriodCollectionAnyNotEqualTo(SearchTypeDateTime.Id, SearchValue.Value, FhirDateTimeSupport.CalculateHighDateTimeForRange(SearchValue.Value, SearchValue.Precision));
      var CollectionIsNullExpression = Search.SearchParameterIsNull<ResIndexDateTimeType>(SearchTypeDateTime.Id);
      NewPredicate = NewPredicate.Or(NotEqualToExpression);
      NewPredicate = NewPredicate.Or(CollectionIsNullExpression);
      return NewPredicate;
    }

  }
}
