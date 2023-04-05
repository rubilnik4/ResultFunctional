using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value async functions converting to result value
    /// </summary>
    public static class ResultValueWhereToCollectionAsyncExtensions
    {
        /// <summary>
        /// Execute result value async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IResultCollection<TValueOut>> ResultValueContinueToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                                        Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                                        Func<TValueIn, Task<IReadOnlyCollection<IRError>>> badFunc) =>
            await @this.ResultValueContinueToCollectionAsync(predicate,
                                                             values => okFunc(values).GetEnumerableTaskAsync(),
                                                             values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result value async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IResultCollection<TValueOut>> ResultValueContinueToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                                        Func<TValueIn, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                                        Func<TValueIn, IEnumerable<IRError>> badFunc) =>
             await @this.ResultValueContinueToCollectionAsync(predicate,
                                                             okFunc,
                                                             values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result value async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IResultCollection<TValueOut>> ResultValueContinueToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                                        Func<TValueIn, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                                        Func<TValueIn, Task<IEnumerable<IRError>>> badFunc) =>
            await @this.
            ResultValueContinueAsync(predicate, okFunc, badFunc).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Execute result value async function converting to result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IResultCollection<TValueOut>> ResultValueOkBadToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> badFunc) =>
             await @this.ResultValueOkBadToCollectionAsync(values => okFunc(values).GetEnumerableTaskAsync(),
                                                           values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result value async function converting to result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IResultCollection<TValueOut>> ResultValueOkBadToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, Task<IEnumerable<TValueOut>>> badFunc) =>
            await @this.
            ResultValueOkBadAsync(okFunc, badFunc).
            ToResultCollectionTaskAsync();

        /// <summary>
        /// Execute result value async function converting to result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultCollection<TValueOut>> ResultValueOkToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc) =>
             await @this.ResultValueOkToCollectionAsync(values => okFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result value async function converting to result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultCollection<TValueOut>> ResultValueOkToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<IEnumerable<TValueOut>>> okFunc) =>
            await @this.
            ResultValueOkAsync(okFunc).
            ToResultCollectionTaskAsync();
    }
}