using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Extension methods for task condition functions
    /// </summary>
    public static class WhereTaskAsyncExtensions
    {
        /// <summary>
        /// Execute converting task function base on predicate condition
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>   
        public static async Task<TResult> WhereContinueTaskAsync<TSource, TResult>(this Task<TSource> @this,
                                                                                   Func<TSource, bool> predicate,
                                                                                   Func<TSource, TResult> okFunc,
                                                                                   Func<TSource, TResult> badFunc) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.WhereContinue(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute converting task function if predicate condition <see langword="true"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <returns>Converting function result</returns>     
        public static async Task<TSource> WhereOkTaskAsync<TSource>(this Task<TSource> @this,
                                                                  Func<TSource, bool> predicate,
                                                                  Func<TSource, TSource> okFunc) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.WhereOk(predicate, okFunc));

        /// <summary>
        /// Execute converting task function if predicate condition <see langword="false"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns> 
        public static async Task<TSource> WhereBadTaskAsync<TSource>(this Task<TSource> @this,
                                                                     Func<TSource, bool> predicate,
                                                                     Func<TSource, TSource> badFunc) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.WhereBad(predicate, badFunc));
    }
}