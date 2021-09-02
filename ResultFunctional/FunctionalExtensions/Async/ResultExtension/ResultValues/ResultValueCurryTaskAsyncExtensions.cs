using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка для задачи-объекта
    /// </summary>
    public static class ResultValueCurryTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента
        /// </summary>
        public static async Task<IResultValue<Func<TOut>>> ResultValueCurryOkTaskAsync<TIn1, TOut>(this Task<IResultValue<Func<TIn1, TOut>>> @this,
                                                                                                  IResultValue<TIn1> arg1) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueCurryOk(arg1));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TOut>>> ResultValueCurryOkTaskAsync<TIn1, TIn2, TOut>(this Task<IResultValue<Func<TIn1, TIn2, TOut>>> @this,
                                                                                                          IResultValue<TIn1> arg1) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueCurryOk(arg1));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TOut>>> ResultValueCurryOkTaskAsync<TIn1, TIn2, TIn3, TOut>(this Task<IResultValue<Func<TIn1, TIn2, TIn3, TOut>>> @this,
                                                                                                                      IResultValue<TIn1> arg1) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueCurryOk(arg1));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TOut>>> ResultValueCurryOkTaskAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this Task<IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TOut>>> @this,
                                                                                                                                      IResultValue<TIn1> arg1) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueCurryOk(arg1));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> ResultValueCurryOkTaskAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this Task<IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>>> @this,
                                                                                                                                              IResultValue<TIn1> arg1) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueCurryOk(arg1));
    }
}