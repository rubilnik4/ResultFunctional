using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task result value monad function with conditions and exception handling
    /// </summary>
    public static class ResultValueBindTryWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Execute task result value function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                       Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                                       Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultValueBindOkTaskAsync(value => ResultValueBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute task result value function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                       Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                                       IErrorResult error) =>
            await @this.
            ResultValueBindOkTaskAsync(value => ResultValueBindTry(() => func.Invoke(value), error));
    }
}