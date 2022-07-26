using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection async functor function with conditions
    /// </summary>
    public static class ResultCollectionWhereAsyncExtensions
    {
        /// <summary>
        /// Execute result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionContinueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
            await @this.ResultCollectionContinueAsync(predicate,
                                                      values => okFunc(values).GetEnumerableTaskAsync(),
                                                      values => badFunc(values).GetEnumerableTaskAsync());
        /// <summary>
        /// Execute result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionContinueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<IErrorResult>>> badFunc) =>
              @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultCollection<TValueOut>(await okFunc.Invoke(@this.Value))
                 : new ResultCollection<TValueOut>(await badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionWhereAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> badFunc) =>
            await @this.ResultCollectionWhereAsync(predicate,
                                                      values => okFunc(values).GetEnumerableTaskAsync(),
                                                      values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionWhereAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultCollection<TValueOut>(await okFunc.Invoke(@this.Value))
                 : new ResultCollection<TValueOut>(await badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkBadAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                              Func<IReadOnlyCollection<IErrorResult>, Task<IReadOnlyCollection<TValueOut>>> badFunc) =>
             await @this.ResultCollectionOkBadAsync(values => okFunc(values).GetEnumerableTaskAsync(),
                                                    values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkBadAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                              Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? new ResultCollection<TValueOut>(await okFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(await badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Execute result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc) =>
             await @this.ResultCollectionOkAsync(values => okFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc) =>
            @this.OkStatus
                ? new ResultCollection<TValueOut>(await okFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                            Func<IReadOnlyCollection<IErrorResult>, Task<IReadOnlyCollection<TValue>>> badFunc) =>

            await @this.ResultCollectionBadAsync(values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                            Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValue>>> badFunc) =>

            @this.OkStatus
                ? @this
                : new ResultCollection<TValue>(await badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Check errors by predicate async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionCheckErrorsOkAsync<TValue>(this IResultCollection<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
            await @this.ResultCollectionCheckErrorsOkAsync(predicate, values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Check errors by predicate async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionCheckErrorsOkAsync<TValue>(this IResultCollection<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.
            ResultCollectionContinueAsync(predicate,
                                          collection => Task.FromResult((IEnumerable<TValue>)collection),
                                          badFunc);
    }
}