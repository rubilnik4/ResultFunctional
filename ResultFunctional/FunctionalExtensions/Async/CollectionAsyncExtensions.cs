using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async;

/// <summary>
/// Collection extensions for async method
/// </summary>
public static class CollectionAsyncExtensions
{
    /// <summary>
    /// Converting collection to enumerable type
    /// </summary>
    public static Task<IEnumerable<T>> GetEnumerableTaskAsync<T>(this Task<IReadOnlyCollection<T>> collection) =>
        collection.MapTaskAsync(values => (IEnumerable<T>)values);

    /// <summary>
    /// Converting enumerable to collection type
    /// </summary>
    public static Task<IReadOnlyCollection<T>> GetCollectionTaskAsync<T>(this Task<IEnumerable<T>> collection) =>
        collection.MapTaskAsync(values => (IReadOnlyCollection<T>)values.ToList());
}