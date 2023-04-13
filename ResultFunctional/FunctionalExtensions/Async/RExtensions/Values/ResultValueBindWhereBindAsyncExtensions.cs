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
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>       
        public static async Task<IRValue<TValueOut>> ResultValueBindContinueBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                           Func<TValueIn, bool> predicate,
                                                                                                           Func<TValueIn, Task<IRValue<TValueOut>>> someFunc,
                                                                                                           Func<TValueIn, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.ResultValueBindContinueAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>       
        public static async Task<IRValue<TValueOut>> ResultValueBindContinueBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IRValue<TValueOut>>> someFunc,
                                                                                                            Func<TValueIn, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultValueBindContinueBindAsync(predicate, someFunc,
                                                         values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute task monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>          
        public static async Task<IRValue<TValueOut>> ResultValueBindWhereBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, bool> predicate,
                                                                                                             Func<TValueIn, Task<IRValue<TValueOut>>> someFunc,
                                                                                                             Func<TValueIn, Task<IRValue<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultValueBindWhereAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task monad result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IRValue<TValueOut>> ResultValueBindOkBadBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, Task<IRValue<TValueOut>>> someFunc,
                                                                                                             Func<IReadOnlyCollection<IRError>, Task<IRValue<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultValueBindOkBadAsync(someFunc, noneFunc));

        /// <summary>
        /// Execute task monad result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> ResultValueBindOkBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                          Func<TValueIn, Task<IRValue<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultValueBindOkAsync(someFunc));

        /// <summary>
        /// Execute task monad result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="noneFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindBadBindAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                           Func<IReadOnlyCollection<IRError>, Task<IRValue<TValue>>> noneFunc)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultValueBindBadAsync(noneFunc));

        /// <summary>
        /// Adding errors async to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindErrorsOkBindAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                           Func<TValue, Task<IROption>> someFunc)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultValueBindErrorsOkAsync(someFunc));
    }
}