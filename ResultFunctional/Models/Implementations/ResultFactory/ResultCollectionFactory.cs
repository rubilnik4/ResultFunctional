using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.ResultFactory
{
    /// <summary>
    /// Result collection factory
    /// </summary>
    public static class ResultCollectionFactory
    {
        /// <summary>
        /// Create result collection by values
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="values">Values</param>
        /// <returns>Result collection in task</returns>
        public static Task<IResultCollection<TValue>> CreateTaskResultCollection<TValue>(IEnumerable<TValue> values)
            where TValue : notnull =>
            Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(values));

        /// <summary>
        /// Create result collection by error
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="error">Error</param>
        /// <returns>Result collection in task</returns>
        public static Task<IResultCollection<TValue>> CreateTaskResultCollectionError<TValue>(IErrorResult error)
            where TValue : notnull =>
            Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(error));

        /// <summary>
        /// Create result collection by errors
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Result collection in task</returns>
        public static Task<IResultCollection<TValue>> CreateTaskResultCollectionError<TValue>(IEnumerable<IErrorResult> errors)
            where TValue : notnull =>
            Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(errors));

        ///// <summary>
        ///// Создать асинхронный результирующий ответ с коллекцией
        ///// </summary>
        //public static async Task<IResultCollection<TValue>> CreateTaskResultCollectionAsync<TValue>(IEnumerable<TValue> value)
        //    where TValue : notnull =>
        //    await Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(value));

        ///// <summary>
        ///// Создать асинхронный результирующий ответ с коллекцией и ошибкой
        ///// </summary>
        //public static async Task<IResultCollection<TValue>> CreateTaskResultCollectionErrorAsync<TValue>(IErrorResult error)
        //    where TValue : notnull =>
        //    await Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(error));

        ///// <summary>
        ///// Создать асинхронный результирующий ответ с коллекцией и ошибкой
        ///// </summary>
        //public static async Task<IResultCollection<TValue>> CreateTaskResultCollectionErrorAsync<TValue>(IEnumerable<IErrorResult> errors)
        //    where TValue : notnull =>
        //    await Task.FromResult((IResultCollection<TValue>)new ResultCollection<TValue>(errors));
    }
}