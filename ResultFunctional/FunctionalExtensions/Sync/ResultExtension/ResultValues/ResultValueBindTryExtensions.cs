using System;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value monad function with exception handling
    /// </summary>
    public static class ResultValueBindTryExtensions
    {
        /// <summary>
        /// Execute result value monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Result value</returns>
        public static IResultValue<TValue> ResultValueBindTry<TValue>(Func<IResultValue<TValue>> func,
                                                                      Func<Exception, IRError> exceptionFunc)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Execute result collection monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Result value</returns>
        public static IResultValue<TValue> ResultValueBindTry<TValue>(Func<IResultValue<TValue>> func, IRError error) => 
            ResultValueBindTry(func, error.AppendException);
    }
}