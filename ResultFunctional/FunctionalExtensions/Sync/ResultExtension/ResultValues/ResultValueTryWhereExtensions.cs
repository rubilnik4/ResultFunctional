using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений
    /// </summary>
    public static class ResultValueTryWhereExtensions
    {
        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии
        /// </summary>
        public static IResultValue<TValueOut> ResultValueTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> func,
                                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            @this.ResultValueBindOk(value => ResultValueTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии
        /// </summary>
        public static IResultValue<TValueOut> ResultValueTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> func,
                                                                                    IErrorResult error) =>
            @this.ResultValueBindOk(value => ResultValueTry(() => func.Invoke(value), error));
    }
}