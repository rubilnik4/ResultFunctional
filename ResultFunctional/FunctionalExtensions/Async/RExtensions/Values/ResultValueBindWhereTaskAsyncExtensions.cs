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
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>        
        public static async Task<IRValue<TValueOut>> ResultValueBindContinueTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                           Func<TValueIn, bool> predicate,
                                                                                                           Func<TValueIn, IRValue<TValueOut>> okFunc,
                                                                                                           Func<TValueIn, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindContinue(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task monad result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>          
        public static async Task<IRValue<TValueOut>> ResultValueBindWhereTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, bool> predicate,
                                                                                                             Func<TValueIn, IRValue<TValueOut>> okFunc,
                                                                                                             Func<TValueIn, IRValue<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindWhere(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task monad result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IRValue<TValueOut>> ResultValueBindOkBadTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, IRValue<TValueOut>> okFunc,
                                                                                                             Func<IReadOnlyCollection<IRError>, IRValue<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindOkBad(okFunc, badFunc));

        /// <summary>
        /// Execute task monad result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> ResultValueBindOkTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                          Func<TValueIn, IRValue<TValueOut>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindOk(okFunc));

        /// <summary>
        /// Execute task monad result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindBadTaskAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                           Func<IReadOnlyCollection<IRError>, IRValue<TValue>> badFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindBad(badFunc));

        /// <summary>
        /// Adding errors to task result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindErrorsOkTaskAsync<TValue>(this Task<IRValue<TValue>> @this,
                                                                                            Func<TValue, IROption> okFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindErrorsOk(okFunc));
    }
}