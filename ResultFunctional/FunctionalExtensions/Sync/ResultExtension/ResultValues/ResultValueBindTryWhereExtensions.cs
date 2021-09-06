using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений
    /// </summary>
    public static class ResultValueBindTryWhereExtensions
    {
        /// <summary>
        /// Связать результирующий ответ со значением со связыванием с обработкой функции при положительном условии
        /// </summary>
        public static IResultValue<TValueOut> ResultValueBindTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                        Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                        Func<Exception, IErrorResult> exceptionFunc) =>
            @this.ResultValueBindOk(value => ResultValueBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Связать результирующий ответ со значением со связыванием с обработкой функции при положительном условии
        /// </summary>
        public static IResultValue<TValueOut> ResultValueBindTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                        Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                        IErrorResult error) =>
            @this.ResultValueBindOk(value => ResultValueBindTry(() => func.Invoke(value), error));
    }
}