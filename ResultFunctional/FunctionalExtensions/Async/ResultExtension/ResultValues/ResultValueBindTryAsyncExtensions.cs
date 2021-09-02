using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultValueBindTryAsyncExtensions
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со связыванием со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultValue<TValue>> ResultValueBindTryAsync<TValue>(Func<Task<IResultValue<TValue>>> func,
                                                                                       IErrorResult error)
        {
            IResultValue<TValue> funcResult;
       
            try
            {
                funcResult = await func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(error.AppendException(ex));
            }

            return funcResult;
        }

        /// <summary>
        /// Результирующий ответ cj связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                         Func<TValueIn, Task<IResultValue<TValueOut>>> func,
                                                                                                         IErrorResult error) =>
            await @this.
            ResultValueBindOkAsync(value => ResultValueBindTryAsync(() => func.Invoke(value), error));
    }
}