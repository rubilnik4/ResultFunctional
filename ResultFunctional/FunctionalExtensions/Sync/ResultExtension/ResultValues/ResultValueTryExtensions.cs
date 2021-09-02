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
        public static IResultValue<TValue> ResultValueTry<TValue>(Func<TValue> func, IErrorResult error)
        {
            TValue funcResult;

            try
            {
                funcResult = func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(error.AppendException(ex));
            }

            return new ResultValue<TValue>(funcResult);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static IResultValue<TValue> ResultValueTry<TValue>(Func<TValue> func, Func<Exception, IErrorResult> exceptionFunc)
        {
            TValue funcResult;

            try
            {
                funcResult = func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }

            return new ResultValue<TValue>(funcResult);
        }

        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии
        /// </summary>
        public static IResultValue<TValueOut> ResultValueTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> func, 
                                                                                    IErrorResult error) =>
            @this.ResultValueBindOk(value => ResultValueTry(() => func.Invoke(value), error));
    }
}