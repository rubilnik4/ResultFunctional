using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Task result value action extension methods
    /// </summary>
    public static class ResultCollectionVoidTaskAsyncExtensions
    {
        /// <summary>
        /// Execute action if task result value hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueVoidOkTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                          Action<TValue> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueVoidOk(action));

        /// <summary>
        /// Execute action if task result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>      
        public static async Task<IResultValue<TValue>> ResultValueVoidBadTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                      Action<IReadOnlyCollection<IRError>> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueVoidBad(action));

        /// <summary>
        /// Execute action depending on task result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionOk">Action if result value hasn't errors</param>
        /// <param name="actionBad">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>  
        public static async Task<IResultValue<TValue>> ResultValueVoidOkBadTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                           Action<TValue> actionOk,
                                                                                           Action<IReadOnlyCollection<IRError>> actionBad) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueVoidOkBad(actionOk, actionBad));

        /// <summary>
        /// Execute action depending on task result value errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result value</returns>   
        public static async Task<IResultValue<TValue>> ResultValueVoidOkWhereTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                          Func<TValue, bool> predicate,
                                                                                          Action<TValue> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueVoidOkWhere(predicate, action));
    }
}