﻿using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Task result value extension methods with condition
    /// </summary>
    public static class ToResultValueWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereTaskAsync<TValue>(this Task<TValue> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue, IRError> badFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToRValueWhere(predicate, badFunc));

        /// <summary>
        /// Converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereNullTaskAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, IRError> badFunc)
            where TValue : class =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToRValueWhereNull(predicate, badFunc));

        /// <summary>
        /// Converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereNullTaskAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, IRError> badFunc)
            where TValue : struct =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToRValueWhereNull(predicate, badFunc));

        /// <summary>
        /// Converting task value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ToResultValueWhereNullOkBadTaskAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IRError> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToRValueWhereNullOkBad(predicate, okFunc, badFunc));

        /// <summary>
        /// Converting task value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ToResultValueWhereNullOkBadTaskAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IRError> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToRValueWhereNullOkBad(predicate, okFunc, badFunc));
    }
}