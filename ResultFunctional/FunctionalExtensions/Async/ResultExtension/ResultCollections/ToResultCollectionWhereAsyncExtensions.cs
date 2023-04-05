using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Result collections async extension methods with condition
    /// </summary>
    public static class ToResultCollectionWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Async converting collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ToResultCollectionWhereAsync<TValue>(this IEnumerable<TValue> @this,
                                                                                            Func<IEnumerable<TValue>, bool> predicate,
                                                                                            Func<IEnumerable<TValue>, Task<IRError>> badFunc)
            where TValue : notnull =>
            await @this.WhereContinueAsync(predicate,
                                           value => Task.FromResult(new ResultCollection<TValue>(value)),
                                           value => badFunc(value).
                                                    MapTaskAsync(error => new ResultCollection<TValue>(error)));
    }
}