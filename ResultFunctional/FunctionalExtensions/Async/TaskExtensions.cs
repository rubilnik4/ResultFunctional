using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async;

/// <summary>
/// Extensions for async method
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// Decorate value to task
    /// </summary>
    /// <typeparam name="TValue">Value type</typeparam>
    /// <param name="value">Value</param>
    /// <returns>Value task</returns>
    public static Task<TValue> ToTask<TValue>(this TValue value)
        where TValue : notnull =>
        Task.FromResult(value);
}