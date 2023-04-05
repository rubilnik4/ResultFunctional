using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists
{
    /// <summary>
    /// Result collection action extension methods
    /// </summary>
    public static class ResultCollectionVoidExtensions
    {
        /// <summary>
        /// Execute action if result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>
        public static IRList<TValue> ResultCollectionVoidOk<TValue>(this IRList<TValue> @this,
                                                                    Action<IReadOnlyCollection<TValue>> action) 
            where TValue : notnull =>
            @this.
            VoidOk(_ => @this.Success,
                   _ => action.Invoke(@this.GetValue()));

        /// <summary>
        /// Execute action if result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>
        public static IRList<TValue> ResultCollectionVoidBad<TValue>(this IRList<TValue> @this,
                                                                                Action<IReadOnlyCollection<IRError>> action)
            where TValue : notnull =>
            @this.
            VoidOk(_ => @this.Failure,
                   _ => action.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result collection errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="actionOk">Action if result collection hasn't errors</param>
        /// <param name="actionBad">Action if result collection has errors</param>
        /// <returns>Unchanged result collection</returns>
        public static IRList<TValue> ResultCollectionVoidOkBad<TValue>(this IRList<TValue> @this,
                                                                                  Action<IReadOnlyCollection<TValue>> actionOk,
                                                                                  Action<IReadOnlyCollection<IRError>> actionBad)
            where TValue : notnull =>
            @this.
            VoidWhere(_ => @this.Success,
                      _ => actionOk.Invoke(@this.GetValue()),
                      _ => actionBad.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute action depending on result collection errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result collection</returns>
        public static IRList<TValue> ResultCollectionVoidOkWhere<TValue>(this IRList<TValue> @this,
                                                                                    Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                    Action<IReadOnlyCollection<TValue>> action)
            where TValue : notnull =>
            @this.
            VoidOk(_ => @this.Success && predicate(@this.GetValue()),
                   _ => action.Invoke(@this.GetValue()));
    }
}