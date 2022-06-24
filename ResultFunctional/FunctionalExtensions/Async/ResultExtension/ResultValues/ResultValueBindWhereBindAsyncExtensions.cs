using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task result value async monad function with conditions
    /// </summary>
    public static class ResultValueBindWhereBindAsyncExtensions
    {
        /// <summary>
        /// Execute task monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>       
        public static async Task<IResultValue<TValueOut>> ResultValueBindContinueBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                            Func<TValueIn, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindContinueAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>          
        public static async Task<IResultValue<TValueOut>> ResultValueBindWhereBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, bool> predicate,
                                                                                                             Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                             Func<TValueIn, Task<IResultValue<TValueOut>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindWhereAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task monad result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkBadBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                             Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValueOut>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindOkBadAsync(okFunc, badFunc));

        /// <summary>
        /// Execute task monad result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                          Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindOkAsync(okFunc));

        /// <summary>
        /// Execute task monad result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueBindBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                           Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValue>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindBadAsync(badFunc));

        /// <summary>
        /// Adding errors async to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueBindErrorsOkBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                            Func<TValue, Task<IResultError>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindErrorsOkAsync(okFunc));
    }
}