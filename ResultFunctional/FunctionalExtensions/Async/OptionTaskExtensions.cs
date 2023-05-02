using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Extension methods for task condition functions
    /// </summary>
    public static class OptionTaskExtensions
    {
        /// <summary>
        /// Execute converting task function base on predicate condition
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>   
        public static async Task<TResult> OptionTask<TSource, TResult>(this Task<TSource> @this,
                                                                                   Func<TSource, bool> predicate,
                                                                                   Func<TSource, TResult> someFunc,
                                                                                   Func<TSource, TResult> noneFunc) =>
            await @this.
            MapTask(thisAwaited => thisAwaited.Option(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute converting task function if predicate condition <see langword="true"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <returns>Converting function result</returns>     
        public static async Task<TSource> OptionSomeTask<TSource>(this Task<TSource> @this,
                                                                  Func<TSource, bool> predicate,
                                                                  Func<TSource, TSource> someFunc) =>
            await @this.
            MapTask(thisAwaited => thisAwaited.OptionSome(predicate, someFunc));

        /// <summary>
        /// Execute converting task function if predicate condition <see langword="false"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns> 
        public static async Task<TSource> OptionNoneTask<TSource>(this Task<TSource> @this,
                                                                     Func<TSource, bool> predicate,
                                                                     Func<TSource, TSource> noneFunc) =>
            await @this.
            MapTask(thisAwaited => thisAwaited.OptionNone(predicate, noneFunc));
    }
}