using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Async exception handling result value with conditions extension methods
    /// </summary>
    public static class ResultValueTryWhereAsyncExtensions
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
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                     Func<TValueIn, Task<TValueOut>> func,
                                                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultValueBindOkAsync(value => ResultValueTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                     Func<TValueIn, Task<TValueOut>> func,
                                                                                                     IErrorResult error) =>
            await @this.
            ResultValueBindOkAsync(value => ResultValueTryAsync(() => func.Invoke(value), error));
    }
}