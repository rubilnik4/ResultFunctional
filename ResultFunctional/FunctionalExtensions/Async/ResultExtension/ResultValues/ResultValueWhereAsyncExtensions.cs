using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value async functor function with conditions
    /// </summary>
    public static class ResultValueWhereAsyncExtensions
    {
        /// <summary>
        /// Execute result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueContinueAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                        Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                        Func<TValueIn, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
            await @this.ResultValueContinueAsync(predicate, okFunc,
                                                 values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueContinueAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                        Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                        Func<TValueIn, Task<IEnumerable<IErrorResult>>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultValue<TValueOut>(await okFunc.Invoke(@this.Value))
                 : new ResultValue<TValueOut>(await badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result value async function base on predicate condition returning value in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IResultValue<TValueOut>> ResultValueWhereAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                     Func<TValueIn, bool> predicate,
                                                                                                     Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                     Func<TValueIn, Task<TValueOut>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultValue<TValueOut>(await okFunc.Invoke(@this.Value))
                 : new ResultValue<TValueOut>(await badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IResultValue<TValueOut>> ResultValueOkBadAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                     Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                     Func<IReadOnlyCollection<IErrorResult>, Task<TValueOut>> badFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(await okFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(await badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Execute result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IResultValue<TValueOut>> ResultValueOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<TValueOut>> okFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(await okFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueBadAsync<TValue>(this IResultValue<TValue> @this,
                                                                                   Func<IReadOnlyCollection<IErrorResult>, Task<TValue>> badFunc) =>

            @this.OkStatus
                ? @this
                : new ResultValue<TValue>(await badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Check errors by predicate async to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueCheckErrorsOkAsync<TValue>(this IResultValue<TValue> @this,
                                                                           Func<TValue, bool> predicate,
                                                                           Func<TValue, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
             await @this.ResultValueCheckErrorsOkAsync(predicate,
                                                  values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Check errors by predicate async to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueCheckErrorsOkAsync<TValue>(this IResultValue<TValue> @this,
                                                                           Func<TValue, bool> predicate,
                                                                           Func<TValue, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.
            ResultValueContinueAsync(predicate,
                                     Task.FromResult,
                                     badFunc);
    }
}