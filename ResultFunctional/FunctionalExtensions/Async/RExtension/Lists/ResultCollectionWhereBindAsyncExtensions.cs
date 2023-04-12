 using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
 using ResultFunctional.Models.Lists;

 namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
{
    /// <summary>
    /// Extension methods for task result collection async functor function with conditions
    /// </summary>
    public static class ResultCollectionWhereBindAsyncExtensions
    {
        /// <summary>
        /// Execute task result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>        
        public static async Task<IRList<TValueOut>> ResultCollectionContinueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapBindAsync(awaitedThis => awaitedThis.ResultCollectionContinueAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>        
        public static async Task<IRList<TValueOut>> ResultCollectionContinueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionContinueBindAsync(predicate,
                                                          okFunc,
                                                          values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute task result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionWhereBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapBindAsync(awaitedThis => awaitedThis.ResultCollectionWhereAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionWhereBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionWhereBindAsync(predicate,
                                                       okFunc,
                                                       values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute task result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IRList<TValueOut>> ResultCollectionOkBadBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapBindAsync(awaitedThis => awaitedThis.ResultCollectionOkBadAsync(okFunc, badFunc));

        /// <summary>
        /// Execute task result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionOkBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapBindAsync(awaitedThis => awaitedThis.ResultCollectionOkAsync(okFunc));

        /// <summary>
        /// Execute task result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IRList<TValue>> ResultCollectionBadBindAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValue>>> badFunc)
            where TValue : notnull =>
            await @this.
                MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBadAsync(badFunc));

        /// <summary>
        /// Check errors by predicate async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionCheckErrorsOkBindAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValue : notnull =>
            await @this.
                MapBindAsync(awaitedThis => awaitedThis.ResultCollectionCheckErrorsOkAsync(predicate, badFunc));

        /// <summary>
        /// Check errors by predicate async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionCheckErrorsOkBindAsync<TValue>(this Task<IRList<TValue>> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<IRError>> badFunc)
            where TValue : notnull =>
            await @this.ResultCollectionCheckErrorsOkBindAsync(predicate,
                                                               values => badFunc(values).GetCollectionTaskAsync());
    }
}