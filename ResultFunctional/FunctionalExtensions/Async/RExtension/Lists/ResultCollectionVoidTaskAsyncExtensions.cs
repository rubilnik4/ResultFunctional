﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
{
    /// <summary>
    /// Task result collection async action extension methods
    /// </summary>
    public static class ResultCollectionVoidTaskAsyncExtensions
    {
        /// <summary>
        /// Execute action if task result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>     
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                     Action<IReadOnlyCollection<TValue>> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidOk(action));

        /// <summary>
        /// Execute action if task result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>  
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidBadTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                      Action<IReadOnlyCollection<IRError>> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidBad(action));

        /// <summary>
        /// Execute action depending on task result collection errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="actionOk">Action if result collection hasn't errors</param>
        /// <param name="actionBad">Action if result collection has errors</param>
        /// <returns>Unchanged result collection</returns>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkBadTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                                     Action<IReadOnlyCollection<TValue>> actionOk,
                                                                                                     Action<IReadOnlyCollection<IRError>> actionBad) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidOkBad(actionOk, actionBad));

        /// <summary>
        /// Execute action depending on task result collection errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkWhereTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                          Action<IReadOnlyCollection<TValue>> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidOkWhere(predicate, action));
    }
}