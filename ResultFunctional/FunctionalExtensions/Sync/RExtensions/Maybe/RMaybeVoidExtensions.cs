using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe
{
    /// <summary>
    /// Result error action extension methods
    /// </summary>
    public static class RMaybeVoidExtensions
    {
        /// <summary>
        /// Execute action if result hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static IRMaybe RMaybeVoidSome(this IRMaybe @this, Action action) =>
            @this.
            VoidSome(_ => @this.Success,
                   _ => action.Invoke());

        /// <summary>
        /// Execute action if result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static IRMaybe RMaybeVoidNone(this IRMaybe @this, Action<IReadOnlyCollection<IRError>> action) =>
            @this.
            VoidSome(_ => @this.Failure,
                   _ => action.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionSome">Action if result hasn't errors</param>
        /// <param name="actionNone">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>
        public static IRMaybe RMaybeVoidMatch(this IRMaybe @this, Action actionSome,
                                              Action<IReadOnlyCollection<IRError>> actionNone) =>
            @this.
            VoidOption(_ => @this.Success,
                      _ => actionSome.Invoke(),
                      _ => actionNone.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>
        public static IRMaybe RMaybeVoidOption(this IRMaybe @this, Func<bool> predicate, Action action) =>
            @this.
            VoidSome(_ => @this.Success && predicate(),
                     _ => action.Invoke());
    }
}