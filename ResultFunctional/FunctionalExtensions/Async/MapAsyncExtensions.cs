using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Async functor functions
    /// </summary>
    public static class MapAsyncExtensions
    {
        /// <summary>
        /// Converting source type to result type by functor function async
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="func">Functor function</param>
        /// <returns>Result</returns>    
        public static async Task<TResult> MapAsync<TSource, TResult>(this TSource @this, Func<TSource, Task<TResult>> func) =>
            await func(@this);
    }
}