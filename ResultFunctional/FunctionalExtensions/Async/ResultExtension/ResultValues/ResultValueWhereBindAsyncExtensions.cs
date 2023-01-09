using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task result value async functor function with conditions
    /// </summary>
    public static class ResultValueWhereTaskBindAsyncExtensions
    {
        /// <summary>
        /// Execute task result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IResultValue<TValueOut>> ResultValueContinueBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                            Func<TValueIn, Task<IReadOnlyCollection<IRError>>> badFunc) =>
            await @this.ResultValueContinueBindAsync(predicate, okFunc,
                                                     values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute task result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IResultValue<TValueOut>> ResultValueContinueBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                            Func<TValueIn, IEnumerable<IRError>> badFunc) =>
            await @this.ResultValueContinueBindAsync(predicate, okFunc,
                                                     values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute task result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IResultValue<TValueOut>> ResultValueContinueBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                            Func<TValueIn, Task<IEnumerable<IRError>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueContinueAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task result value async function base on predicate condition returning value in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IResultValue<TValueOut>> ResultValueWhereBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, bool> predicate,
                                                                                                         Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                         Func<TValueIn, Task<TValueOut>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueWhereAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IResultValue<TValueOut>> ResultValueOkBadBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, Task<TValueOut>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueOkBadAsync(okFunc, badFunc));

        /// <summary>
        /// Execute task result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IResultValue<TValueOut>> ResultValueOkBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                      Func<TValueIn, Task<TValueOut>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueOkAsync(okFunc));

        /// <summary>
        /// Execute task result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IRError>, Task<TValue>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBadAsync(badFunc));

        /// <summary>
        /// Check errors by predicate async to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueCheckErrorsOkBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                           Func<TValue, bool> predicate,
                                                                           Func<TValue, Task<IReadOnlyCollection<IRError>>> badFunc) =>
              await @this.ResultValueCheckErrorsOkBindAsync(predicate, 
                                                       values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Check errors by predicate async to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueCheckErrorsOkBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                           Func<TValue, bool> predicate,
                                                                           Func<TValue, Task<IEnumerable<IRError>>> badFunc) =>
             await @this.
             MapBindAsync(awaitedThis => awaitedThis.ResultValueCheckErrorsOkAsync(predicate, badFunc));
    }
}