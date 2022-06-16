using System;
using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value higher order functions
    /// </summary>
    public static class ResultValueCurryExtensions
    {
        /// <summary>
        /// Get no arguments result value higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static IResultValue<Func<TOut>> ResultValueCurryOk<TIn1, TOut>(this IResultValue<Func<TIn1, TOut>> @this,
                                                                              IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TOut>>(@this.Errors.Concat(arg1.Errors));

        /// <summary>
        /// Get one argument result value higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static IResultValue<Func<TIn2, TOut>> ResultValueCurryOk<TIn1, TIn2, TOut>(this IResultValue<Func<TIn1, TIn2, TOut>> @this,
                                                                                          IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TIn2, TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TIn2, TOut>>(@this.Errors.Concat(arg1.Errors));

        /// <summary>
        /// Get two arguments result value higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value three arguments high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static IResultValue<Func<TIn2, TIn3, TOut>> ResultValueCurryOk<TIn1, TIn2, TIn3, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TOut>> @this,
                                                                                                     IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TIn2, TIn3, TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TIn2, TIn3, TOut>>(@this.Errors.Concat(arg1.Errors));

        /// <summary>
        /// Get three arguments result value higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value four arguments high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static IResultValue<Func<TIn2, TIn3, TIn4, TOut>> ResultValueCurryOk<TIn1, TIn2, TIn3, TIn4, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                  IResultValue<TIn1> arg1) =>
             @this.OkStatus && arg1.OkStatus
                 ? new ResultValue<Func<TIn2, TIn3, TIn4, TOut>>(@this.Value.Curry(arg1.Value))
                 : new ResultValue<Func<TIn2, TIn3, TIn4, TOut>>(@this.Errors.Concat(arg1.Errors));

        /// <summary>
        /// Get four arguments result value higher order function
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
        public static IResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>> ResultValueCurryOk<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                             IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>(@this.Errors.Concat(arg1.Errors));
    }
}