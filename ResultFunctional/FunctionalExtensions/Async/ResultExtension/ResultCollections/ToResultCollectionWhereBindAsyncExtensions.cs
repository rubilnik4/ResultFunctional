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
    /// Result collections task async extension methods with condition
    /// </summary>
    public static class ToResultCollectionWhereBindAsyncExtensions
    {
        /// <summary>
        /// Async converting task collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ToResultCollectionWhereBindAsync<TValue>(this Task<IEnumerable<TValue>> @this,
                                                                                            Func<IEnumerable<TValue>, bool> predicate,
                                                                                            Func<IEnumerable<TValue>, Task<IRError>> badFunc)
            where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ToResultCollectionWhereAsync(predicate, badFunc));

        /// <summary>
        /// Async converting task collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ToResultCollectionWhereBindAsync<TValue>(this Task<IReadOnlyCollection<TValue>> @this,
                                                                                            Func<IEnumerable<TValue>, bool> predicate,
                                                                                            Func<IEnumerable<TValue>, Task<IRError>> badFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(resultCollection => (IEnumerable<TValue>)resultCollection).
            ToResultCollectionWhereBindAsync(predicate, badFunc);
    }
}