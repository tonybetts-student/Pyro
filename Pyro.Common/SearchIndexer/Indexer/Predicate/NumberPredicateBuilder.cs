﻿using System;
using LinqKit;
using Pyro.Common.Search;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Pyro.Common.Search.SearchParameterEntity;

namespace Pyro.Common.SearchIndexer.Indexer.Predicate
{
  public static class NumberPredicateBuilder<IResourceIndexed>
  {
    public static ExpressionStarter<IResourceIndexed> Build(Search<IResourceIndexed> Search, ExpressionStarter<IResourceIndexed> NewPredicate, SearchParameterBase SearchItem)
    {
      if (SearchItem is SearchParameterNumber)
      {
        var SearchTypeNumber = SearchItem as SearchParameterNumber;
        foreach (var SearchValue in SearchTypeNumber.ValueList)
        {
          if (!SearchTypeNumber.Modifier.HasValue)
          {
            if (!SearchValue.Prefix.HasValue)
            {
              NewPredicate = CollectionEqualToPredicate(Search, NewPredicate, SearchTypeNumber, SearchValue);
            }
            else
            {
              switch (SearchValue.Prefix.Value)
              {
                case SearchParameter.SearchComparator.Eq:
                  NewPredicate = CollectionEqualToPredicate(Search, NewPredicate, SearchTypeNumber, SearchValue);
                  break;
                case SearchParameter.SearchComparator.Ne:
                  NewPredicate = CollectionNotEqualToPredicate(Search, NewPredicate, SearchTypeNumber, SearchValue);
                  break;
                case SearchParameter.SearchComparator.Gt:
                  NewPredicate = NewPredicate.Or(Search.NumberCollectionAnyGreaterThan(SearchTypeNumber.Id, SearchValue.Value));
                  break;
                case SearchParameter.SearchComparator.Lt:
                  NewPredicate = NewPredicate.Or(Search.NumberCollectionAnyLessThan(SearchTypeNumber.Id, SearchValue.Value));
                  break;
                case SearchParameter.SearchComparator.Ge:
                  NewPredicate = NewPredicate.Or(Search.NumberCollectionAnyGreaterThanOrEqualTo(SearchTypeNumber.Id, SearchValue.Value));
                  break;
                case SearchParameter.SearchComparator.Le:
                  NewPredicate = NewPredicate.Or(Search.NumberCollectionAnyLessThanOrEqualTo(SearchTypeNumber.Id, SearchValue.Value));
                  break;
                case SearchParameter.SearchComparator.Sa:
                  throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number.");
                case SearchParameter.SearchComparator.Eb:
                  throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number.");
                case SearchParameter.SearchComparator.Ap:
                  throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number.");
                default:
                  throw new System.ComponentModel.InvalidEnumArgumentException(SearchValue.Prefix.Value.GetLiteral(), (int)SearchValue.Prefix.Value, typeof(SearchParameter.SearchComparator));
              }
            }
          }
          else
          {
            switch (SearchTypeNumber.Modifier.Value)
            {
              case SearchParameter.SearchModifierCode.Missing:
                {
                  if (!SearchValue.Prefix.HasValue)
                  {
                    if (SearchValue.IsMissing)
                    {
                      NewPredicate = NewPredicate.Or(Search.SearchParameterIsNull<Index.IQuantityIndex>(SearchTypeNumber.Id));
                    }
                    else
                    {
                      NewPredicate = NewPredicate.Or(Search.SearchParameterIdIsNotNull<Index.IQuantityIndex>(SearchTypeNumber.Id));
                    }
                  }
                  else
                  {
                    switch (SearchValue.Prefix)
                    {
                      case SearchParameter.SearchComparator.Eq:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      case SearchParameter.SearchComparator.Ne:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      case SearchParameter.SearchComparator.Gt:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      case SearchParameter.SearchComparator.Lt:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      case SearchParameter.SearchComparator.Ge:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      case SearchParameter.SearchComparator.Le:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      case SearchParameter.SearchComparator.Sa:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      case SearchParameter.SearchComparator.Eb:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      case SearchParameter.SearchComparator.Ap:
                        throw new FormatException($"The search prefix: {SearchValue.Prefix.ToString()} is not supported for search parameter types of Number with a Modifier of: {SearchTypeNumber.Modifier.ToString()}.");
                      default:
                        throw new System.ComponentModel.InvalidEnumArgumentException(SearchValue.Prefix.Value.GetLiteral(), (int)SearchValue.Prefix, typeof(SearchParameter.SearchComparator));
                    }
                  }
                }
                break;
              case SearchParameter.SearchModifierCode.Exact:
                throw new FormatException($"The search modifier: {SearchTypeNumber.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Contains:
                throw new FormatException($"The search modifier: {SearchTypeNumber.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Text:
                throw new FormatException($"The search modifier: {SearchTypeNumber.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Type:
                throw new FormatException($"The search modifier: {SearchTypeNumber.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Below:
                throw new FormatException($"The search modifier: {SearchTypeNumber.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.Above:
                throw new FormatException($"The search modifier: {SearchTypeNumber.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.In:
                throw new FormatException($"The search modifier: {SearchTypeNumber.Modifier.ToString()} is not supported for search parameter types of date.");
              case SearchParameter.SearchModifierCode.NotIn:
                throw new FormatException($"The search modifier: {SearchTypeNumber.Modifier.ToString()} is not supported for search parameter types of date.");
              default:
                throw new System.ComponentModel.InvalidEnumArgumentException(SearchTypeNumber.Modifier.Value.GetLiteral(), (int)SearchTypeNumber.Modifier, typeof(SearchParameter.SearchModifierCode));
            }
          }
        }
      }

      return NewPredicate;
    }

    private static ExpressionStarter<IResourceIndexed> CollectionNotEqualToPredicate(Search<IResourceIndexed> Search, ExpressionStarter<IResourceIndexed> NewPredicate, SearchParameterNumber SearchTypeNumber, SearchParameterNumberValue SearchValue)
    {
      var NotEqualToExpression = Search.NumberCollectionAllNotEqualTo(SearchTypeNumber.Id, Common.Tools.DecimalSupport.CalculateLowNumber(SearchValue.Value, SearchValue.Scale), SearchValue.Value, Common.Tools.DecimalSupport.CalculateHighNumber(SearchValue.Value, SearchValue.Scale));
      var CollectionNotNull_Expression = Search.SearchParameterIdIsNotNull<Index.IQuantityIndex>(SearchTypeNumber.Id);

      ExpressionStarter<IResourceIndexed> NewAndPredicate = LinqKit.PredicateBuilder.New<IResourceIndexed>();
      NewAndPredicate = NewAndPredicate.And(NotEqualToExpression);
      NewAndPredicate = NewAndPredicate.And(CollectionNotNull_Expression);

      NewPredicate = NewPredicate.Or(NewAndPredicate);

      return NewPredicate;
    }

    private static ExpressionStarter<IResourceIndexed> CollectionEqualToPredicate(Search<IResourceIndexed> Search, ExpressionStarter<IResourceIndexed> NewPredicate, SearchParameterNumber SearchTypeNumber, SearchParameterNumberValue SearchValue)
    {
      var Expression = Search.NumberCollectionAnyEqualTo(SearchTypeNumber.Id, Common.Tools.DecimalSupport.CalculateLowNumber(SearchValue.Value, SearchValue.Scale), SearchValue.Value, Common.Tools.DecimalSupport.CalculateHighNumber(SearchValue.Value, SearchValue.Scale));
      NewPredicate = NewPredicate.Or(Expression);
      return NewPredicate;
    }

  }
}
