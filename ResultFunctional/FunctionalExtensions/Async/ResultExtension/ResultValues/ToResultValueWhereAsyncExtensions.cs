using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием асинхронно
    /// </summary>
    public static class ToResultValueWhereAsyncExtensions
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereAsync<TValue>(this TValue @this,
                                                                                       Func<TValue, bool> predicate,
                                                                                       Func<TValue, Task<IErrorResult>> badFunc)
            where TValue : notnull =>
          await @this.WhereContinueAsync(predicate,
                              value => Task.FromResult(new ResultValue<TValue>(value)),
                              value => badFunc(value).
                                       MapTaskAsync(error => new ResultValue<TValue>(error)));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullAsync<TValue>(this TValue? @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IErrorResult>> badFunc)
            where TValue : class =>
            await @this.ToResultValueNullCheckAsync(badFunc(@this)).
            ResultValueBindOkBindAsync(value => value.ToResultValueWhereAsync(predicate, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullAsync<TValue>(this TValue? @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IErrorResult>> badFunc)
            where TValue : struct =>
            await @this.ToResultValueNullCheckAsync(badFunc(@this)).
            ResultValueBindOkBindAsync(value => value.ToResultValueWhereAsync(predicate, valueWhere => badFunc(valueWhere)));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
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
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
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