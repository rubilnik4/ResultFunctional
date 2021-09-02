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
    /// Асинхронного преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка
    /// </summary>
    public static class ResultValueCurryAsyncExtensions
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента
        /// </summary>
        public static async Task<IResultValue<Func<TOut>>> ResultValueCurryOkAsync<TIn1, TOut>(this IResultValue<Func<TIn1, TOut>> @this,
                                                                                              Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TOut>>> ResultValueCurryOkAsync<TIn1, TIn2, TOut>(this IResultValue<Func<TIn1, TIn2, TOut>> @this,
                                                                                                          Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TOut>>> ResultValueCurryOkAsync<TIn1, TIn2, TIn3, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TOut>> @this,
                                                                                                                      Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TOut>>> ResultValueCurryOkAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                                  Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов
        /// </summary>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> ResultValueCurryOkAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                                              Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);
    }
}