using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection monad function with exception handling
    /// </summary>
    public static class ResultCollectionBindTryExtensions
    {
        /// <summary>
        /// Execute result collection monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result collection function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Result collection</returns>
        public static IResultCollection<TValue> ResultCollectionBindTry<TValue>(Func<IResultCollection<TValue>> func,
                                                                                Func<Exception, IRError> exceptionFunc)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Execute result collection monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static IResultCollection<TValue> ResultCollectionBindTry<TValue>(Func<IResultCollection<TValue>> func, IRError error) =>
            ResultCollectionBindTry(func, error.AppendException);
    }
}