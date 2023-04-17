using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Task result value extension methods with condition
    /// </summary>
    public static class ToRValueOptionTaskExtensions
    {
        /// <summary>
        /// Converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueOptionTask<TValue>(this Task<TValue> @this,
                                                                             Func<TValue, bool> predicate,
                                                                             Func<TValue, IRError> noneFunc)
            where TValue : notnull =>
            await @this.
                MapTask(thisAwaited => thisAwaited.ToRValueOption(predicate, noneFunc));
    }
}