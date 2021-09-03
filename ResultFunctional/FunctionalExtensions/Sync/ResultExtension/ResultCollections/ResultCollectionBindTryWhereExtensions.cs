using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections.ResultCollectionBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием с коллекцией и обработкой исключений
    /// </summary>
    public static class ResultCollectionBindTryWhereExtensions
    {
        /// <summary>
        /// Связать результирующий ответ со значением со связыванием с обработкой функции при положительном условии
        /// </summary>
        public static IResultCollection<TValueOut> ResultCollectionBindTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> func,
                                                                                                  Func<Exception, IErrorResult> exceptionFunc) =>
            @this.ResultCollectionBindOk(value => ResultCollectionBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Связать результирующий ответ со значением со связыванием с обработкой функции при положительном условии
        /// </summary>
        public static IResultCollection<TValueOut> ResultCollectionBindTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> func,
                                                                                                  IErrorResult error) =>
            @this.ResultCollectionBindOk(value => ResultCollectionBindTry(() => func.Invoke(value), error));
    }
}