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
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereAsync<TValue>(this TValue @this,
                                                                                  Func<TValue, bool> predicate,
                                                                                  Func<TValue, Task<IRError>> badFunc)
            where TValue : notnull =>
          await @this.WhereContinueAsync(predicate,
                              value => value.ToRValue().ToTask(),
                              value => badFunc(value).ToRValueTaskAsync<TValue>());

        /// <summary>
        /// Async converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereNullAsync<TValue>(this TValue? @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IRError>> badFunc)
            where TValue : class =>
            await @this.ToResultValueNullCheckAsync(badFunc(@this)).
            ResultValueBindOkBindAsync(value => value.ToResultValueWhereAsync(predicate, badFunc));

        /// <summary>
        /// Async converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereNullAsync<TValue>(this TValue? @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IRError>> badFunc)
            where TValue : struct =>
            await @this.ToResultValueNullCheckAsync(badFunc(@this)).
            ResultValueBindOkBindAsync(value => value.ToResultValueWhereAsync(predicate, valueWhere => badFunc(valueWhere)));

        /// <summary>
        /// Async converting value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ToResultValueWhereNullOkBadAsync<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                      Func<TValueIn?, Task<IRError>> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            await @this.ToResultValueNullCheckAsync(badFunc(@this)).
            ResultValueBindWhereBindAsync(predicate,
                                          value => okFunc.Invoke(value).ToRValueTaskAsync(),
                                          value => badFunc(value).ToRValueTaskAsync<TValueOut>());

        /// <summary>
        /// Async converting value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ToResultValueWhereNullOkBadAsync<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                      Func<TValueIn?, Task<IRError>> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            await @this.ToResultValueNullCheckAsync(badFunc(@this)).
            ResultValueBindWhereBindAsync(predicate,
                                          value => okFunc.Invoke(value).ToRValueTaskAsync(),
                                          value => badFunc(value).ToRValueTaskAsync<TValueOut>());
    }
}