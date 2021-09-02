using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием асинхронно для задачи-объекта
    /// </summary>
    public static class ToResultValueWhereBindAsyncExtensions
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereBindAsync<TValue>(this Task<TValue> @this,
                                                                                           Func<TValue, bool> predicate,
                                                                                           Func<TValue, Task<IErrorResult>> badFunc)
            where TValue : notnull =>
          await @this.
          MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereAsync(predicate, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullBindAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IErrorResult>> badFunc)
            where TValue : class =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereNullAsync(predicate, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullBindAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IErrorResult>> badFunc)
            where TValue : struct =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereNullAsync(predicate, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ToResultValueWhereNullOkBadBindAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                      Func<TValueIn?, Task<IErrorResult>> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereNullOkBadAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ToResultValueWhereNullOkBadBindAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                      Func<TValueIn?, Task<IErrorResult>> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereNullOkBadAsync(predicate, okFunc, badFunc));
    }
}