using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;
using static ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues.ResultValueTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Async exception handling task result value with conditions extension methods
    /// </summary>
    public static class ResultValueTryWhereBindAsyncExtensions
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
        public static async Task<IRValue<TValueOut>> ResultValueTryOkBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                    Func<TValueIn, Task<TValueOut>> func,
                                                                                                    Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueBindOkBindAsync(value => ResultValueTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with task result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ResultValueTryOkBindAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, Task<TValueOut>> func,
                                                                                                         IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueBindOkBindAsync(value => ResultValueTryAsync(() => func.Invoke(value), error));
    }
}