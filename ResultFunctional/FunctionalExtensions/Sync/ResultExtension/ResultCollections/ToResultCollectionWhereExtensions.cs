using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
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
        public static IResultCollection<TValue> ToResultCollectionWhere<TValue>(this IEnumerable<TValue> @this,
                                                                                Func<IEnumerable<TValue>, bool> predicate,
                                                                                Func<IEnumerable<TValue>, IErrorResult> badFunc)
            where TValue : notnull =>
            @this.WhereContinue(predicate,
                                values => new ResultCollection<TValue>(values),
                                values => new ResultCollection<TValue>(badFunc(values)));
    }
}