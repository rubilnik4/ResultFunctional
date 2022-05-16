using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Result error extension methods
    /// </summary>
    public static class ResultErrorExtensions
    {
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

        /// <summary>
        /// Merge result error with result collection
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultCollection">Result Collection</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValue> ToResultCollection<TValue>(this IResultError @this, IReadOnlyCollection<TValue> resultCollection) =>
            @this.OkStatus
                ? new ResultCollection<TValue>(resultCollection)
                : new ResultCollection<TValue>(@this.Errors);
    }
}