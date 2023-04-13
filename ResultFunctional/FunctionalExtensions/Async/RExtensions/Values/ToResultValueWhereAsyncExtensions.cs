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
    public static class ToResultValueWhereAsyncExtensions
    {
        /// <summary>
        /// Async converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereAsync<TValue>(this TValue @this,
                                                                                  Func<TValue, bool> predicate,
                                                                                  Func<TValue, Task<IRError>> noneFunc)
            where TValue : notnull =>
          await @this.OptionAsync(predicate,
                              value => value.ToRValue().ToTask(),
                              value => noneFunc(value).ToRValueTaskAsync<TValue>());

        /// <summary>
        /// Async converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereNullAsync<TValue>(this TValue? @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IRError>> noneFunc)
            where TValue : class =>
            await @this.ToResultValueNullCheckAsync(noneFunc(@this)).
            ResultValueBindOkBindAsync(value => value.ToResultValueWhereAsync(predicate, noneFunc));

        /// <summary>
        /// Async converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereNullAsync<TValue>(this TValue? @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IRError>> noneFunc)
            where TValue : struct =>
            await @this.ToResultValueNullCheckAsync(noneFunc(@this)).
            ResultValueBindOkBindAsync(value => value.ToResultValueWhereAsync(predicate, valueWhere => noneFunc(valueWhere)));

        /// <summary>
        /// Async converting value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ToResultValueWhereNullOkBadAsync<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                      Func<TValueIn?, Task<IRError>> noneFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            await @this.ToResultValueNullCheckAsync(noneFunc(@this)).
            ResultValueBindWhereBindAsync(predicate,
                                          value => someFunc.Invoke(value).ToRValueTaskAsync(),
                                          value => noneFunc(value).ToRValueTaskAsync<TValueOut>());

        /// <summary>
        /// Async converting value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ToResultValueWhereNullOkBadAsync<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                      Func<TValueIn?, Task<IRError>> noneFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            await @this.ToResultValueNullCheckAsync(noneFunc(@this)).
            ResultValueBindWhereBindAsync(predicate,
                                          value => someFunc.Invoke(value).ToRValueTaskAsync(),
                                          value => noneFunc(value).ToRValueTaskAsync<TValueOut>());
    }
}