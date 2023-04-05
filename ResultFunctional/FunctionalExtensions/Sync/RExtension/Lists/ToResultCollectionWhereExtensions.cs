using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists
{
    /// <summary>
    /// Result collections extension methods with condition
    /// </summary>
    public static class ToResultCollectionWhereExtensions
    {
        /// <summary>
        /// Converting collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> ToRListWhere<TValue>(this IEnumerable<TValue> @this,
                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                          Func<IReadOnlyCollection<TValue>, IRError> badFunc)
            where TValue : notnull =>
            @this.ToList()
                 .WhereContinue(predicate,
                                values => values.ToRList(),
                                values => badFunc(values).ToRList<TValue>());
    }
}