using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value async monad function with conditions
    /// </summary>
    public static class ResultCollectionBindWhereAsyncExtensions
    {
        /// <summary>
        /// Execute monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IResultValue<TValueOut>> ResultValueBindContinueAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                            Func<TValueIn, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
            await @this.ResultValueBindContinueAsync(predicate, okFunc,
                                                     values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IResultValue<TValueOut>> ResultValueBindContinueAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                            Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
            await @this.ResultValueBindContinueAsync(predicate, okFunc,
                                                     values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IResultValue<TValueOut>> ResultValueBindContinueAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                            Func<TValueIn, Task<IEnumerable<IErrorResult>>> badFunc) =>
            @this.OkStatus
                ? predicate(@this.Value)
                    ? await okFunc.Invoke(@this.Value)
                    : new ResultValue<TValueOut>(await badFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IResultValue<TValueOut>> ResultValueBindWhereAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                         Func<TValueIn, bool> predicate,
                                                                                                         Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                         Func<TValueIn, Task<IResultValue<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? predicate(@this.Value)
                    ? await okFunc.Invoke(@this.Value)
                    : await badFunc.Invoke(@this.Value)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkBadAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                         Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : await badFunc.Invoke(@this.Errors);


        /// <summary>
        /// Execute monad result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                      Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueBindBadAsync<TValue>(this IResultValue<TValue> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValue>>> badFunc) =>
            @this.OkStatus
                ? @this
                : await badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Adding errors async to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueBindErrorsOkAsync<TValue>(this IResultValue<TValue> @this,
                                                                                            Func<TValue, Task<IResultError>> okFunc) =>
            await @this.
            ResultValueBindOkAsync(value => okFunc.Invoke(value).
                                            MapTaskAsync(resultError => resultError.ToResultValue(value)));
    }
}