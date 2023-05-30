using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

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

    /// <summary>
    /// Decorate action to task function
    /// </summary>
    /// <typeparam name="TValue">Value type</typeparam>
    /// <param name="action">Action</param>
    /// <returns>Task function</returns>
    public static Func<TValue, Task> ToTask<TValue>(this Action<TValue> action)
        where TValue : notnull =>
        value => Task.CompletedTask.Void(_ => action(value));
}