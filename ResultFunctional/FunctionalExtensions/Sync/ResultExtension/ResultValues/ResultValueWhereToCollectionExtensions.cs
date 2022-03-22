using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего ответа с значением с возвращением к коллекции
    /// </summary>
    public static class ResultValueWhereToCollectionExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>      
        public static IResultCollection<TValueOut> ResultValueContinueToCollection<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IEnumerable<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
            @this.
            ResultValueContinue(predicate, okFunc, badFunc).
            ToResultCollection();

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>      
        public static IResultCollection<TValueOut> ResultValueOkBadToCollection<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, IEnumerable<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            @this.
            ResultValueOkBad(okFunc, badFunc).
            ToResultCollection();

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static IResultCollection<TValueOut> ResultValueOkToCollection<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, IEnumerable<TValueOut>> okFunc) =>
            @this.
            ResultValueOk(okFunc).
            ToResultCollection();
    }
}