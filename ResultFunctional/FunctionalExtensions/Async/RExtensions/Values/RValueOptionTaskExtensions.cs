using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for task result value functor function with conditions
    /// </summary>
    public static class RValueOptionTaskExtensions
    {
        /// <summary>
        /// Execute task result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> RValueOptionTask<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                           Func<TValueIn, bool> predicate,
                                                                                           Func<TValueIn, TValueOut> someFunc,
                                                                                           Func<TValueIn, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RValueOption(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result value function base on predicate condition returning value in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IRValue<TValueOut>> RValueWhereTask<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                          Func<TValueIn, bool> predicate,
                                                                                          Func<TValueIn, TValueOut> someFunc,
                                                                                          Func<TValueIn, TValueOut> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RValueWhere(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> RValueMatchTask<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                          Func<TValueIn, TValueOut> someFunc,
                                                                                          Func<IReadOnlyCollection<IRError>, TValueOut> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RValueMatch(someFunc, noneFunc));

        /// <summary>
        /// Execute task result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IRValue<TValueOut>> RValueSomeTask<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                         Func<TValueIn, TValueOut> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RValueSome(someFunc));

        /// <summary>
        /// Execute task result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> RValueNoneTask<TValue>(this Task<IRValue<TValue>> @this,
                                                                         Func<IReadOnlyCollection<IRError>, TValue> noneFunc)
            where TValue : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RValueNone(noneFunc));

        /// <summary>
        /// Check errors by predicate to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> RValueEnsureTask<TValue>(this Task<IRValue<TValue>> @this,
                                                                           Func<TValue, bool> predicate,
                                                                           Func<TValue, IEnumerable<IRError>> noneFunc)
            where TValue : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RValueEnsure(predicate, noneFunc));
    }
}