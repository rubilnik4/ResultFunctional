using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value async higher order collection functions
    /// </summary>
    public static class ResultValueCurryCollectionAsyncExtensions
    {
        /// <summary>
        /// Get no arguments result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TOut>(this IRValue<Func<IEnumerable<TIn1>, TOut>> @this,
                                                                                                    Task<IRList<TIn1>> arg1)
            where TIn1 : notnull
            where TOut : notnull =>
            await @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TOut>)func).
            ResultValueCurryCollectionOkAsync(arg1);

        /// <summary>
        /// Get no arguments result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TOut>> @this,
                                                                                        Task<IRList<TIn1>> arg1) =>
            await @this.
                ResultValueOk(func => func).
                ResultValueCurryCollectionOkAsync(arg1);

        /// <summary>
        /// Get one argument result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TIn2, TOut>(this IRValue<Func<IEnumerable<TIn1>, TIn2, TOut>> @this,
                                                                                                    Task<IRList<TIn1>> arg1) =>
            await @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TOut>)func).
            ResultValueCurryCollectionOkAsync(arg1);


        /// <summary>
        /// Get one argument result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TIn2, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TIn2, TOut>> @this,
                                                                                          Task<IRList<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryCollectionOk);

        /// <summary>
        /// Get two arguments result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value three arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TIn3, TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TIn2, TIn3, TOut>(this IRValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TOut>> @this,
                                                                                                    Task<IRList<TIn1>> arg1) =>
            await @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TOut>)func).
            ResultValueCurryCollectionOkAsync(arg1);

        /// <summary>
        /// Get two arguments result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value three arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TIn3, TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TIn2, TIn3, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TOut>> @this,
                                                                                                    Task<IRList<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryCollectionOk);

        /// <summary>
        /// Get three arguments result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value four arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TIn3, TIn4, TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this IRValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                            Task<IRList<TIn1>> arg1) =>
            await @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TOut>)func).
            ResultValueCurryCollectionOkAsync(arg1);

        /// <summary>
        /// Get three arguments result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value four arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TIn3, TIn4, TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                  Task<IRList<TIn1>> arg1) =>
             await arg1.
            MapTaskAsync(@this.ResultValueCurryCollectionOk);

        /// <summary>
        /// Get four arguments result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TIn5">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value five arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IRValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                                        Task<IRList<TIn1>> arg1) =>
            await @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>)func).
            ResultValueCurryCollectionOkAsync(arg1);

        /// <summary>
        /// Get four arguments result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TIn5">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value five arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IRValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> ResultValueCurryCollectionOkAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                             Task<IRList<TIn1>> arg1) =>
            await arg1.
            MapTaskAsync(@this.ResultValueCurryCollectionOk);
    }
}