using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;

/// <summary>
/// Task result value extension methods
/// </summary>
public static class ToResultValueTaskAsyncExtensions
{
    /// <summary>
    /// Converting task value to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultValueTaskAsync<TValue>(this Task<TValue> @this)
        where TValue : notnull =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValue());

    /// <summary>
    /// Converting task result collection to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<IReadOnlyCollection<TValue>>> ToResultValueTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this)
        where TValue : notnull =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis);

    /// <summary>
    /// Converting value to result value with null checking
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming value</param>
    /// <param name="error">Null error</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultValueNullValueCheckTaskAsync<TValue>(this Task<TValue> @this, IErrorResult error) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullValueCheck(error));

    /// <summary>
    /// Converting value to result value with null checking
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming value</param>
    /// <param name="error">Null error</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultValueNullCheckTaskAsync<TValue>(this Task<TValue?> @this, IErrorResult error)
        where TValue : class =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullCheck(error));

    /// <summary>
    /// Converting value to result value with null checking
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming value</param>
    /// <param name="error">Null error</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultValueNullCheckTaskAsync<TValue>(this Task<TValue?> @this, IErrorResult error)
        where TValue : struct =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullCheck(error));

    /// <summary>
    /// Converting task result error to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result error</param>
    /// <param name="value">Value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultValueTaskAsync<TValue>(this Task<IResultError> @this, TValue value) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValue(value));

    /// <summary>
    /// Merge task result error with result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result error</param>
    /// <param name="resultValue">Result value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultBindValueTaskAsync<TValue>(this Task<IResultError> @this,
                                                                                      IResultValue<TValue> resultValue) =>
        await @this.
        MapTaskAsync(result => result.ToResultBindValue(resultValue));
}