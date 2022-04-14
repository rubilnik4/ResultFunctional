using System;
using System.Collections.Generic;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Functor functions
    /// </summary>
    public static class MapExtensions
    {
        /// <summary>
        /// Converting source type to result type by functor function
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="func">Functor function</param>
        /// <returns>Result</returns>
        public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> func) =>
            func(@this);
    }
}
