﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for task result value functions converting to result collection
    /// </summary>
    public static class RValueToListOptionTaskExtensions
    {
        /// <summary>
        /// Execute result value function converting to task result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRList<TValueOut>> RValueToListOptionTask<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                              Func<TValueIn, bool> predicate,
                                                                                              Func<TValueIn, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                              Func<TValueIn, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                  RValueOptionTask(predicate, someFunc, noneFunc).
                  ToRListTask();

        /// <summary>
        /// Execute result value function converting to task result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> RValueToListMatchTask<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                             Func<TValueIn, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                             Func<IReadOnlyCollection<IRError>, IEnumerable<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                  RValueMatchTask(someFunc, noneFunc).
                  ToRListTask();

        /// <summary>
        /// Execute result value function converting to task result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRList<TValueOut>> RValueToListSomeTask<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                            Func<TValueIn, IReadOnlyCollection<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                  RValueSomeTask(someFunc).
                  ToRListTask();
    }
}