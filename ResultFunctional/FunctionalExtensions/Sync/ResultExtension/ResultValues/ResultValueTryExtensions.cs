using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений
    /// </summary>
    public static class ResultValueTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static IResultValue<TValue> ResultValueTry<TValue>(Func<TValue> func, Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return new ResultValue<TValue>(func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static IResultValueType<TValue, TError> ResultValueTypeTry<TValue, TError>(Func<TValue> func,
                                                                                          Func<Exception, TError> exceptionFunc)
             where TError : IErrorResult
        {
            try
            {
                return new ResultValueType<TValue, TError>(func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultValueType<TValue, TError>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static IResultValue<TValue> ResultValueTry<TValue>(Func<TValue> func, IErrorResult error) =>
            ResultValueTry(func, error.AppendException);

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static IResultValueType<TValue, TError> ResultValueTypeTry<TValue, TError>(Func<TValue> func, TError error)
            where TError : IErrorBaseExtendResult<TError> =>
            ResultValueTypeTry(func, error.AppendException);
    }
}