using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection monad task function with exception handling
    /// </summary>
    public static class ResultCollectionBindTryTaskAsyncExtensions
    {
        /// <summary>
        /// Execute result collection monad task function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result collection function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindTryTaskAsync<TValue>(Func<Task<IResultCollection<TValue>>> func,
                                                                                                     Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Execute result collection monad task function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindTryTaskAsync<TValue>(Func<Task<IResultCollection<TValue>>> func,
                                                                                                     IErrorResult error) =>
            await ResultCollectionBindTryTaskAsync(func, error.AppendException);
    }
}