using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
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
        public static IResultCollection<TValue> ResultCollectionVoidOk<TValue>(this IResultCollection<TValue> @this,
                                                                               Action<IReadOnlyCollection<TValue>> action) =>
            @this.
            VoidOk(_ => @this.OkStatus,
                   _ => action.Invoke(@this.Value));

        /// <summary>
        /// Execute action if result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>
        public static IResultCollection<TValue> ResultCollectionVoidBad<TValue>(this IResultCollection<TValue> @this,
                                                                                Action<IReadOnlyCollection<IRError>> action) =>
            @this.
            VoidOk(_ => @this.HasErrors,
                   _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Execute action depending on result collection errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="actionOk">Action if result collection hasn't errors</param>
        /// <param name="actionBad">Action if result collection has errors</param>
        /// <returns>Unchanged result collection</returns>
        public static IResultCollection<TValue> ResultCollectionVoidOkBad<TValue>(this IResultCollection<TValue> @this,
                                                                                  Action<IReadOnlyCollection<TValue>> actionOk,
                                                                                  Action<IReadOnlyCollection<IRError>> actionBad) =>
            @this.
            VoidWhere(_ => @this.OkStatus,
                      _ => actionOk.Invoke(@this.Value),
                      _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Execute action depending on result collection errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result collection</returns>
        public static IResultCollection<TValue> ResultCollectionVoidOkWhere<TValue>(this IResultCollection<TValue> @this,
                                                                                    Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                    Action<IReadOnlyCollection<TValue>> action) =>
            @this.
            VoidOk(_ => @this.OkStatus && predicate(@this.Value),
                   _ => action.Invoke(@this.Value));
    }
}