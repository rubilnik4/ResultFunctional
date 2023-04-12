using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values
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
        public static async Task<IRList<TValueOut>> ResultValueContinueToCollectionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                              Func<TValueIn, bool> predicate,
                                                                                                              Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                              Func<TValueIn, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueContinueAsync(predicate, okFunc, badFunc).
            ToRListTaskAsync();

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
        public static async Task<IRList<TValueOut>> ResultValueContinueToCollectionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                                        Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                                        Func<TValueIn, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
             await @this.ResultValueContinueToCollectionAsync(predicate,
                                                              okFunc,
                                                              values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute result value async function converting to result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IRList<TValueOut>> ResultValueOkBadToCollectionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
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
        public static async Task<IRList<TValueOut>> ResultValueOkBadToCollectionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, Task<IEnumerable<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueOkBadAsync(okFunc, badFunc).
            ToRListTaskAsync();

        /// <summary>
        /// Execute result value async function converting to result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRList<TValueOut>> ResultValueOkToCollectionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueOkAsync(okFunc).
            ToRListTaskAsync();
    }
}