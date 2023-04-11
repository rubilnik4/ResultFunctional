using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Exception handling result value async extension methods
    /// </summary>
    public static class ResultValueTryAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result value converting
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="func">Value function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> ResultValueTryAsync<TValue>(Func<Task<TValue>> func,
                                                                              Func<Exception, IRError> exceptionFunc)
            where TValue : notnull
        {
            try
            {
                return await func.Invoke().ToRValueTaskAsync();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRValue<TValue>();
            }
        }

        /// <summary>
        /// Execute async function and handle exception with result value converting
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> ResultValueTryAsync<TValue>(Func<Task<TValue>> func, IRError error)
            where TValue : notnull =>
            await ResultValueTryAsync(func, error.AppendException);
    }
}