using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;

/// <summary>
/// Result collections extension methods
/// </summary>
public static class ToResultCollectionExtensions
{
    /// <summary>
    /// Converting collection to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection</param>
    /// <returns>Outgoing result collection</returns>
    public static IResultCollection<TValue> ToResultCollection<TValue>(this IEnumerable<TValue> @this)
        where TValue : notnull =>
        new ResultCollection<TValue>(@this);

    /// <summary>
    /// Converting collection to result collection with null checking
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection</param>
    /// <param name="error">Null error</param>
    /// <returns>Outgoing result collection</returns>
    public static IResultCollection<TValue> ToResultCollectionNullCheck<TValue>(this IEnumerable<TValue?>? @this,
                                                                                IErrorResult error)
        where TValue : class =>
        @this != null
            ? @this.Select(value => value.ToResultValueNullCheck(error)).
                    ToResultCollection()
            : new ResultCollection<TValue>(error);

    /// <summary>
    /// Converting result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>
    public static IResultCollection<TValue> ToResultCollection<TValue>(this IResultValue<IEnumerable<TValue>> @this) =>
        @this.OkStatus
            ? new ResultCollection<TValue>(@this.Value)
            : new ResultCollection<TValue>(@this.Errors);

    /// <summary>
    /// Converting collection of result value to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection of result value</param>
    /// <returns>Outgoing result collection</returns>
    public static IResultCollection<TValue> ToResultCollection<TValue>(this IEnumerable<IResultValue<TValue>> @this) =>
         @this.ToList().
         Map(collection => collection.All(result => result.OkStatus)
                 ? new ResultCollection<TValue>(collection.Select(result => result.Value))
                 : new ResultCollection<TValue>(collection.SelectMany(result => result.Errors)));

    /// <summary>
    /// Concat collection of result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result collection</param>
    /// <returns>Outgoing result collection</returns>
    public static IResultCollection<TValue> ToResultCollection<TValue>(this IEnumerable<IResultCollection<TValue>> @this) =>
         @this.ToList().
         Map(collection => collection.All(result => result.OkStatus)
                 ? new ResultCollection<TValue>(collection.SelectMany(result => result.Value))
                 : new ResultCollection<TValue>(collection.SelectMany(result => result.Errors)));
}