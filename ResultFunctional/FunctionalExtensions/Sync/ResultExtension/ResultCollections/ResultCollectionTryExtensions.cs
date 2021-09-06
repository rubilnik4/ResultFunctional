using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со коллекцией и обработкой исключений
    /// </summary>
    public static class ResultCollectionTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией или ошибку исключения
        /// </summary>
        public static IResultCollection<TValue> ResultCollectionTry<TValue>(Func<IEnumerable<TValue>> func,
                                                                            Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                return new ResultCollection<TValue>(func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией или ошибку исключения
        /// </summary>
        public static IResultCollection<TValue> ResultCollectionTry<TValue>(Func<IEnumerable<TValue>> func, IErrorResult error) =>
            ResultCollectionTry(func, error.AppendException);
    }
}