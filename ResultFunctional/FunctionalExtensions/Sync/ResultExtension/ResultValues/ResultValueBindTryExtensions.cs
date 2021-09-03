using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений
    /// </summary>
    public static class ResultValueBindTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со связыванием со значением или ошибку исключения
        /// </summary>
        public static IResultValue<TValue> ResultValueBindTry<TValue>(Func<IResultValue<TValue>> func,
                                                                      Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со связыванием со значением или ошибку исключения
        /// </summary>
        public static IResultValue<TValue> ResultValueBindTry<TValue>(Func<IResultValue<TValue>> func, IErrorResult error) => 
            ResultValueBindTry(func, error.AppendException);
    }
}