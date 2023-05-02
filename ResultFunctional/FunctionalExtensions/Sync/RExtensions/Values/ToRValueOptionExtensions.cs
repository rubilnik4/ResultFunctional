using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Result value extension methods with condition
    /// </summary>
    public static class ToRValueOptionExtensions
    {
        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueOption<TValue>(this TValue @this, Func<TValue, bool> predicate,
                                                             Func<TValue, IRError> noneFunc)
            where TValue : notnull =>
            @this.Option(predicate,
                         value => value.ToRValue(),
                         value => noneFunc(value).ToRValue<TValue>());
    }
}