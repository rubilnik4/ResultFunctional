using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;

/// <summary>
/// Task result collections extension methods
/// </summary>
public static class ToResultCollectionTaskAsyncExtensions
{
    /// <summary>
    /// Converting task collection of result value to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection of result value</param>
    /// <returns>Outgoing result collection</returns>   
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IEnumerable<IRValue<TValue>>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList());

    /// <summary>
    /// Converting task collection of result value to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection of result value</param>
    /// <returns>Outgoing result collection</returns>   
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IList<IRValue<TValue>>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList());

    /// <summary>
    /// Converting task collection of result value to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection of result value</param>
    /// <returns>Outgoing result collection</returns>   
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IReadOnlyCollection<IRValue<TValue>>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList());

    /// <summary>
    /// Converting task result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IRValue<IEnumerable<TValue>>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList());

    /// <summary>
    /// Converting task result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>   
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IRValue<IReadOnlyCollection<TValue>>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList());

    /// <summary>
    /// Converting task result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IRValue<ReadOnlyCollection<TValue>>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList());

    /// <summary>
    /// Converting task result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>      
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IRValue<List<TValue>>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList());

    /// <summary>
    /// Converting task result collection to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Result collection</param>
    /// <returns>Result value</returns>
    public static async Task<IRValue<IReadOnlyCollection<TValue>>> ToRValueFromCollectionTaskAsync<TValue>(this Task<IRList<TValue>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRValue());

    /// <summary>
    /// Converting task result collection to result error
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Result collection</param>
    /// <returns>Result error</returns>
    public static async Task<IRUnit> ToRUnitFromCollectionTaskAsync<TValue>(this Task<IRList<TValue>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRUnit());

    /// <summary>
    ///Converting task result collection to result error
    /// </summary>  
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this IEnumerable<Task<IRValue<TValue>>> @this)
        where TValue : notnull =>
        await Task.WhenAll(@this).
        MapTask(result => result.ToRList());

    /// <summary>
    /// Converting task errors to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming errors</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IReadOnlyCollection<IRError>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList<TValue>());

    /// <summary>
    /// Converting task error to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming errors</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IRError> @this)
        where TValue : notnull =>
        await @this.
            MapTask(awaitedThis => awaitedThis.ToRList<TValue>());

    /// <summary>
    /// Converting task result unit to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming errors</param>
    /// <param name="values">Collection</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IRUnit> @this, IReadOnlyCollection<TValue> values)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList(values));

    /// <summary>
    /// Converting task result to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming errors</param>
    /// <param name="values">Collection</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IROption> @this, IReadOnlyCollection<TValue> values)
        where TValue : notnull =>
        await @this.
            MapTask(awaitedThis => awaitedThis.ToRList(values));

    /// <summary>
    /// Converting task collection to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IRList<TValue>> ToRListTaskAsync<TValue>(this Task<IReadOnlyCollection<TValue>> @this)
        where TValue : notnull =>
        await @this.
        MapTask(awaitedThis => awaitedThis.ToRList());
}