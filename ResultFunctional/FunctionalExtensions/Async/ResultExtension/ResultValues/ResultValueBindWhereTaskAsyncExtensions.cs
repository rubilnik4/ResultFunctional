using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением для задачи-объекта
    /// </summary>
    public static class ResultValueBindWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе со значением для задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindContinueTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                                Func<TValueIn, bool> predicate,
                                                                                                                Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                                                Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindContinue(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе со значением для задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindWhereTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, bool> predicate,
                                                                                                             Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                                             Func<TValueIn, IResultValue<TValueOut>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindWhere(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе со значением для задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkBadTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                             Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                                             Func<IReadOnlyCollection<IErrorResult>, IResultValue<TValueOut>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindOkBad(okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного условия результирующего ответа или возвращение предыдущей ошибки в результирующем ответе для задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                          Func<TValueIn, IResultValue<TValueOut>> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindOk(okFunc));

        /// <summary>
        /// Выполнение негативного условия результирующего ответа или возвращение положительного в результирующем ответе для задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValue>> ResultValueBindBadTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, IResultValue<TValue>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindBad(badFunc));

        /// <summary>
        /// Добавить ошибки результирующего ответа или вернуть результат с ошибками для ответа со значением для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ResultValueBindErrorsOkTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                            Func<TValue, IResultError> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindErrorsOk(okFunc));
    }
}