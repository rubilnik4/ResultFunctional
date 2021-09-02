using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Асинхронного преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка для задачи-обхекта
    /// </summary>
    public static class ResultValueCurryBindAsyncExtensions
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента
        /// </summary>
        public static async Task<IResultValue<Func<TOut>>> ResultValueCurryOkBindAsync<TIn1, TOut>(this Task<IResultValue<Func<TIn1, TOut>>> @this,
                                                                                              Task<IResultValue<TIn1>> arg1) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueCurryOkAsync(arg1));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TOut>>> ResultValueCurryOkBindAsync<TIn1, TIn2, TOut>(this Task<IResultValue<Func<TIn1, TIn2, TOut>>> @this,
                                                                                                          Task<IResultValue<TIn1>> arg1) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueCurryOkAsync(arg1));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TOut>>> ResultValueCurryOkBindAsync<TIn1, TIn2, TIn3, TOut>(this Task<IResultValue<Func<TIn1, TIn2, TIn3, TOut>>> @this,
                                                                                                                      Task<IResultValue<TIn1>> arg1) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueCurryOkAsync(arg1));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TOut>>> ResultValueCurryOkBindAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this Task<IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TOut>>> @this,
                                                                                                                                  Task<IResultValue<TIn1>> arg1) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueCurryOkAsync(arg1));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> ResultValueCurryOkBindAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this Task<IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>>> @this,
                                                                                                                                              Task<IResultValue<TIn1>> arg1) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueCurryOkAsync(arg1));
    }
}