using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection monad async function with conditions
    /// </summary>
    public static class ResultCollectionBindWhereAsyncExtensions
    {
        /// <summary>
        /// Execute monad result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindContinueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
            await @this.ResultCollectionBindContinueAsync(predicate, okFunc,
                                                          values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute monad result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindContinueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<IErrorResult>>> badFunc) =>
            @this.OkStatus
                ? predicate(@this.Value)
                    ? await okFunc.Invoke(@this.Value)
                    : new ResultCollection<TValueOut>(await badFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindWhereAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? predicate(@this.Value)
                    ? await okFunc.Invoke(@this.Value)
                    : await badFunc.Invoke(@this.Value)
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindOkBadAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc,
                                                                                                                   Func<IReadOnlyCollection<IErrorResult>, Task<IResultCollection<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : await badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Execute monad result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                                Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                                 Func<IReadOnlyCollection<IErrorResult>, Task<IResultCollection<TValue>>> badFunc) =>
            @this.OkStatus
                ? @this
                : await badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Adding errors async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindErrorsOkAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                                      Func<IReadOnlyCollection<TValue>, Task<IResultError>> okFunc) =>
            await @this.
                ResultCollectionBindOkAsync(collection => okFunc.Invoke(collection).
                                                                 MapTaskAsync(resultError => resultError.ToResultCollection(collection)));
    }
}