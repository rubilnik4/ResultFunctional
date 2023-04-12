﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
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
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ToResultCollectionWhereBindAsync<TValue>(this Task<IEnumerable<TValue>> @this,
                                                                                          Func<IEnumerable<TValue>, bool> predicate,
                                                                                          Func<IEnumerable<TValue>, Task<IRError>> noneFunc)
            where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ToResultCollectionWhereAsync(predicate, noneFunc));

        /// <summary>
        /// Async converting task collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ToResultCollectionWhereBindAsync<TValue>(this Task<IReadOnlyCollection<TValue>> @this,
                                                                                            Func<IEnumerable<TValue>, bool> predicate,
                                                                                            Func<IEnumerable<TValue>, Task<IRError>> noneFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(resultCollection => (IEnumerable<TValue>)resultCollection).
            ToResultCollectionWhereBindAsync(predicate, noneFunc);
    }
}