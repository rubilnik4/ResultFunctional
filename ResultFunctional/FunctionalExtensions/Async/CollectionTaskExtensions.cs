using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async;

/// <summary>
/// Collection extensions for async method
/// </summary>
public static class CollectionTaskExtensions
{
    /// <summary>
    /// Converting collection to enumerable type
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection</param>
    /// <returns>Enumerable</returns>
    public static Task<IEnumerable<T>> ToEnumerableTask<T>(this IEnumerable<T> collection) =>
        Task.FromResult(collection);

    /// <summary>
    /// Converting collection to enumerable type
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection</param>
    /// <returns>Enumerable</returns>
    public static Task<IEnumerable<T>> ToEnumerableTask<T>(this Task<IReadOnlyCollection<T>> collection) =>
        collection.MapTask(values => (IEnumerable<T>)values);

    /// <summary>
    /// Converting enumerable to collection type
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection</param>
    /// <returns>Collection</returns>
    public static Task<IReadOnlyCollection<T>> ToCollectionTask<T>(this Task<IEnumerable<T>> collection) =>
        collection.MapTask(values => (IReadOnlyCollection<T>)values.ToList());

    /// <summary>
    /// Converting collection to collection task type
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection</param>
    /// <returns>Collection</returns>
    public static Task<IReadOnlyCollection<T>> ToCollectionTask<T>(this IReadOnlyCollection<T> collection) =>
        Task.FromResult(collection);

    /// <summary>
    /// Converting to enumerable type
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    /// <param name="value">Value</param>
    /// <returns>Enumerable</returns>
    public static Task<IReadOnlyCollection<T>> ToCollectionTask<T>(this Task<T> value)
        where T : IEnumerable<T> =>
        value.MapTask(values => (IReadOnlyCollection<T>)values.ToList());

    /// <summary>
    /// Converting to enumerable type
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    /// <param name="value">Value</param>
    /// <returns>Enumerable</returns>
    public static Task<IEnumerable<T>> ToEnumerableTask<T>(this Task<T> value)
        where T : IEnumerable<T> =>
        value.MapTask(values => (IEnumerable<T>)values);
}