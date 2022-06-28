using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Functor task async functions
    /// </summary>
    public static class MapBindAsyncExtensions
    {
        /// <summary>
        /// Converting source type to result type by functor task async function
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="func">Functor function</param>
        /// <returns>Result</returns>     
        public static async Task<TResult> MapBindAsync<TSource, TResult>(this Task<TSource> @this, 
                                                                         Func<TSource, Task<TResult>> func) =>
            await func(await @this);
    }
}