using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Task result collections extension methods with condition
    /// </summary>
    public static class ToResultCollectionWhereAsyncExtensions
    {
        /// <summary>
        /// Converting task collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ToResultCollectionWhereTaskAsync<TValue>(this Task<IEnumerable<TValue>> @this,
                                                                                            Func<IEnumerable<TValue>, bool> predicate,
                                                                                            Func<IEnumerable<TValue>, IRError> badFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultCollectionWhere(predicate, badFunc));

        /// <summary>
        /// Converting task collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ToResultCollectionWhereTaskAsync<TValue>(this Task<IReadOnlyCollection<TValue>> @this,
                                                                                            Func<IEnumerable<TValue>, bool> predicate,
                                                                                            Func<IEnumerable<TValue>, IRError> badFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(resultCollection => (IEnumerable<TValue>)resultCollection).
            ToResultCollectionWhereTaskAsync(predicate, badFunc);
    }
}