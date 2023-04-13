using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for task result value monad function with conditions
    /// </summary>
    public static class ResultValueBindWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Execute task monad result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>        
        public static async Task<IRValue<TValueOut>> ResultValueBindContinueTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                           Func<TValueIn, bool> predicate,
                                                                                                           Func<TValueIn, IRValue<TValueOut>> someFunc,
                                                                                                           Func<TValueIn, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueBindOption(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task monad result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>          
        public static async Task<IRValue<TValueOut>> ResultValueBindWhereTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, bool> predicate,
                                                                                                             Func<TValueIn, IRValue<TValueOut>> someFunc,
                                                                                                             Func<TValueIn, IRValue<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueBindWhere(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task monad result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IRValue<TValueOut>> ResultValueBindOkBadTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, IRValue<TValueOut>> someFunc,
                                                                                                             Func<IReadOnlyCollection<IRError>, IRValue<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueBindMatch(someFunc, noneFunc));

        /// <summary>
        /// Execute task monad result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> ResultValueBindOkTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                          Func<TValueIn, IRValue<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueBindSome(someFunc));

        /// <summary>
        /// Execute task monad result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="noneFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindBadTaskAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                           Func<IReadOnlyCollection<IRError>, IRValue<TValue>> noneFunc)
            where TValue : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueBindNone(noneFunc));

        /// <summary>
        /// Adding errors to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindErrorsOkTaskAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                            Func<TValue, IROption> someFunc)
            where TValue : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueBindEnsure(someFunc));
    }
}