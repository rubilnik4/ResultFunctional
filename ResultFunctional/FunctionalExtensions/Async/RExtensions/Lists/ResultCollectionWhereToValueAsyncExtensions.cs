﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection async functions converting to result value
    /// </summary>
    public static class ResultCollectionWhereToValueAsyncExtensions
    {
        /// <summary>
        /// Execute result collection async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IRValue<TValueOut>> ResultCollectionContinueToValueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValue().
                ResultValueContinueAsync(predicate, okFunc, badFunc);

        /// <summary>
        /// Execute result collection async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IRValue<TValueOut>> ResultCollectionContinueToValueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionContinueToValueAsync(predicate, okFunc,
                                                             values => badFunc(values).GetCollectionTaskAsync());


        /// <summary>
        /// Execute result collection async function converting to result value depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IRValue<TValueOut>> ResultCollectionOkBadToValueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, Task<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValue().
            ResultValueOkBadAsync(okFunc, badFunc);

        /// <summary>
        /// Execute result collection async function converting to result value if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRValue<TValueOut>> ResultCollectionOkToValueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValue().
            ResultValueOkAsync(okFunc);
    }
}