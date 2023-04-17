using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Result value async extension methods with condition
    /// </summary>
    public static class ToRValueOptionAsyncExtensions
    {
        /// <summary>
        /// Async converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueOptionAsync<TValue>(this TValue @this,
                                                                              Func<TValue, bool> predicate,
                                                                              Func<TValue, Task<IRError>> noneFunc)
            where TValue : notnull =>
            await @this.OptionAsync(predicate,
                                    value => value.ToRValue().ToTask(),
                                    value => noneFunc(value).ToRValueTask<TValue>());
    }
}