using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Units
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
        public static IRUnit ResultErrorVoidOk(this IRUnit @this, Action action) =>
            @this.
            VoidOk(_ => @this.Success,
                   _ => action.Invoke());

        /// <summary>
        /// Execute action if result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static IRUnit ResultErrorVoidBad(this IRUnit @this,
                                                      Action<IReadOnlyCollection<IRError>> action) =>
            @this.
            VoidOk(_ => @this.Failure,
                   _ => action.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionOk">Action if result hasn't errors</param>
        /// <param name="actionBad">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>
        public static IRUnit ResultErrorVoidOkBad(this IRUnit @this,
                                                        Action actionOk,
                                                        Action<IReadOnlyCollection<IRError>> actionBad) =>
            @this.
            VoidWhere(_ => @this.Success,
                      _ => actionOk.Invoke(),
                      _ => actionBad.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>
        public static IRUnit ResultErrorVoidOkWhere(this IRUnit @this,
                                                          Func<bool> predicate,
                                                          Action action) =>
            @this.
            VoidOk(_ => @this.Success && predicate(),
                   _ => action.Invoke());
    }
}