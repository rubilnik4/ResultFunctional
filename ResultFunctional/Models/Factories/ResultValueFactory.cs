using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Factories
{
    /// <summary>
    /// Result value factory
    /// </summary>
    public static class ResultValueFactory
    {
        /// <summary>
        /// Create result value by value
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="value">Value</param>
        /// <returns>Result value in task</returns>
        public static IResultValue<TValue> CreateResultValue<TValue>(TValue value)
            where TValue : notnull =>
            new ResultValue<TValue>(value);

        /// <summary>
        /// Create task result value by value
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="value">Value</param>
        /// <returns>Result value in task</returns>
        public static Task<IResultValue<TValue>> CreateTaskResultValue<TValue>(TValue value)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(value));

        /// <summary>
        /// Create result value by error
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="error">Error</param>
        /// <returns>Result value in task</returns>
        public static IResultValue<TValue> CreateResultValueError<TValue>(IRError error)
            where TValue : notnull =>
            new ResultValue<TValue>(error);

        /// <summary>
        /// Create task result value by error
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="error">Error</param>
        /// <returns>Result value in task</returns>
        public static Task<IResultValue<TValue>> CreateTaskResultValueError<TValue>(IRError error)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(error));

        /// <summary>
        /// Create result value by errors
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Result value in task</returns>
        public static IResultValue<TValue> CreateResultValueError<TValue>(IEnumerable<IRError> errors)
            where TValue : notnull =>
            new ResultValue<TValue>(errors);

        /// <summary>
        /// Create task result value by errors
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Result value in task</returns>
        public static Task<IResultValue<TValue>> CreateTaskResultValueError<TValue>(IEnumerable<IRError> errors)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(errors));
    }
}