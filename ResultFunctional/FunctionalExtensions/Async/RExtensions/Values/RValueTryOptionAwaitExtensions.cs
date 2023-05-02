using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Values.RValueTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Async exception handling task result value with conditions extension methods
    /// </summary>
    public static class RValueTryOptionAwaitExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with task result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueTrySomeAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                             Func<TValueIn, Task<TValueOut>> func,
                                                                                             Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                RValueBindSomeAwait(value => RValueTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with task result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueTrySomeAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                             Func<TValueIn, Task<TValueOut>> func,
                                                                                             IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                RValueBindSomeAwait(value => RValueTryAsync(() => func.Invoke(value), error));
    }
}