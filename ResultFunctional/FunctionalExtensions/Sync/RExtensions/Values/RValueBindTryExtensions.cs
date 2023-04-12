using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value monad function with exception handling
    /// </summary>
    public static class RValueBindTryExtensions
    {
        /// <summary>
        /// Execute result value monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Result value</returns>
        public static IRValue<TValue> RValueBindTry<TValue>(Func<IRValue<TValue>> func, Func<Exception, IRError> exceptionFunc)
            where TValue : notnull
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRValue<TValue>();
            }
        }

        /// <summary>
        /// Execute result collection monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Result value</returns>
        public static IRValue<TValue> RValueBindTry<TValue>(Func<IRValue<TValue>> func, IRError error)
            where TValue : notnull => 
            RValueBindTry(func, error.AppendException);
    }
}