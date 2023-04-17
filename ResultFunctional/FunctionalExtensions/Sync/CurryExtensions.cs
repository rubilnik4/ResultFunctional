using System;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Higher order functions
    /// </summary>
    public static class CurryExtensions
    {
        /// <summary>
        /// Get no arguments higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">One argument higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>No arguments higher order function</returns>
        public static TOut Curry<TIn1, TOut>(this Func<TIn1, TOut> @this, TIn1 arg1) =>
            @this(arg1);

        /// <summary>
        /// Get one argument higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Two arguments higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>One argument higher order function</returns>
        public static Func<TIn2, TOut> Curry<TIn1, TIn2, TOut>(this Func<TIn1, TIn2, TOut> @this, TIn1 arg1) =>
            arg2 => @this(arg1, arg2);

        /// <summary>
        /// Get two arguments higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Three arguments higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>Two argument higher order function</returns>
        public static Func<TIn2, TIn3, TOut> Curry<TIn1, TIn2, TIn3, TOut>(this Func<TIn1, TIn2, TIn3, TOut> @this, TIn1 arg1) =>
            (arg2, arg3) => @this(arg1, arg2, arg3);

        /// <summary>
        /// Get three arguments higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Four arguments higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>Three argument higher order function</returns>
        public static Func<TIn2, TIn3, TIn4, TOut> Curry<TIn1, TIn2, TIn3, TIn4, TOut>(this Func<TIn1, TIn2, TIn3, TIn4, TOut> @this,
                                                                                       TIn1 arg1) =>
            (arg2, arg3, arg4) => @this(arg1, arg2, arg3, arg4);

        /// <summary>
        /// Get four arguments higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TIn5">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Five arguments higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>Four argument higher order function</returns>
        public static Func<TIn2, TIn3, TIn4, TIn5, TOut> Curry<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> @this,
                                                                                                   TIn1 arg1) =>
            (arg2, arg3, arg4, arg5) => @this(arg1, arg2, arg3, arg4, arg5);
    }
}