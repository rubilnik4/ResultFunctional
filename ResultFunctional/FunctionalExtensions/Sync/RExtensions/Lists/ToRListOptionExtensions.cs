using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
{
    /// <summary>
    /// Result collections extension methods with condition
    /// </summary>
    public static class ToRListOptionExtensions
    {
        /// <summary>
        /// Converting collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> ToRListOption<TValue>(this IEnumerable<TValue> @this,
                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                          Func<IReadOnlyCollection<TValue>, IRError> noneFunc)
            where TValue : notnull =>
            @this.ToList()
                 .Option(predicate,
                                values => values.ToRList(),
                                values => noneFunc(values).ToRList<TValue>());
    }
}