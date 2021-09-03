using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultValueTryWhereAsyncExtensions
    {
        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                     Func<TValueIn, Task<TValueOut>> func,
                                                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultValueBindOkAsync(value => ResultValueTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                     Func<TValueIn, Task<TValueOut>> func,
                                                                                                     IErrorResult error) =>
            await @this.
            ResultValueBindOkAsync(value => ResultValueTryAsync(() => func.Invoke(value), error));
    }
}