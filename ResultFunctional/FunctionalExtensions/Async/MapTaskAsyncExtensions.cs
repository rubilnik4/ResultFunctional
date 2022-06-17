using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Functor task functions
    /// </summary>
    public static class MapTaskAsyncExtensions
    {
        /// <summary>
        /// Converting source type to result type by functor task function
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="func">Functor function</param>
        /// <returns>Result</returns>      
        public static async Task<TResult> MapTaskAsync<TSource, TResult>(this Task<TSource> @this,
                                                                         Func<TSource, TResult> func) =>
            func(await @this);

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        /// Converting source type to result type by functor task function
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="func">Functor function</param>
        /// <returns>Result</returns>       
        public static async ValueTask<TResult> MapValueTaskAsync<TSource, TResult>(this ValueTask<TSource> @this,
                                                                                   Func<TSource, TResult> func) =>
            func(await @this);

        /// <summary>
        /// Converting valueTask to task
        /// </summary>
        /// <typeparam name="TValue">Value</typeparam>
        /// <param name="this">ValueTask</param>
        /// <returns>Task</returns>
        public static async Task<TValue> MapValueToTask<TValue>(this ValueTask<TValue> @this) =>
            await @this;
#endif
    }
}