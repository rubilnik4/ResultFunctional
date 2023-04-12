using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Exception handling result value extension methods
    /// </summary>
    public static class RValueTryExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result value converting
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="func">Value function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result value</returns>
        public static IRValue<TValue> RValueTry<TValue>(Func<TValue> func, Func<Exception, IRError> exceptionFunc)
            where TValue : notnull
        {
            try
            {
                return func.Invoke().ToRValue();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRValue<TValue>();
            }
        }

        /// <summary>
        /// Execute function and handle exception with result value converting
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Result value</returns>
        public static IRValue<TValue> RValueTry<TValue>(Func<TValue> func, IRError error)
            where TValue : notnull =>
            RValueTry(func, error.AppendException);
    }
}