using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value async higher order functions
    /// </summary>
    public static class RValueCurryAsyncExtensions
    {
        /// <summary>
        /// Get no arguments result value async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TOut>>> RValueCurryAsync<TIn1, TOut>(this IRValue<Func<TIn1, TOut>> @this,
                                                                                          Task<IRValue<TIn1>> arg1)
            where TIn1 : notnull
            where TOut : notnull =>
            await arg1.
            MapTask(@this.RValueCurry);

        /// <summary>
        /// Get one argument result value async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result value argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TOut>>> RValueCurryAsync<TIn1, TIn2, TOut>(this IRValue<Func<TIn1, TIn2, TOut>> @this,
                                                                                                          Task<IRValue<TIn1>> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TOut : notnull =>
            await arg1.
            MapTask(@this.RValueCurry);

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
        public static async Task<IRValue<Func<TIn2, TIn3, TOut>>> RValueCurryAsync<TIn1, TIn2, TIn3, TOut>(this IRValue<Func<TIn1, TIn2, TIn3, TOut>> @this,
                                                                                                                      Task<IRValue<TIn1>> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TOut : notnull =>
            await arg1.
            MapTask(@this.RValueCurry);

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
        public static async Task<IRValue<Func<TIn2, TIn3, TIn4, TOut>>> RValueCurryAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this IRValue<Func<TIn1, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                                  Task<IRValue<TIn1>> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TIn4 : notnull
            where TOut : notnull =>
            await arg1.
            MapTask(@this.RValueCurry);

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
        public static async Task<IRValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> RValueCurryAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IRValue<Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                                              Task<IRValue<TIn1>> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TIn4 : notnull
            where TIn5 : notnull
            where TOut : notnull =>
            await arg1.
            MapTask(@this.RValueCurry);
    }
}