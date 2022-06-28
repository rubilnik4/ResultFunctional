using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value async higher order functions
    /// </summary>
    public static class ResultValueCurryAsyncExtensions
    {
        /// <summary>
        /// Get no arguments result value async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TOut>>> ResultValueCurryOkAsync<TIn1, TOut>(this IResultValue<Func<TIn1, TOut>> @this,
                                                                                              Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);

        /// <summary>
        /// Get one argument result value async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TOut>>> ResultValueCurryOkAsync<TIn1, TIn2, TOut>(this IResultValue<Func<TIn1, TIn2, TOut>> @this,
                                                                                                          Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);

        /// <summary>
        /// Get two arguments result value async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value three arguments high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TIn3, TOut>>> ResultValueCurryOkAsync<TIn1, TIn2, TIn3, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TOut>> @this,
                                                                                                                      Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);

        /// <summary>
        /// Get three arguments result value async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value four arguments high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TOut>>> ResultValueCurryOkAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                                  Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);

        /// <summary>
        /// Get four arguments result value async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TIn5">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value five arguments high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> ResultValueCurryOkAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                                              Task<IResultValue<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryOk);
    }
}