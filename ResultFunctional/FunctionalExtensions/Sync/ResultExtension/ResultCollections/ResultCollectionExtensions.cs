using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Result collections extension methods
    /// </summary>
    public static class ResultCollectionExtensions
    {
        /// <summary>
        /// Aggregate collection of result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValue> ConcatResultCollection<TValue>(this IEnumerable<IResultCollection<TValue>> @this) =>
             ConcatResultCollection(@this.ToList());

        /// <summary>
        /// Aggregate collection of result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValue> ConcatResultCollection<TValue>(this IReadOnlyCollection<IResultCollection<TValue>> @this) =>
             new ResultCollection<TValue>(@this.SelectMany(result => result.Value)).
             ConcatErrors(@this.SelectMany(result => result.Errors));

        /// <summary>
        /// Merge result error with result collection
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultCollection">Result Collection</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValue> ToResultCollection<TValue>(this IResultError @this, IReadOnlyCollection<TValue> resultCollection) =>
            @this.OkStatus
                ? new ResultCollection<TValue>(resultCollection)
                : new ResultCollection<TValue>(@this.Errors);

    }
}