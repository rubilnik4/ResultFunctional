using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
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
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc,
                                                                                                               Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this
                .MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindContinueAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute monad result collection async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionBindContinueBindAsync(predicate, okFunc,
                                                              values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute monad result collection async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindWhereBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindWhereAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute monad result collection async task function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkBadBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<IRError>, Task<IRList<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindOkBadAsync(okFunc, badFunc));

        /// <summary>
        /// Execute monad result collection async task function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindOkAsync(okFunc));

        /// <summary>
        /// Execute monad result collection async task function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindBadBindAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                          Func<IReadOnlyCollection<IRError>, Task<IRList<TValue>>> badFunc)
            where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindBadAsync(badFunc));

        /// <summary>
        /// Adding errors async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindErrorsOkBindAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                               Func<IReadOnlyCollection<TValue>, Task<IROption>> okFunc)
            where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindErrorsOkAsync(okFunc));


    }
}