using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
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
        public static async Task<IResultValue<TValue>> ToResultValueWhereAsync<TValue>(this TValue @this,
                                                                                       Func<TValue, bool> predicate,
                                                                                       Func<TValue, Task<IErrorResult>> badFunc)
            where TValue : notnull =>
          await @this.WhereContinueAsync(predicate,
                              value => Task.FromResult(new ResultValue<TValue>(value)),
                              value => badFunc(value).
                                       MapTaskAsync(error => new ResultValue<TValue>(error)));

        /// <summary>
        /// Async converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullAsync<TValue>(this TValue? @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IErrorResult>> badFunc)
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
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullAsync<TValue>(this TValue? @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IErrorResult>> badFunc)
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
        public static async Task<IResultValue<TValueOut>> ToResultValueWhereNullOkBadAsync<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                      Func<TValueIn?, Task<IErrorResult>> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            await @this.ToResultValueNullCheckAsync(badFunc(@this)).
            ResultValueBindWhereBindAsync<TValueIn, TValueOut>(predicate,
                                 async value => new ResultValue<TValueOut>(await okFunc.Invoke(value)),
                                 async value => new ResultValue<TValueOut>(await badFunc(value)));

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
        public static async Task<IResultValue<TValueOut>> ToResultValueWhereNullOkBadAsync<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                      Func<TValueIn?, Task<IErrorResult>> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            await @this.ToResultValueNullCheckAsync(badFunc(@this)).
            ResultValueBindWhereBindAsync<TValueIn, TValueOut>(predicate,
                                 async value => new ResultValue<TValueOut>(await okFunc.Invoke(value)),
                                 async value => new ResultValue<TValueOut>(await badFunc(value)));
    }
}