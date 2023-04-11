using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task result value functions converting to result collection
    /// </summary>
    public static class ResultValueWhereToCollectionBindAsyncExtensions
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
        public static async Task<IRList<TValueOut>> ResultValueContinueToCollectionBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                                  Func<TValueIn, bool> predicate,
                                                                                                                  Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                                  Func<TValueIn, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueContinueBindAsync(predicate, okFunc, badFunc).
            ToRListTaskAsync();

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
        public static async Task<IRList<TValueOut>> ResultValueContinueToCollectionBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                       Func<TValueIn, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultValueContinueToCollectionBindAsync(predicate,
                                                             okFunc,
                                                             values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute result value function converting to task result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> ResultValueOkBadToCollectionBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                    Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueOkBadBindAsync(okFunc, badFunc).
            ToRListTaskAsync();

        /// <summary>
        /// Execute result value function converting to task result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRList<TValueOut>> ResultValueOkToCollectionBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueOkBindAsync(okFunc).
            ToRListTaskAsync();
    }
}