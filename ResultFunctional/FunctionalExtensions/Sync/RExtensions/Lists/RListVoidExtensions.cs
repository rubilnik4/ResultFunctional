using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
{
    /// <summary>
    /// Result collection action extension methods
    /// </summary>
    public static class RListVoidExtensions
    {
        /// <summary>
        /// Execute action if result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>
        public static IRList<TValue> RListVoidSome<TValue>(this IRList<TValue> @this, Action<IReadOnlyCollection<TValue>> action) 
            where TValue : notnull =>
            @this.
            VoidSome(_ => @this.Success,
                   _ => action.Invoke(@this.GetValue()));

        /// <summary>
        /// Execute action if result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>
        public static IRList<TValue> RListVoidNone<TValue>(this IRList<TValue> @this, Action<IReadOnlyCollection<IRError>> action)
            where TValue : notnull =>
            @this.
            VoidSome(_ => @this.Failure,
                   _ => action.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result collection errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="actionSome">Action if result collection hasn't errors</param>
        /// <param name="actionNone">Action if result collection has errors</param>
        /// <returns>Unchanged result collection</returns>
        public static IRList<TValue> RListVoidMatch<TValue>(this IRList<TValue> @this,
                                                            Action<IReadOnlyCollection<TValue>> actionSome,
                                                            Action<IReadOnlyCollection<IRError>> actionNone)
            where TValue : notnull =>
            @this.
            VoidOption(_ => @this.Success,
                      _ => actionSome.Invoke(@this.GetValue()),
                      _ => actionNone.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result collection errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result collection</returns>
        public static IRList<TValue> RListVoidOption<TValue>(this IRList<TValue> @this,
                                                            Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                            Action<IReadOnlyCollection<TValue>> action)
            where TValue : notnull =>
            @this.
            VoidSome(_ => @this.Success && predicate(@this.GetValue()),
                   _ => action.Invoke(@this.GetValue()));
    }
}