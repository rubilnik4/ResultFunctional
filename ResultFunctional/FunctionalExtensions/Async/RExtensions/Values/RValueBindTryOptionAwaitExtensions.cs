﻿using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Values.RValueBindTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for task result value async monad function with conditions and exception handling
    /// </summary>
    public static class RValueBindTryOptionAwaitExtensions
    {
        /// <summary>
        /// Execute task result value async function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueBindTrySomeAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                 Func<TValueIn, Task<IRValue<TValueOut>>> func,
                                                                                                 Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                RValueBindSomeAwait(value => RValueBindTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute task result value async function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueBindTrySomeAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                 Func<TValueIn, Task<IRValue<TValueOut>>> func,
                                                                                                 IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                RValueBindSomeAwait(value => RValueBindTryAsync(() => func.Invoke(value), error));
    }
}