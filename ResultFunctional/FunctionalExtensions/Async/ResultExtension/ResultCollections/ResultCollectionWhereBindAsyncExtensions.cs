using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionContinueBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
            await @this.ResultCollectionContinueBindAsync(predicate,
                                                      values => okFunc(values).GetEnumerableTaskAsync(),
                                                      values => badFunc(values).GetEnumerableTaskAsync());

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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionContinueBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<IErrorResult>> badFunc) =>
            await @this.ResultCollectionContinueBindAsync(predicate,
                                                      okFunc,
                                                      values => badFunc(values).GetEnumerableTaskAsync());

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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionContinueBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionContinueAsync(predicate, okFunc, badFunc));

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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionWhereBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> badFunc) =>
            await @this.ResultCollectionWhereBindAsync(predicate,
                                                      values => okFunc(values).GetEnumerableTaskAsync(),
                                                      values => badFunc(values).GetEnumerableTaskAsync());

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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionWhereBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> badFunc) =>
            await @this.ResultCollectionWhereBindAsync(predicate,
                                                      okFunc,
                                                      values => badFunc(values).GetEnumerableTaskAsync());

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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionWhereBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionWhereAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Execute task result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkBadBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, Task<IReadOnlyCollection<TValueOut>>> badFunc) =>
            await @this.ResultCollectionOkBadBindAsync(values => okFunc(values).GetEnumerableTaskAsync(),
                                                       values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute task result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkBadBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValueOut>>> badFunc) =>
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
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc) =>
             await @this.ResultCollectionOkBindAsync(values => okFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute task result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionOkAsync(okFunc));

        /// <summary>
        /// Execute task result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IResultCollection<TValue>> ResultCollectionBadBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<IReadOnlyCollection<TValue>>> badFunc) =>
              await @this.ResultCollectionBadBindAsync(values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute task result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IResultCollection<TValue>> ResultCollectionBadBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValue>>> badFunc) =>
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
        public static async Task<IResultCollection<TValue>> ResultCollectionCheckErrorsOkBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
              await @this.ResultCollectionCheckErrorsOkBindAsync(predicate, 
                                                                 values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Check errors by predicate async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionCheckErrorsOkBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, IEnumerable<IErrorResult>> badFunc) =>
            await @this.ResultCollectionCheckErrorsOkBindAsync(predicate,
                                                               values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Check errors by predicate async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionCheckErrorsOkBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, Task<IEnumerable<IErrorResult>>> badFunc) =>
             await @this.
             MapBindAsync(awaitedThis => awaitedThis.ResultCollectionCheckErrorsOkAsync(predicate, badFunc));
    }
}