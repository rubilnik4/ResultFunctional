using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Values.RValueTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Async exception handling result value with conditions extension methods
    /// </summary>
    public static class RValueTryOptionAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueTrySomeAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<TValueOut>> func,
                                                                                             Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            RValueBindSomeAsync(value => RValueTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueTrySomeAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                     Func<TValueIn, Task<TValueOut>> func,
                                                                                                     IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            RValueBindSomeAsync(value => RValueTryAsync(() => func.Invoke(value), error));
    }
}