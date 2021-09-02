using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа задачи-объекта
    /// </summary>
    public static class ResultErrorBindWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static async Task<IResultError> ResultErrorBindOkBadTaskAsync(this Task<IResultError> @this,
                                                             Func<IResultError> okFunc,
                                                             Func<IReadOnlyCollection<IErrorResult>, IResultError> badFunc) =>
           await @this.
           MapTaskAsync(awaitedThis => awaitedThis.ResultErrorBindOkBad(okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static async Task<IResultError> ResultErrorBindOkTaskAsync(this Task<IResultError> @this,
                                                                          Func<IResultError> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultErrorBindOk(okFunc));
    }
}