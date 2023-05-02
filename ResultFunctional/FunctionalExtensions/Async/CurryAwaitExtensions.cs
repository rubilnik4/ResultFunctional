using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Task async higher order functions
    /// </summary>
    public static class CurryAwaitExtensions
    {
        /// <summary>
        /// Get no arguments task async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">One argument higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>No arguments higher order function</returns>
        public static async Task<TOut> CurryAwait<TIn1, TOut>(this Task<Func<TIn1, TOut>> @this, Task<TIn1> arg1) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.CurryAsync(arg1));

        /// <summary>
        /// Get one argument task async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Two arguments higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>One argument higher order function</returns>
        public static async Task<Func<TIn2, TOut>> CurryAwait<TIn1, TIn2, TOut>(this Task<Func<TIn1, TIn2, TOut>> @this,
                                                                                Task<TIn1> arg1) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.CurryAsync(arg1));

        /// <summary>
        /// Get two arguments task async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Three arguments higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>Two argument higher order function</returns>
        public static async Task<Func<TIn2, TIn3, TOut>> CurryAwait<TIn1, TIn2, TIn3, TOut>(this Task<Func<TIn1, TIn2, TIn3, TOut>> @this,
                                                                                Task<TIn1> arg1) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.CurryAsync(arg1));

        /// <summary>
        /// Get three arguments task async higher order function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Four arguments higher order function</param>
        /// <param name="arg1">Argument</param>
        /// <returns>Three argument higher order function</returns>
        public static async Task<Func<TIn2, TIn3, TIn4, TOut>> CurryAwait<TIn1, TIn2, TIn3, TIn4, TOut>(this Task<Func<TIn1, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                            Task<TIn1> arg1) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.CurryAsync(arg1));

        /// <summary>
        /// Get four arguments task async higher order function
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
        public static async Task<Func<TIn2, TIn3, TIn4, TIn5, TOut>> CurryAwait<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this Task<Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                        Task<TIn1> arg1) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.CurryAsync(arg1));
    }
}