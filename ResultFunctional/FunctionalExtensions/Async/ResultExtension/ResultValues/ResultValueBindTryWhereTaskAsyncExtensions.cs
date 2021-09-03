using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений для задач-объектов
    /// </summary>
    public static class ResultValueBindTryWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Результирующий ответ cj связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                       Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                                       Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultValueBindOkTaskAsync(value => ResultValueBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ cj связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                       Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                                       IErrorResult error) =>
            await @this.
            ResultValueBindOkTaskAsync(value => ResultValueBindTry(() => func.Invoke(value), error));
    }
}