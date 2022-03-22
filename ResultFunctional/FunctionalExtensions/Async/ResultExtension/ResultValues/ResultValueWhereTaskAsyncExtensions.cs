using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением задачей-объектом
    /// </summary>
    public static class ResultValueWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueContinueTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, TValueOut> okFunc,
                                                                                                            Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueContinue(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение условия в положительном или негативном варианте в результирующем ответе задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueWhereTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, bool> predicate,
                                                                                                         Func<TValueIn, TValueOut> okFunc,
                                                                                                         Func<TValueIn, TValueOut> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueWhere(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultValueOkBadTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, TValueOut> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, TValueOut> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueOkBad(okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultValueOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                      Func<TValueIn, TValueOut> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueOk(okFunc));

        /// <summary>
        /// Выполнение негативного условия или возвращение положительного условия в результирующем ответе задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValue>> ResultValueBadTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, TValue> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBad(badFunc));
    }
}