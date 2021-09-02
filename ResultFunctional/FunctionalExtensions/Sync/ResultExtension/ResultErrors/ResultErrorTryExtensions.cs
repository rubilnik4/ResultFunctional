using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений
    /// </summary>
    public static class ResultErrorTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ или ошибку исключения
        /// </summary>
        public static IResultError ResultErrorTry(Action action, IErrorResult error)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultError(error.AppendException(ex));
            }

            return new ResultError();
        }
    }
}