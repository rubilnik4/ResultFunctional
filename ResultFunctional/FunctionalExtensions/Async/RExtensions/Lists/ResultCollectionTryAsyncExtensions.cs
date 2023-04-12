using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Async exception handling result collection extension methods
    /// </summary>
    public static class ResultCollectionTryAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IReadOnlyCollection<TValue>>> func,
                                                                                  Func<Exception, IRError> exceptionFunc)
            where TValue : notnull
        {
            try
            {
                return await func.Invoke().ToRListTaskAsync();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRList<TValue>();
            }
        }

        /// <summary>
        /// Execute async function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IReadOnlyCollection<TValue>>> func,
                                                                                  IRError error)
            where TValue : notnull =>
            await ResultCollectionTryAsync(func, error.AppendException);
    }
}