using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection functor function with conditions
    /// </summary>
    public static class ResultCollectionWhereExtensions
    {
        /// <summary>
        /// Execute result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionContinue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<IRError>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultCollection<TValueOut>(okFunc.Invoke(@this.Value))
                 : new ResultCollection<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result collection function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionWhere<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultCollection<TValueOut>(okFunc.Invoke(@this.Value))
                 : new ResultCollection<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result collection function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionOkBad<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                              Func<IReadOnlyCollection<IRError>, IEnumerable<TValueOut>> badFunc) =>
            @this.OkStatus
                ? new ResultCollection<TValueOut>(okFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Execute result collection function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                           Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc) =>
            @this.OkStatus
                ? new ResultCollection<TValueOut>(okFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result collection function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValue> ResultCollectionBad<TValue>(this IResultCollection<TValue> @this,
                                                                            Func<IReadOnlyCollection<IRError>, IEnumerable<TValue>> badFunc) =>

            @this.OkStatus
                ? @this
                : new ResultCollection<TValue>(badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Check errors by predicate to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static IResultCollection<TValue> ResultCollectionCheckErrorsOk<TValue>(this IResultCollection<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, IEnumerable<IRError>> badFunc) =>
            @this.
            ResultCollectionContinue(predicate,
                                     value => value,
                                     badFunc);
    }
}