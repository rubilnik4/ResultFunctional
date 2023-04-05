using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Result value async action extension methods
    /// </summary>
    public static class ResultValueVoidAsyncExtensions
    {
        /// <summary>
        /// Execute async action if result value hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueVoidOkAsync<TValue>(this IResultValue<TValue> @this,
                                                                                 Func<TValue, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus,
                action: _ => action.Invoke(@this.Value));

        /// <summary>
        /// Execute async action if result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns> 
        public static async Task<IResultValue<TValue>> ResultValueVoidBadAsync<TValue>(this IResultValue<TValue> @this,
                                                                                       Func<IReadOnlyCollection<IRError>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.HasErrors,
                action: _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Execute async action depending on result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionOk">Action if result value hasn't errors</param>
        /// <param name="actionBad">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueVoidOkBadAsync<TValue>(this IResultValue<TValue> @this,
                                                                                         Func<TValue, Task> actionOk,
                                                                                         Func<IReadOnlyCollection<IRError>, Task> actionBad) =>
            await @this.
            VoidWhereAsync(_ => @this.OkStatus,
                actionOk: _ => actionOk.Invoke(@this.Value),
                actionBad: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Execute async action depending on result value errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result value</returns>   
        public static async Task<IResultValue<TValue>> ResultValueVoidOkWhereAsync<TValue>(this IResultValue<TValue> @this,
                                                                                           Func<TValue, bool> predicate,
                                                                                           Func<TValue, Task> action) =>
            await  @this.
            VoidOkAsync(_ => @this.OkStatus && predicate(@this.Value),
                action: _ => action.Invoke(@this.Value));
    }
}