using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value async monad function with exception handling
    /// </summary>
    public static class ResultValueBindTryAsyncExtensions
    {
        /// <summary>
        /// Execute result value async monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueBindTryAsync<TValue>(Func<Task<IResultValue<TValue>>> func,
                                                                                       Func<Exception, IRError> exceptionFunc)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Execute result collection async monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueBindTryAsync<TValue>(Func<Task<IResultValue<TValue>>> func,
                                                                                       IRError error) =>
             await ResultValueBindTryAsync(func, error.AppendException);
    }
}