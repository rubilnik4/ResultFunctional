using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
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
        public static async Task<IRValue<TValueOut>> ResultValueBindContinueBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                           Func<TValueIn, bool> predicate,
                                                                                                           Func<TValueIn, Task<IRValue<TValueOut>>> okFunc,
                                                                                                           Func<TValueIn, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
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
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>       
        public static async Task<IRValue<TValueOut>> ResultValueBindContinueBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IRValue<TValueOut>>> okFunc,
                                                                                                            Func<TValueIn, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultValueBindContinueBindAsync(predicate, okFunc,
                                                         values => badFunc(values).GetCollectionTaskAsync());

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
        public static async Task<IRValue<TValueOut>> ResultValueBindWhereBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, bool> predicate,
                                                                                                             Func<TValueIn, Task<IRValue<TValueOut>>> okFunc,
                                                                                                             Func<TValueIn, Task<IRValue<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
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
        public static async Task<IRValue<TValueOut>> ResultValueBindOkBadBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, Task<IRValue<TValueOut>>> okFunc,
                                                                                                             Func<IReadOnlyCollection<IRError>, Task<IRValue<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
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
        public static async Task<IRValue<TValueOut>> ResultValueBindOkBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                          Func<TValueIn, Task<IRValue<TValueOut>>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindOkAsync(okFunc));

        /// <summary>
        /// Execute task monad result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindBadBindAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                           Func<IReadOnlyCollection<IRError>, Task<IRValue<TValue>>> badFunc)
            where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindBadAsync(badFunc));

        /// <summary>
        /// Adding errors async to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindErrorsOkBindAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                           Func<TValue, Task<IROption>> okFunc)
            where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBindErrorsOkAsync(okFunc));
    }
}