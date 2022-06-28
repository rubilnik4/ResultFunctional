using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Exception handling result collection extension methods
    /// </summary>
    public static class ResultCollectionTryExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result collection</returns>
        public static IResultCollection<TValue> ResultCollectionTry<TValue>(Func<IEnumerable<TValue>> func,
                                                                            Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return new ResultCollection<TValue>(func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Execute function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static IResultCollection<TValue> ResultCollectionTry<TValue>(Func<IEnumerable<TValue>> func, IErrorResult error) =>
            ResultCollectionTry(func, error.AppendException);
    }
}