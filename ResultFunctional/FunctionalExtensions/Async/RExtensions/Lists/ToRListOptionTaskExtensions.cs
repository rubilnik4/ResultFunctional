﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Task result collections extension methods with condition
    /// </summary>
    public static class ToRListOptionTaskExtensions
    {
        /// <summary>
        /// Converting task collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ToRListOptionTask<TValue>(this Task<IEnumerable<TValue>> @this,
                                                                           Func<IEnumerable<TValue>, bool> predicate,
                                                                           Func<IEnumerable<TValue>, IRError> noneFunc)
            where TValue : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.ToRListOption(predicate, noneFunc));

        /// <summary>
        /// Converting task collection to result collection base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ToRListOptionTask<TValue>(this Task<IReadOnlyCollection<TValue>> @this,
                                                                           Func<IEnumerable<TValue>, bool> predicate,
                                                                           Func<IEnumerable<TValue>, IRError> noneFunc)
            where TValue : notnull =>
            await @this.
                  MapTask(resultCollection => (IEnumerable<TValue>)resultCollection).
                  ToRListOptionTask(predicate, noneFunc);
    }
}