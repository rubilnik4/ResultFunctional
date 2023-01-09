using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Result error action extension methods
    /// </summary>
    public static class ResultErrorVoidExtensions
    {
        /// <summary>
        /// Execute action if result hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static IResultError ResultErrorVoidOk(this IResultError @this, Action action) =>
            @this.
            VoidOk(_ => @this.OkStatus,
                   _ => action.Invoke());

        /// <summary>
        /// Execute action if result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static IResultError ResultErrorVoidBad(this IResultError @this,
                                                      Action<IReadOnlyCollection<IRError>> action) =>
            @this.
            VoidOk(_ => @this.HasErrors,
                   _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Execute action depending on result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionOk">Action if result hasn't errors</param>
        /// <param name="actionBad">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>
        public static IResultError ResultErrorVoidOkBad(this IResultError @this,
                                                        Action actionOk,
                                                        Action<IReadOnlyCollection<IRError>> actionBad) =>
            @this.
            VoidWhere(_ => @this.OkStatus,
                      _ => actionOk.Invoke(),
                      _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Execute action depending on result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>
        public static IResultError ResultErrorVoidOkWhere(this IResultError @this,
                                                          Func<bool> predicate,
                                                          Action action) =>
            @this.
            VoidOk(_ => @this.OkStatus && predicate(),
                   _ => action.Invoke());
    }
}