using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists
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
        public static IRList<TValue> ConcatResultCollection<TValue>(this IEnumerable<IRList<TValue>> @this)
            where TValue : notnull =>
             ConcatResultCollection(@this.ToList());

        /// <summary>
        /// Aggregate collection of result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> ConcatResultCollection<TValue>(this IReadOnlyCollection<IRList<TValue>> @this)
            where TValue : notnull =>
             @this.SelectMany(result => result.GetValue())
                  .ToRList()
                  .ConcatErrors(@this.SelectMany(result => result.GetErrors()));

    }
}