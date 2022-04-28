using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.ResultFactory
{
    /// <summary>
    /// Result value factory
    /// </summary>
    public static class ResultValueFactory
    {
        /// <summary>
        /// Create result collection by value
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="value">Value</param>
        /// <returns>Result value in task</returns>
        public static Task<IResultValue<TValue>> CreateTaskResultValue<TValue>(TValue value)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(value));

        /// <summary>
        /// Create result collection by error
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="error">Error</param>
        /// <returns>Result value in task</returns>
        public static Task<IResultValue<TValue>> CreateTaskResultValueError<TValue>(IErrorResult error)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(error));

        /// <summary>
        /// Create result collection by errors
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Result value in task</returns>
        public static Task<IResultValue<TValue>> CreateTaskResultValueError<TValue>(IEnumerable<IErrorResult> errors)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(errors));
    }
}