using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
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
        public static IRList<TValue> ResultCollectionTry<TValue>(Func<IEnumerable<TValue>> func,
                                                                 Func<Exception, IRError> exceptionFunc)
            where TValue : notnull
        {
            try
            {
                return func.Invoke().ToRList();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRList<TValue>();
            }
        }

        /// <summary>
        /// Execute function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static IRList<TValue> ResultCollectionTry<TValue>(Func<IEnumerable<TValue>> func, IRError error) 
            where TValue : notnull =>
            ResultCollectionTry(func, error.AppendException);
    }
}