using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Options;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection monad async task function with conditions
    /// </summary>
    public static class ResultCollectionBindWhereBindAsyncExtensions
    {
        /// <summary>
        /// Execute monad result collection async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this
                .MapAwait(awaitedThis => awaitedThis.ResultCollectionBindContinueAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute monad result collection async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionBindContinueBindAsync(predicate, someFunc,
                                                              values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute monad result collection async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindWhereBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultCollectionBindWhereAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute monad result collection async task function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkBadBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<IRError>, Task<IRList<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultCollectionBindOkBadAsync(someFunc, noneFunc));

        /// <summary>
        /// Execute monad result collection async task function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultCollectionBindOkAsync(someFunc));

        /// <summary>
        /// Execute monad result collection async task function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindBadBindAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                          Func<IReadOnlyCollection<IRError>, Task<IRList<TValue>>> noneFunc)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultCollectionBindBadAsync(noneFunc));

        /// <summary>
        /// Adding errors async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindErrorsOkBindAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                               Func<IReadOnlyCollection<TValue>, Task<IROption>> someFunc)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultCollectionBindErrorsOkAsync(someFunc));


    }
}