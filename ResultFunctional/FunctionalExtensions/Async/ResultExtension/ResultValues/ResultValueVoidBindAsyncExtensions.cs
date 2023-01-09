using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Task result value async action extension methods
    /// </summary>
    public static class ResultCollectionVoidBindAsyncExtensions
    {
        /// <summary>
        /// Execute async action if task result value hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueVoidOkBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                     Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueVoidOkAsync(action));

        /// <summary>
        /// Execute async action if task result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns> 
        public static async Task<IResultValue<TValue>> ResultValueVoidBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                      Func<IReadOnlyCollection<IRError>, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueVoidBadAsync(action));

        /// <summary>
        /// Execute async action depending on task result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionOk">Action if result value hasn't errors</param>
        /// <param name="actionBad">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>  
        public static async Task<IResultValue<TValue>> ResultValueVoidOkBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                             Func<TValue, Task> actionOk,
                                                                                             Func<IReadOnlyCollection<IRError>, Task> actionBad) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueVoidOkBadAsync(actionOk, actionBad));

        /// <summary>
        /// Execute async action depending on task result value errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result value</returns>   
        public static async Task<IResultValue<TValue>> ResultValueVoidOkWhereBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                          Func<TValue, bool> predicate,
                                                                                          Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueVoidOkWhereAsync(predicate, action));
    }
}