using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием с коллекцией и обработкой исключений
    /// </summary>
    public static class ResultCollectionBindTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со связыванием со значением или ошибку исключения
        /// </summary>
        public static IResultCollection<TValue> ResultCollectionBindTry<TValue>(Func<IResultCollection<TValue>> func,
                                                                                Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией или ошибку исключения
        /// </summary>
        public static IResultCollection<TValue> ResultCollectionBindTry<TValue>(Func<IResultCollection<TValue>> func, IErrorResult error) =>
            ResultCollectionBindTry(func, error.AppendException);
    }
}