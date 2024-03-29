﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for task result value async functor function with conditions
    /// </summary>
    public static class RValueOptionAwaitExtensions
    {
        /// <summary>
        /// Execute task result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> RValueOptionAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                            Func<TValueIn, Task<bool>> predicate,
                                                                                            Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                            Func<TValueIn, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.MapAwait(awaitedThis => awaitedThis.RValueOptionAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> RValueOptionAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                            Func<TValueIn, bool> predicate,
                                                                                            Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                            Func<TValueIn, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RValueOptionAwait(value => predicate(value).ToTask(), someFunc, noneFunc);

        /// <summary>
        /// Execute task result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> RValueOptionAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                            Func<TValueIn, bool> predicate,
                                                                                            Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                            Func<TValueIn, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RValueOptionAwait(predicate, someFunc, values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute task result value async function base on predicate condition returning value in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IRValue<TValueOut>> RValueWhereAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                           Func<TValueIn, bool> predicate,
                                                                                           Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                           Func<TValueIn, Task<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.MapAwait(awaitedThis => awaitedThis.RValueWhereAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> RValueMatchAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                           Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                           Func<IReadOnlyCollection<IRError>, Task<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RValueMatchAsync(someFunc, noneFunc));

        /// <summary>
        /// Execute task result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IRValue<TValueOut>> RValueSomeAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                          Func<TValueIn, Task<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RValueSomeAsync(someFunc));

        /// <summary>
        /// Execute task result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> RValueNoneAwait<TValue>(this Task<IRValue<TValue>> @this,
                                                                          Func<IReadOnlyCollection<IRError>, Task<TValue>> noneFunc)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RValueNoneAsync(noneFunc));

        /// <summary>
        /// Check errors by predicate async to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> RValueEnsureAwait<TValue>(this Task<IRValue<TValue>> @this,
                                                                            Func<TValue, Task<bool>> predicate,
                                                                            Func<TValue, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValue : notnull =>
            await @this.MapAwait(awaitedThis => awaitedThis.RValueEnsureAsync(predicate, noneFunc));

        /// <summary>
        /// Check errors by predicate async to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> RValueEnsureAwait<TValue>(this Task<IRValue<TValue>> @this,
                                                                            Func<TValue, bool> predicate,
                                                                            Func<TValue, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValue : notnull =>
            await @this.RValueEnsureAwait(value => predicate(value).ToTask(), noneFunc);
    }
}