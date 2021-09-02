using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа со значением
    /// </summary>
    public static class ResultValueWhereAsyncExtensions
    {
        /// <summary>
        /// Выполнение асинхронного условия или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueContinueAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                        Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                        Func<TValueIn, Task<IEnumerable<IErrorResult>>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultValue<TValueOut>(await okFunc.Invoke(@this.Value))
                 : new ResultValue<TValueOut>(await badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение асинхронного положительного или негативного условия в результирующем ответе
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueOkBadAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                     Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                                     Func<IReadOnlyCollection<IErrorResult>, Task<TValueOut>> badFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(await okFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(await badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Выполнение асинхронного положительного условия или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultValueOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<TValueOut>> okFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(await okFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение асинхронного негативного условия или возвращение положительного условия в результирующем ответе
        /// </summary>   
        public static async Task<IResultValue<TValue>> ResultValueBadAsync<TValue>(this IResultValue<TValue> @this,
                                                                                   Func<IReadOnlyCollection<IErrorResult>, Task<TValue>> badFunc) =>

            @this.OkStatus
                ? @this
                : new ResultValue<TValue>(await badFunc.Invoke(@this.Errors));
    }
}