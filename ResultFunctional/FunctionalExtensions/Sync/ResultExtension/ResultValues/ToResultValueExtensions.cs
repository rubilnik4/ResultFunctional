using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Result value extension methods
    /// </summary>
    public static class ToResultValueExtensions
    {
        /// <summary>
        /// Converting value to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ToResultValue<TValue>(this TValue @this)
            where TValue : notnull =>
            new ResultValue<TValue>(@this);

        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ToResultValueNullValueCheck<TValue>(this TValue @this, IErrorResult error) =>
            @this != null
                ? new ResultValue<TValue>(@this)
                : new ResultValue<TValue>(error);

        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ToResultValueNullCheck<TValue>(this TValue? @this, IErrorResult error)
            where TValue : class =>
            @this != null
                ? new ResultValue<TValue>(@this)
                : new ResultValue<TValue>(error);

        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ToResultValueNullCheck<TValue>(this TValue? @this, IErrorResult error)
            where TValue : struct =>
            @this != null
                ? new ResultValue<TValue>((TValue)@this)
                : new ResultValue<TValue>(error);

        /// <summary>
        /// Converting result error to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ToResultValue<TValue>(this IResultError @this, TValue value) =>
            @this.OkStatus
                ? new ResultValue<TValue>(value)
                : new ResultValue<TValue>(@this.Errors);

        /// <summary>
        /// Merge result error with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ToResultBindValue<TValue>(this IResultError @this, IResultValue<TValue> resultValue) =>
            @this.OkStatus
                ? resultValue
                : new ResultValue<TValue>(@this.Errors);
    }
}