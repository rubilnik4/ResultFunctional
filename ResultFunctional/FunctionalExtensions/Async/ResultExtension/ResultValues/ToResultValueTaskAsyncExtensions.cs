using System.Threading.Tasks;
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
    public static async Task<IResultValue<TValue>> ToResultValue<TValue>(this Task<TValue> @this)
        where TValue : notnull =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValue());

    /// <summary>
    /// Converting value to result value with null checking
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming value</param>
    /// <param name="error">Null error</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultValueNullValueCheck<TValue>(this Task<TValue> @this, IErrorResult error) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullValueCheck(error));

    /// <summary>
    /// Converting value to result value with null checking
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming value</param>
    /// <param name="error">Null error</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultValueNullCheck<TValue>(this Task<TValue?> @this, IErrorResult error)
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
    public static async Task<IResultValue<TValue>> ToResultValueNullCheck<TValue>(this Task<TValue?> @this, IErrorResult error)
        where TValue : struct =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullCheck(error));
}