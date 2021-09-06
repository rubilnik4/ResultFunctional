using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultCollectionBindTryAsyncExtensions
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindTryAsync<TValue>(Func<Task<IResultCollection<TValue>>> func,
                                                                                             Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindTryAsync<TValue>(Func<Task<IResultCollection<TValue>>> func,
                                                                                             IErrorResult error) =>
            await ResultCollectionBindTryAsync(func, error.AppendException);
    }
}