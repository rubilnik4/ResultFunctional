using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.ResultFactory
{
    /// <summary>
    /// Фабрика для создания результирующего ответа
    /// </summary>
    public static class ResultErrorFactory
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        public static Task<IResultError> CreateTaskResultError()=>
            Task.FromResult((IResultError)new ResultError());

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static Task<IResultError> CreateTaskResultError(IErrorResult errorType)=>
            Task.FromResult((IResultError)new ResultError(errorType));

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static Task<IResultError> CreateTaskResultError(IEnumerable<IErrorResult> errors)=>
            Task.FromResult((IResultError)new ResultError(errors));

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static Task<IResultError> CreateTaskResultError(IResultError error) =>
            Task.FromResult(error);

        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        public static async Task<IResultError> CreateTaskResultErrorAsync() =>
            await Task.FromResult((IResultError)new ResultError());

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static async Task<IResultError> CreateTaskResultErrorAsync(IErrorResult errorType)=>
            await Task.FromResult((IResultError)new ResultError(errorType));

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static async Task<IResultError> CreateTaskResultErrorAsync(IEnumerable<IErrorResult> errors) =>
            await Task.FromResult((IResultError)new ResultError(errors));
    }
}