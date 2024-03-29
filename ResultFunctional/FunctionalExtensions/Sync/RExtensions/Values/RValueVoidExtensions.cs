﻿using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Result value action extension methods
    /// </summary>
    public static class RValueVoidExtensions
    {
        /// <summary>
        /// Execute action if result value hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>
        public static IRValue<TValue> RValueVoidSome<TValue>(this IRValue<TValue> @this, Action<TValue> action)
             where TValue : notnull =>
            @this.
            VoidSome(_ => @this.Success,
                _ => action.Invoke(@this.GetValue()));

        /// <summary>
        /// Execute action if result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns> 
        public static IRValue<TValue> RValueVoidNone<TValue>(this IRValue<TValue> @this,
                                                             Action<IReadOnlyCollection<IRError>> action)
             where TValue : notnull =>
            @this.
            VoidSome(_ => @this.Failure,
                _ => action.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionSome">Action if result value hasn't errors</param>
        /// <param name="actionNone">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>
        public static IRValue<TValue> RValueVoidMatch<TValue>(this IRValue<TValue> @this,
                                                              Action<TValue> actionSome,
                                                              Action<IReadOnlyCollection<IRError>> actionNone)
             where TValue : notnull =>
            @this.
            VoidOption(_ => @this.Success,
                _ => actionSome.Invoke(@this.GetValue()),
                _ => actionNone.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result value errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result value</returns>  
        public static IRValue<TValue> RValueVoidOption<TValue>(this IRValue<TValue> @this, Func<TValue, bool> predicate,
                                                              Action<TValue> action)
             where TValue : notnull =>
            @this.
            VoidSome(_ => @this.Success && predicate(@this.GetValue()),
                     _ => action.Invoke(@this.GetValue()));
    }
}