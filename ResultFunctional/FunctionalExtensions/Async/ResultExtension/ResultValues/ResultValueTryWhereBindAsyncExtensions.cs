using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Values.ResultValueTryExtensions;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
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
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, Task<TValueOut>> func,
                                                                                                         Func<Exception, IRError> exceptionFunc) =>
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
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, Task<TValueOut>> func,
                                                                                                         IRError error) =>
            await @this.
            ResultValueBindOkBindAsync(value => ResultValueTryAsync(() => func.Invoke(value), error));
    }
}