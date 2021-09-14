using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

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
        public static async Task<IResultValue<TValue>> ResultValueTryAsync<TValue>(Func<Task<TValue>> func,
                                                                                   Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return new ResultValue<TValue>(await func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultValue<TValue>> ResultValueTryAsync<TValue>(Func<Task<TValue>> func, IErrorResult error) =>
            await ResultValueTryAsync(func, error.AppendException);
    }
}