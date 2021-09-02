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
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultValueTryAsyncExtensions
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultValue<TValue>> ResultValueTryAsync<TValue>(Func<Task<TValue>> func, IErrorResult error)
        {
            TValue funcResult;
       
            try
            {
                funcResult = await func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(error.AppendException(ex));
            }

            return new ResultValue<TValue>(funcResult);
        }

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