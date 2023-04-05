using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task result value async higher order collection functions
    /// </summary>
    public static class ResultValueCurryCollectionBindAsyncExtensions
    {
        /// <summary>
        /// Get no arguments task result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TOut>(this Task<IResultValue<Func<IEnumerable<TIn1>, TOut>>> @this,
                                                                                                        Task<IResultCollection<TIn1>> arg1) =>
            await @this.
            ResultValueOkTaskAsync(func => (Func<IReadOnlyCollection<TIn1>, TOut>)func).
            ResultValueCurryCollectionOkBindAsync(arg1);

        /// <summary>
        /// Get no arguments task result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TOut>(this Task<IResultValue<Func<IReadOnlyCollection<TIn1>, TOut>>> @this,
                                                                                        Task<IResultCollection<TIn1>> arg1) =>
            await arg1.
            MapBindAsync(@this.ResultValueCurryCollectionOkTaskAsync);

        /// <summary>
        /// Get one argument task result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TIn2, TOut>(this Task<IResultValue<Func<IEnumerable<TIn1>, TIn2, TOut>>> @this,
                                                                                                    Task<IResultCollection<TIn1>> arg1) =>
            await @this.
            ResultValueOkTaskAsync(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TOut>)func).
            ResultValueCurryCollectionOkBindAsync(arg1);


        /// <summary>
        /// Get one argument task result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TIn2, TOut>(this Task<IResultValue<Func<IReadOnlyCollection<TIn1>, TIn2, TOut>>> @this,
                                                                                          Task<IResultCollection<TIn1>> arg1) =>
            await arg1.
            MapBindAsync(@this.ResultValueCurryCollectionOkTaskAsync);

        /// <summary>
        /// Get two arguments task result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value three arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TIn3, TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TIn2, TIn3, TOut>(this Task<IResultValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TOut>>> @this,
                                                                                                    Task<IResultCollection<TIn1>> arg1) =>
            await @this.
            ResultValueOkTaskAsync(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TOut>)func).
            ResultValueCurryCollectionOkBindAsync(arg1);

        /// <summary>
        /// Get two arguments task result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value three arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TIn3, TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TIn2, TIn3, TOut>(this Task<IResultValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TOut>>> @this,
                                                                                                    Task<IResultCollection<TIn1>> arg1) =>
            await arg1.
            MapBindAsync(@this.ResultValueCurryCollectionOkTaskAsync);

        /// <summary>
        /// Get three arguments task result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value four arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this Task<IResultValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TIn4, TOut>>> @this,
                                                                                                                            Task<IResultCollection<TIn1>> arg1) =>
            await @this.
            ResultValueOkTaskAsync(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TOut>)func).
            ResultValueCurryCollectionOkBindAsync(arg1);

        /// <summary>
        /// Get three arguments task result value async higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value four arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TIn2, TIn3, TIn4, TOut>(this Task<IResultValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TOut>>> @this,
                                                                                                                  Task<IResultCollection<TIn1>> arg1) =>
            await arg1.
            MapBindAsync(@this.ResultValueCurryCollectionOkTaskAsync);

        /// <summary>
        /// Get four arguments task result value async higher order collection function
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
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this Task<IResultValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>>> @this,
                                                                                                                                        Task<IResultCollection<TIn1>> arg1) =>
            await @this.
            ResultValueOkTaskAsync(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>)func).
            ResultValueCurryCollectionOkBindAsync(arg1);

        /// <summary>
        /// Get four arguments task result value async higher order collection function
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
        public static async Task<IResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>> ResultValueCurryCollectionOkBindAsync<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this Task<IResultValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>>> @this,
                                                                                                                             Task<IResultCollection<TIn1>> arg1) =>
            await arg1.
            MapBindAsync(@this.ResultValueCurryCollectionOkTaskAsync);
    }
}