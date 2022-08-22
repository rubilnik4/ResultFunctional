using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection async functions converting to result value
    /// </summary>
    public static class ResultCollectionWhereToValueAsyncExtensions
    {
        /// <summary>
        /// Execute result collection async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IResultValue<TValueOut>> ResultCollectionContinueToValueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
            await @this.ResultCollectionContinueToValueAsync(predicate, okFunc,
                                                             values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result collection async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IResultValue<TValueOut>> ResultCollectionContinueToValueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<IErrorResult>> badFunc) =>
            await @this.ResultCollectionContinueToValueAsync(predicate, okFunc,
                                                             values => badFunc(values).GetEnumerableTaskAsync());

        /// <summary>
        /// Execute result collection async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IResultValue<TValueOut>> ResultCollectionContinueToValueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.ToResultValue().
            ResultValueContinueAsync(predicate, okFunc, badFunc);

        /// <summary>
        /// Execute result collection async function converting to result value depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IResultValue<TValueOut>> ResultCollectionOkBadToValueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, Task<TValueOut>> badFunc) =>
            await @this.ToResultValue().
            ResultValueOkBadAsync(okFunc, badFunc);

        /// <summary>
        /// Execute result collection async function converting to result value if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultValue<TValueOut>> ResultCollectionOkToValueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                                  Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc) =>
            await @this.ToResultValue().
            ResultValueOkAsync(okFunc);
    }
}