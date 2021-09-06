using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueBindTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений асинхронно для задач-объектов
    /// </summary>
    public static class ResultValueBindTryWhereBindAsyncExtensions
    {
        /// <summary>
        /// Результирующий ответ cj связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                       Func<TValueIn, Task<IResultValue<TValueOut>>> func,
                                                                                                       Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultValueBindOkBindAsync(value => ResultValueBindTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ cj связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                       Func<TValueIn, Task<IResultValue<TValueOut>>> func,
                                                                                                       IErrorResult error) =>
            await @this.
            ResultValueBindOkBindAsync(value => ResultValueBindTryAsync(() => func.Invoke(value), error));
    }
}