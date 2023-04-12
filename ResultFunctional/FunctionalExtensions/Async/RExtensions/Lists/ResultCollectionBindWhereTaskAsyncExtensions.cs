using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Options;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection monad task function with conditions
    /// </summary>
    public static class ResultCollectionBindWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Execute monad result collection task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueTaskAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> okFunc,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindContinue(predicate, okFunc, badFunc));

       /// <summary>
        /// Execute monad result collection task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRList<TValueOut>> ResultCollectionBindWhereTaskAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> badFunc)
           where TValueIn : notnull
           where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindWhere(predicate, okFunc, badFunc));
       
        /// <summary>
        /// Execute monad result collection task function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkBadTaskAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                        Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> okFunc,
                                                                                        Func<IReadOnlyCollection<IRError>, IRList<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindOkBad(okFunc, badFunc));

        /// <summary>
        /// Execute monad result collection task function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkTaskAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this, 
                                                                                                         Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindOk(okFunc));

        /// <summary>
        /// Execute monad result collection task function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRList<TValue>> ResultCollectionBindBadTaskAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IRError>, IRList<TValue>> badFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindBad(badFunc));

        /// <summary>
        /// Adding errors to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindErrorsOkTaskAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                               Func<IReadOnlyCollection<TValue>, IROption> okFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindErrorsOk(okFunc));
    }
}