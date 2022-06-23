using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;

/// <summary>
/// Result error async extension methods
/// </summary> 
public static class ToResultErrorAsyncExtensions
{
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