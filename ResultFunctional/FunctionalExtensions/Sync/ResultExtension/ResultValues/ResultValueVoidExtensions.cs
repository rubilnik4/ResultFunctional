using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Result value action extension methods
    /// </summary>
    public static class ResultValueVoidExtensions
    {
        /// <summary>
        /// Execute action if result value hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>
        public static IResultValue<TValue> ResultValueVoidOk<TValue>(this IResultValue<TValue> @this, Action<TValue> action) =>
            @this.
            VoidOk(_ => @this.OkStatus,
                _ => action.Invoke(@this.Value));

        /// <summary>
        /// Execute action if result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns> 
        public static IResultValue<TValue> ResultValueVoidBad<TValue>(this IResultValue<TValue> @this,
                                                                      Action<IReadOnlyCollection<IRError>> action) =>
            @this.
            VoidOk(_ => @this.HasErrors,
                _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Execute action depending on result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionOk">Action if result value hasn't errors</param>
        /// <param name="actionBad">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>
        public static IResultValue<TValue> ResultValueVoidOkBad<TValue>(this IResultValue<TValue> @this, 
                                                                        Action<TValue> actionOk,
                                                                        Action<IReadOnlyCollection<IRError>> actionBad) =>
            @this.
            VoidWhere(_ => @this.OkStatus,
                _ => actionOk.Invoke(@this.Value),
                _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Execute action depending on result value errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result value</returns>  
        public static IResultValue<TValue> ResultValueVoidOkWhere<TValue>(this IResultValue<TValue> @this,
                                                                     Func<TValue, bool> predicate,
                                                                     Action<TValue> action) =>
            @this.
            VoidOk(_ => @this.OkStatus && predicate(@this.Value),
                _ => action.Invoke(@this.Value));
    }
}