﻿using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Result value async extension methods
    /// </summary>
    public static class ToResultValueAsyncExtensions
    {
        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueNullValueCheckAsync<TValue>(this TValue @this,
                                                                                                Task<IRError> error) =>
            @this.ToRValueNullValueCheck(await error);

        /// <summary>
        /// Converting value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckAsync<TValue>(this TValue? @this,
                                                                                           Task<IRError> error)
            where TValue : class =>
            @this.ToRValueNullCheck(await error);

        /// <summary>
        /// Converting value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckAsync<TValue>(this TValue? @this,
                                                                                           Task<IRError> error)
            where TValue : struct =>
            @this.ToRValueNullCheck(await error);

        /// <summary>
        /// Async converting result error to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueAsync<TValue>(this IResultError @this, Task<TValue> value) =>
            @this.OkStatus
                ? new ResultValue<TValue>(await value)
                : new ResultValue<TValue>(@this.Errors);

        /// <summary>
        /// Async merge result error with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultBindValueAsync<TValue>(this IResultError @this, Task<IResultValue<TValue>> resultValue) =>
            @this.OkStatus
                ? await resultValue
                : new ResultValue<TValue>(@this.Errors);
    }
}