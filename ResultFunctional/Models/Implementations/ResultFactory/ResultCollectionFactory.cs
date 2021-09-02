using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.ResultFactory
{
    /// <summary>
    /// Фабрика для создания результирующего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionFactory
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ с коллекцией
        /// </summary>
        public static Task<IResultCollection<TValue>> CreateTaskResultCollection<TValue>(IEnumerable<TValue> value)
            where TValue : notnull =>
            Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(value));

        /// <summary>
        /// Создать асинхронный результирующий ответ с коллекцией и ошибкой
        /// </summary>
        public static Task<IResultCollection<TValue>> CreateTaskResultCollectionError<TValue>(IErrorResult error)
            where TValue : notnull =>
            Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(error));

        /// <summary>
        /// Создать асинхронный результирующий ответ с коллекцией и ошибкой
        /// </summary>
        public static Task<IResultCollection<TValue>> CreateTaskResultCollectionError<TValue>(IEnumerable<IErrorResult> errors)
            where TValue : notnull =>
            Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(errors));

        /// <summary>
        /// Создать асинхронный результирующий ответ с коллекцией
        /// </summary>
        public static async Task<IResultCollection<TValue>> CreateTaskResultCollectionAsync<TValue>(IEnumerable<TValue> value)
            where TValue : notnull =>
            await Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(value));

        /// <summary>
        /// Создать асинхронный результирующий ответ с коллекцией и ошибкой
        /// </summary>
        public static async Task<IResultCollection<TValue>> CreateTaskResultCollectionErrorAsync<TValue>(IErrorResult error)
            where TValue : notnull =>
            await Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(error));

        /// <summary>
        /// Создать асинхронный результирующий ответ с коллекцией и ошибкой
        /// </summary>
        public static async Task<IResultCollection<TValue>> CreateTaskResultCollectionErrorAsync<TValue>(IEnumerable<IErrorResult> errors)
            where TValue : notnull =>
            await Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(errors));
    }
}