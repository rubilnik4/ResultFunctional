using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindContinueTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<IRError>> badFunc) =>
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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindWhereTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> badFunc) =>
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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindOkBadTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                        Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc,
                                                                                        Func<IReadOnlyCollection<IRError>, IResultCollection<TValueOut>> badFunc) =>
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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this, Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindOk(okFunc));

        /// <summary>
        /// Execute monad result collection task function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IResultCollection<TValue>> ResultCollectionBindBadTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IRError>, IResultCollection<TValue>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindBad(badFunc));

        /// <summary>
        /// Adding errors to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindErrorsOkTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<TValue>, IResultError> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionBindErrorsOk(okFunc));
    }
}