using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
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
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> ResultValueContinueBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                       Func<TValueIn, bool> predicate,
                                                                                                       Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                                       Func<TValueIn, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueContinueAsync(predicate, someFunc, noneFunc));

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
        public static async Task<IRValue<TValueOut>> ResultValueContinueBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                                            Func<TValueIn, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultValueContinueBindAsync(predicate, someFunc,
                                                     values => noneFunc(values).GetCollectionTaskAsync());

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
        public static async Task<IRValue<TValueOut>> ResultValueWhereBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, bool> predicate,
                                                                                                         Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                                         Func<TValueIn, Task<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueWhereAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> ResultValueOkBadBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, Task<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueOkBadAsync(someFunc, noneFunc));

        /// <summary>
        /// Execute task result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IRValue<TValueOut>> ResultValueOkBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                      Func<TValueIn, Task<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueOkAsync(someFunc));

        /// <summary>
        /// Execute task result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBadBindAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IRError>, Task<TValue>> noneFunc)
           where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueBadAsync(noneFunc));

        /// <summary>
        /// Check errors by predicate async to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> ResultValueCheckErrorsOkBindAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                           Func<TValue, bool> predicate,
                                                                           Func<TValue, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValue : notnull =>
              await @this.
             MapBindAsync(awaitedThis => awaitedThis.ResultValueCheckErrorsOkAsync(predicate, noneFunc));
    }
}