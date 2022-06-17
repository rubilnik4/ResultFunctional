using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Exception handling result value extension methods
    /// </summary>
    public static class ResultValueTryExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result value converting
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="func">Value function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result value</returns>
        public static IResultValue<TValue> ResultValueTry<TValue>(Func<TValue> func, Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return new ResultValue<TValue>(func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Execute function and handle exception with result value converting
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Result value</returns>
        public static IResultValue<TValue> ResultValueTry<TValue>(Func<TValue> func, IErrorResult error) =>
            ResultValueTry(func, error.AppendException);
    }
}