using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

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
                                                                                       Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со связыванием со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultValue<TValue>> ResultValueBindTryAsync<TValue>(Func<Task<IResultValue<TValue>>> func,
                                                                                       IErrorResult error) =>
             await ResultValueBindTryAsync(func, error.AppendException);
    }
}