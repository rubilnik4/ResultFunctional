using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего асинхронного связывающего ответа со значением
    /// </summary>
    public static class ResultCollectionBindWhereAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в асинхронном результирующем ответе со значением
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindContinueAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                            Func<TValueIn, Task<IEnumerable<IErrorResult>>> badFunc) =>
            @this.OkStatus
                ? predicate(@this.Value)
                    ? await okFunc.Invoke(@this.Value)
                    : new ResultValue<TValueOut>(await badFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в асинхронном результирующем ответе со значением
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindWhereAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                         Func<TValueIn, bool> predicate,
                                                                                                         Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                         Func<TValueIn, Task<IResultValue<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? predicate(@this.Value)
                    ? await okFunc.Invoke(@this.Value)
                    : await badFunc.Invoke(@this.Value)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение положительного или негативного условия в асинхронном результирующем ответе со значением
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkBadAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                         Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : await badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                      Func<TValueIn, Task<IResultValue<TValueOut>>> okFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа или возвращение положительного в результирующем ответе
        /// </summary>   
        public static async Task<IResultValue<TValue>> ResultValueBindBadAsync<TValue>(this IResultValue<TValue> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<IResultValue<TValue>>> badFunc) =>
            @this.OkStatus
                ? @this
                : await badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Добавить асинхронно ошибки результирующего ответа или вернуть результат с ошибками для ответа со значением
        /// </summary>
        public static async Task<IResultValue<TValue>> ResultValueBindErrorsOkAsync<TValue>(this IResultValue<TValue> @this,
                                                                                            Func<TValue, Task<IResultError>> okFunc) =>
            await @this.
            ResultValueBindOkAsync(value => okFunc.Invoke(value).
                                            MapTaskAsync(resultError => resultError.ToResultValue(value)));
    }
}