using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists
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
        public static IRList<TValue> ResultCollectionBindTry<TValue>(Func<IRList<TValue>> func,
                                                                     Func<Exception, IRError> exceptionFunc)
            where TValue : notnull
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRList<TValue>();
            }
        }

        /// <summary>
        /// Execute result collection monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static IRList<TValue> ResultCollectionBindTry<TValue>(Func<IRList<TValue>> func, IRError error)
            where TValue : notnull =>
            ResultCollectionBindTry(func, error.AppendException);
    }
}