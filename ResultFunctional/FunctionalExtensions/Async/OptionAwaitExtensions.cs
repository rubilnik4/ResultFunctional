using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Async extension methods for task condition functions
    /// </summary>
    public static class OptionAwaitExtensions
    {
        /// <summary>
        /// Execute converting async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>      
        public static async Task<TResult> OptionAwait<TSource, TResult>(this Task<TSource> @this,
                                                                                   Func<TSource, bool> predicate,
                                                                                   Func<TSource, Task<TResult>> someFunc,
                                                                                   Func<TSource, Task<TResult>> noneFunc) =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.OptionAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute converting async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>      
        public static async Task<TResult> OptionAwait<TSource, TResult>(this Task<TSource> @this,
                                                                                   Func<TSource, Task<bool>> predicate,
                                                                                   Func<TSource, Task<TResult>> someFunc,
                                                                                   Func<TSource, Task<TResult>> noneFunc) =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.OptionAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute converting async task function if predicate condition <see langword="true"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <returns>Converting function result</returns>         
        public static async Task<TSource> OptionSomeAwait<TSource>(this Task<TSource> @this,
                                                                  Func<TSource, bool> predicate,
                                                                  Func<TSource, Task<TSource>> someFunc) =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.OptionSomeAsync(predicate, someFunc));

        /// <summary>
        /// Execute converting async task function if predicate condition <see langword="true"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <returns>Converting function result</returns>         
        public static async Task<TSource> OptionSomeAwait<TSource>(this Task<TSource> @this,
                                                                  Func<TSource, Task<bool>> predicate,
                                                                  Func<TSource, Task<TSource>> someFunc) =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.OptionSomeAsync(predicate, someFunc));

        /// <summary>
        /// Execute converting async task function if predicate condition <see langword="false"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>     
        public static async Task<TSource> OptionNoneAwait<TSource>(this Task<TSource> @this,
                                                                     Func<TSource, bool> predicate,
                                                                     Func<TSource, Task<TSource>> noneFunc) =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.OptionNoneAsync(predicate, noneFunc));

        /// <summary>
        /// Execute converting async task function if predicate condition <see langword="false"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>     
        public static async Task<TSource> OptionNoneAwait<TSource>(this Task<TSource> @this,
                                                                     Func<TSource, Task<bool>> predicate,
                                                                     Func<TSource, Task<TSource>> noneFunc) =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.OptionNoneAsync(predicate, noneFunc));
    }
}