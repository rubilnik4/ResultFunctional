using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Task result value async extension methods with condition
    /// </summary>
    public static class ToRValueOptionAwaitExtensions
    {
        /// <summary>
        /// Async converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueOptionAwait<TValue>(this Task<TValue> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue, Task<IRError>> noneFunc)
            where TValue : notnull =>
          await @this.
          MapAwait(thisAwaited => thisAwaited.ToRValueOptionAsync(predicate, noneFunc));
    }
}