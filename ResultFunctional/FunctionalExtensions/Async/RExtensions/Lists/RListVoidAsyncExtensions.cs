﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Result collection async action extension methods
    /// </summary>
    public static class RListVoidAsyncExtensions
    {
        /// <summary>
        /// Execute async action if result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IRList<TValue>> RListVoidSomeAsync<TValue>(this IRList<TValue> @this,
                                                                            Func<IReadOnlyCollection<TValue>, Task> action)
            where TValue : notnull =>
            await @this
               .VoidSomeAsync(_ => @this.Success,
                              _ => action.Invoke(@this.GetValue()));

        /// <summary>
        /// Execute async action if result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>  
        public static async Task<IRList<TValue>> RListVoidNoneAsync<TValue>(this IRList<TValue> @this,
                                                                            Func<IReadOnlyCollection<IRError>, Task> action)
            where TValue : notnull =>
            await @this
               .VoidSomeAsync(_ => @this.Failure,
                              _ => action.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute async action depending on result collection errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="actionSome">Action if result collection hasn't errors</param>
        /// <param name="actionNone">Action if result collection has errors</param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IRList<TValue>> RListVoidMatchAsync<TValue>(this IRList<TValue> @this,
                                                                             Func<IReadOnlyCollection<TValue>, Task> actionSome,
                                                                             Action<IReadOnlyCollection<IRError>> actionNone)
            where TValue : notnull =>
            await @this
               .VoidOptionAsync(_ => @this.Success,
                                _ => actionSome.Invoke(@this.GetValue()),
                                _ => actionNone.ToTask().Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute async action depending on result collection errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="actionSome">Action if result collection hasn't errors</param>
        /// <param name="actionNone">Action if result collection has errors</param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IRList<TValue>> RListVoidMatchAsync<TValue>(this IRList<TValue> @this,
                                                                             Func<IReadOnlyCollection<TValue>, Task> actionSome,
                                                                             Func<IReadOnlyCollection<IRError>, Task> actionNone)
            where TValue : notnull =>
            await @this
               .VoidOptionAsync(_ => @this.Success,
                                _ => actionSome.Invoke(@this.GetValue()),
                                _ => actionNone.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute async action depending on result collection errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IRList<TValue>> RListVoidOptionAsync<TValue>(this IRList<TValue> @this,
                                                                              Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                              Func<IReadOnlyCollection<TValue>, Task> action)
            where TValue : notnull =>
            await @this
               .VoidSomeAsync(_ => @this.Success && predicate(@this.GetValue()),
                              _ => action.Invoke(@this.GetValue()));
    }
}