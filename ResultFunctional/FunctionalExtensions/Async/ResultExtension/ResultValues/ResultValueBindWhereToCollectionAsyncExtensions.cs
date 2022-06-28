﻿using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value async monad function converting to result collection
    /// </summary>
    public static class ResultValueBindWhereToCollectionAsyncExtensions
    {
        /// <summary>
        /// Execute monad result value async function converting to result collection if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultValueBindOkToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<IResultCollection<TValueOut>>> okFunc) =>
            await @this.
            ResultValueBindOkAsync(valueIn => okFunc(valueIn).
                                              MapAsync(resultCollection => resultCollection.ToResultValueTaskAsync())).
            ToResultCollectionTaskAsync();
    }
}