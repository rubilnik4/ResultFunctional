using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task result value functions converting to result collection
    /// </summary>
    public static class ResultValueWhereToCollectionTaskAsyncExtensions
    {
        /// <summary>
        /// Execute result value function converting to task result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IResultCollection<TValueOut>> ResultValueContinueToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IEnumerable<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
            await @this.
            ResultValueContinueTaskAsync(predicate, okFunc, badFunc).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Execute result value function converting to task result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IResultCollection<TValueOut>> ResultValueOkBadToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                    Func<TValueIn, IEnumerable<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            await @this.
            ResultValueOkBadTaskAsync(okFunc, badFunc).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Execute result value function converting to task result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultCollection<TValueOut>> ResultValueOkToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                             Func<TValueIn, IEnumerable<TValueOut>> okFunc) =>
            await @this.
            ResultValueOkTaskAsync(okFunc).
            ToResultCollectionTaskAsync();
    }
}