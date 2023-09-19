using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
{
    /// <summary>
    /// Result collections extension methods
    /// </summary>
    public static class RListFoldExtensions
    {
        /// <summary>
        /// Aggregate collection of result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> RListFold<TValue>(this IEnumerable<IRList<TValue>> @this)
            where TValue : notnull =>
             RListFold(@this.ToList());

        /// <summary>
        /// Aggregate collection of result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> RListFold<TValue>(this IReadOnlyCollection<IRList<TValue>> @this)
            where TValue : notnull =>
             @this.All(collection => collection.Success)
                ? @this.SelectMany(collection => collection.GetValue()).ToRList()
                : @this.SelectMany(collection =>  collection.GetErrorsOrEmpty())
                   .ToRList<TValue>();
    }
}