using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;

/// <summary>
/// Result error async task extension methods
/// </summary> 
public static class ToResultErrorTaskBindExtensions
{
    /// <summary>
    /// Converting task result error to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result error</param>
    /// <param name="value">Value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultValueTaskAsync<TValue>(this Task<IResultError> @this, Task<TValue> value) =>
        await @this.
        MapBindAsync(awaitedThis => awaitedThis.ToResultValueAsync(value));

    /// <summary>
    /// Async merge task result error with result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result error</param>
    /// <param name="resultValue">Result value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IResultValue<TValue>> ToResultBindValueBindAsync<TValue>(this Task<IResultError> @this,
                                                                                      Task<IResultValue<TValue>> resultValue) =>
        await @this.
        MapBindAsync(result => result.ToResultBindValueAsync(resultValue));

}