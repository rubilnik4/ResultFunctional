using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units
{
    /// <summary>
    /// Result error action extension methods
    /// </summary>
    public static class RUnitVoidExtensions
    {
        /// <summary>
        /// Execute action if result hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static IRUnit RUnitVoidSome(this IRUnit @this, Action action) =>
            @this.
            VoidSome(_ => @this.Success,
                   _ => action.Invoke());

        /// <summary>
        /// Execute action if result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static IRUnit RUnitVoidNone(this IRUnit @this,
                                                      Action<IReadOnlyCollection<IRError>> action) =>
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
        public static IRUnit RUnitVoidMatch(this IRUnit @this,
                                                        Action actionSome,
                                                        Action<IReadOnlyCollection<IRError>> actionNone) =>
            @this.
            VoidMatch(_ => @this.Success,
                      _ => actionSome.Invoke(),
                      _ => actionNone.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>
        public static IRUnit RUNitVoidWhere(this IRUnit @this,
                                                          Func<bool> predicate,
                                                          Action action) =>
            @this.
            VoidSome(_ => @this.Success && predicate(),
                   _ => action.Invoke());
    }
}