using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueBindTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value async monad function with conditions and exception handling
    /// </summary>
    public static class ResultValueBindTryWhereAsyncExtensions
    {
        /// <summary>
        /// Execute result value async function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                       Func<TValueIn, Task<IResultValue<TValueOut>>> func,
                                                                                                       Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultValueBindOkAsync(value => ResultValueBindTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute result value async function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                       Func<TValueIn, Task<IResultValue<TValueOut>>> func,
                                                                                                       IErrorResult error) =>
            await @this.
            ResultValueBindOkAsync(value => ResultValueBindTryAsync(() => func.Invoke(value), error));
    }
}