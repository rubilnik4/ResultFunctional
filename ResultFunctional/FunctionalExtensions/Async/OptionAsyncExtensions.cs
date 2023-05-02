using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Async extension methods for condition functions
    /// </summary>
    public static class OptionAsyncExtensions
    {
        /// <summary>
        /// Execute converting async function base on predicate condition
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>
        public static async Task<TResult> OptionAsync<TSource, TResult>(this TSource @this,
                                                                              Func<TSource, bool> predicate,
                                                                              Func<TSource, Task<TResult>> someFunc,
                                                                              Func<TSource, Task<TResult>> noneFunc) =>
             predicate(@this)
                ? await someFunc.Invoke(@this)
                : await noneFunc.Invoke(@this);

        /// <summary>
        /// Execute converting async function if predicate condition <see langword="true"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <returns>Converting function result</returns>  
        public static async Task<TSource> OptionSomeAsync<TSource>(this TSource @this,
                                                               Func<TSource, bool> predicate,
                                                               Func<TSource, Task<TSource>> someFunc) =>
             await @this.OptionAsync(predicate, someFunc, _ => Task.FromResult(@this));

        /// <summary>
        /// Execute converting async function if predicate condition <see langword="false"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>
        public static async Task<TSource> OptionNoneAsync<TSource>(this TSource @this,
                                                                Func<TSource, bool> predicate,
                                                                Func<TSource, Task<TSource>> noneFunc) =>
            await @this.OptionAsync(predicate, _ => Task.FromResult(@this), noneFunc);
    }
}