using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection monad function with conditions
    /// </summary>
    public static class ResultCollectionBindWhereExtensions
    {
        /// <summary>
        /// Execute monad result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionBindContinue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<IErrorResult>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? okFunc.Invoke(@this.Value)
                 : new ResultCollection<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionBindWhere<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? okFunc.Invoke(@this.Value)
                 : badFunc.Invoke(@this.Value)
             : new ResultCollection<TValueOut>(@this.Errors);


        /// <summary>
        /// Execute monad result collection function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionBindOkBad<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                        Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc,
                                                                                        Func<IReadOnlyCollection<IErrorResult>, IResultCollection<TValueOut>> badFunc) =>
         @this.OkStatus
             ? okFunc.Invoke(@this.Value)
             : badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Execute monad result collection function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionBindOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                               Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value)
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result collection function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValue> ResultCollectionBindBad<TValue>(this IResultCollection<TValue> @this,
                                                                                Func<IReadOnlyCollection<IErrorResult>, IResultCollection<TValue>> badFunc) =>
            @this.OkStatus
                ? @this
                : badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Adding errors to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValue> ResultCollectionBindErrorsOk<TValue>(this IResultCollection<TValue> @this,
                                                                                     Func<IReadOnlyCollection<TValue>, IResultError> okFunc) =>
            @this.
            ResultCollectionBindOk(collection => okFunc.Invoke(collection).
                                                 Map(resultError => resultError.ToResultCollection(collection)));
    }
}