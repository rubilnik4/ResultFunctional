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
    public static class ResultCollectionTryAsyncExtensions
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IEnumerable<TValue>>> func,
                                                                                             Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return new ResultCollection<TValue>(await func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IEnumerable<TValue>>> func,
                                                                                             IErrorResult error) =>
            await ResultCollectionTryAsync(func, error.AppendException);

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IReadOnlyCollection<TValue>>> func,
                                                                                             IErrorResult error) =>
            await ResultCollectionTryAsync(async () => (IEnumerable<TValue>)await func.Invoke(), error);


        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<List<TValue>>> func,
                                                                                             IErrorResult error) =>
            await ResultCollectionTryAsync(async () => (IEnumerable<TValue>)await func.Invoke(), error);
    }
}