using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;

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
    public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IEnumerable<IResultValue<TValue>>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

    /// <summary>
    /// Converting task collection of result value to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection of result value</param>
    /// <returns>Outgoing result collection</returns>   
    public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IList<IResultValue<TValue>>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

    /// <summary>
    /// Converting task collection of result value to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection of result value</param>
    /// <returns>Outgoing result collection</returns>   
    public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IReadOnlyCollection<IResultValue<TValue>>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

    /// <summary>
    /// Converting task result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<IEnumerable<TValue>>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

    /// <summary>
    /// Converting task result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>   
    public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<IReadOnlyCollection<TValue>>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

    /// <summary>
    /// Converting task result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>
    public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<ReadOnlyCollection<TValue>>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

    /// <summary>
    /// Converting task result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>      
    public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this Task<IResultValue<List<TValue>>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultCollection());

    /// <summary>
    /// Converting task result collection to result value
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Result collection</param>
    /// <returns>Result value</returns>
    public static async Task<IResultValue<IReadOnlyCollection<TValue>>> ToResultValueFromCollectionTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ToResultValue());

    /// <summary>
    /// Converting task result collection to result error
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Result collection</param>
    /// <returns>Result error</returns>
    public static async Task<IResultError> ToResultErrorFromCollectionTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis);


    /// <summary>
    /// Преобразовать в результирующий ответ коллекции
    /// </summary>  
    public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this IEnumerable<Task<IResultValue<TValue>>> @this)
        where TValue : notnull =>
        await Task.WhenAll(@this).
        MapTaskAsync(result => result.ToResultCollection());
}