using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Result collection async action extension methods
    /// </summary>
    public static class ResultCollectionVoidAsyncExtensions
    {
        /// <summary>
        /// Execute async action if result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                 Func<IReadOnlyCollection<TValue>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus,
                action: _ => action.Invoke(@this.Value));

        /// <summary>
        /// Execute async action if result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>  
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                  Func<IReadOnlyCollection<IErrorResult>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.HasErrors,
                action: _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Execute async action depending on result collection errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="actionOk">Action if result collection hasn't errors</param>
        /// <param name="actionBad">Action if result collection has errors</param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                                   Func<IReadOnlyCollection<TValue>, Task> actionOk,
                                                                                                   Func<IReadOnlyCollection<IErrorResult>, Task> actionBad) =>
            await @this.
            VoidWhereAsync(_ => @this.OkStatus,
                actionOk: _ => actionOk.Invoke(@this.Value),
                actionBad: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Execute async action depending on result collection errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkWhereAsync<TValue>(this IResultCollection<TValue> @this,
                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                          Func<IReadOnlyCollection<TValue>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus && predicate(@this.Value),
                action: _ => action.Invoke(@this.Value));
    }
}