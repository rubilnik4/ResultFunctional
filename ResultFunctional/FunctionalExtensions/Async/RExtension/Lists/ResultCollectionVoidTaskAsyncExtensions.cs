using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

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
        public static async Task<IRList<TValue>> ResultCollectionVoidOkTaskAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                         Action<IReadOnlyCollection<TValue>> action)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidOk(action));

        /// <summary>
        /// Execute action if task result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>  
        public static async Task<IRList<TValue>> ResultCollectionVoidBadTaskAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                      Action<IReadOnlyCollection<IRError>> action)
            where TValue : notnull =>
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
        public static async Task<IRList<TValue>> ResultCollectionVoidOkBadTaskAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                                     Action<IReadOnlyCollection<TValue>> actionOk,
                                                                                                     Action<IReadOnlyCollection<IRError>> actionBad)
            where TValue : notnull =>
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
        public static async Task<IRList<TValue>> ResultCollectionVoidOkWhereTaskAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                          Action<IReadOnlyCollection<TValue>> action)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidOkWhere(predicate, action));
    }
}