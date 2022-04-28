using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений
    /// </summary>
    public static class ResultErrorTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static IResultError ResultErrorTry(Action action, Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultError(exceptionFunc(ex));
            }

            return new ResultError();
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ или ошибку исключения
        /// </summary>
        public static IResultError ResultErrorTry(Action action, IErrorResult error) =>
            ResultErrorTry(action, error.AppendException);
    }
}