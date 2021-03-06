using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value monad function with conditions and exception handling
    /// </summary>
    public static class ResultValueBindTryWhereExtensions
    {
        /// <summary>
        /// Execute result value function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueBindTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                        Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                        Func<Exception, IErrorResult> exceptionFunc) =>
            @this.ResultValueBindOk(value => ResultValueBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute result value function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueBindTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                        Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                        IErrorResult error) =>
            @this.ResultValueBindOk(value => ResultValueBindTry(() => func.Invoke(value), error));
    }
}