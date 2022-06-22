using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Async extension methods for condition functions
    /// </summary>
    public static class WhereAsyncExtensions
    {
        /// <summary>
        /// Execute converting async function base on predicate condition
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>
        public static async Task<TResult> WhereContinueAsync<TSource, TResult>(this TSource @this,
                                                                              Func<TSource, bool> predicate,
                                                                              Func<TSource, Task<TResult>> okFunc,
                                                                              Func<TSource, Task<TResult>> badFunc) =>
             predicate(@this)
                ? await okFunc.Invoke(@this)
                : await badFunc.Invoke(@this);

        /// <summary>
        /// Execute converting async function if predicate condition <see langword="true"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <returns>Converting function result</returns>  
        public static async Task<TSource> WhereOkAsync<TSource>(this TSource @this,
                                                               Func<TSource, bool> predicate,
                                                               Func<TSource, Task<TSource>> okFunc) =>
             await @this.WhereContinueAsync(predicate, okFunc, _ => Task.FromResult(@this));

        /// <summary>
        /// Execute converting async function if predicate condition <see langword="false"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>
        public static async Task<TSource> WhereBadAsync<TSource>(this TSource @this,
                                                                Func<TSource, bool> predicate,
                                                                Func<TSource, Task<TSource>> badFunc) =>
            await @this.WhereContinueAsync(predicate, _ => Task.FromResult(@this), badFunc);
    }
}