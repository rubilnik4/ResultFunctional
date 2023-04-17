using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;

/// <summary>
/// Task result value extension methods
/// </summary>
public static class ToRValueTaskExtensions
{
    /// <summary>
    /// Converting task value to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IRValue<TValue>> ToRValueTask<TValue>(this Task<TValue> @this)
        where TValue : notnull =>
        await @this.
            MapTask(awaitedThis => awaitedThis.ToRValue());

    /// <summary>
    /// Converting value to result value with null checking
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming value</param>
    /// <param name="error">Null error</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IRValue<TValue>> ToRValueEnsureTask<TValue>(this Task<TValue> @this, IRError error)
        where TValue : notnull =>
        await @this.
            MapTask(awaitedThis => awaitedThis.ToRValueEnsure(error));

    /// <summary>
    /// Converting task result unit to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result error</param>
    /// <param name="value">Value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IRValue<TValue>> ToRValueTask<TValue>(this Task<IRUnit> @this, TValue value)
        where TValue : notnull =>
        await @this.
            MapTask(awaitedThis => awaitedThis.ToRValue(value));

    /// <summary>
    /// Converting task result to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result error</param>
    /// <param name="value">Value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IRValue<TValue>> ToRValueTask<TValue>(this Task<IROption> @this, TValue value)
        where TValue : notnull =>
        await @this.
            MapTask(awaitedThis => awaitedThis.ToRValue(value));

    /// <summary>
    /// Merge task result error with result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result error</param>
    /// <param name="resultValue">Result value</param>
    /// <returns>Outgoing result value</returns>
    public static async Task<IRValue<TValue>> ToRValueBindTask<TValue>(this Task<IRUnit> @this, IRValue<TValue> resultValue)
        where TValue : notnull =>
        await @this.
            MapTask(result => result.ToRValueBind(resultValue));

    /// <summary>
    /// Converting task errors to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming errors</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRValue<TValue>> ToRValueTask<TValue>(this Task<IReadOnlyCollection<IRError>> @this)
        where TValue : notnull =>
        await @this.
            MapTask(awaitedThis => awaitedThis.ToRValue<TValue>());

    /// <summary>
    /// Converting task errors to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming errors</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRValue<TValue>> ToRValueTask<TValue>(this Task<IRError> @this)
        where TValue : notnull =>
        await @this.
            MapTask(awaitedThis => awaitedThis.ToRValue<TValue>());
}