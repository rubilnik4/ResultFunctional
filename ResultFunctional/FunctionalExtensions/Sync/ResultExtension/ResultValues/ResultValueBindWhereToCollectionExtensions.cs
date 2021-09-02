using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего ответа со связыванием с значением с возвращением к коллекции
    /// </summary>
    public static class ResultValueBindWhereToCollectionExtensions
    {
        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static IResultCollection<TValueOut> ResultValueBindOkToCollection<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, IResultCollection<TValueOut>> okFunc) =>
            @this.
            ResultValueBindOk(okFunc).
            ToResultCollection();
    }
}