using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection functions converting to result value
    /// </summary>
    public static class ResultValueWhereToCollectionExtensions
    {
        /// <summary>
        /// Execute result collection function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultCollectionContinueToValue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                        Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                        Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc,
                                                                                        Func<IReadOnlyCollection<TValueIn>, IEnumerable<IRError>> badFunc) =>
             @this.ToResultValue().
             ResultValueContinue(predicate, okFunc, badFunc);

        /// <summary>
        /// Execute result collection function converting to result value depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultCollectionOkBadToValue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                    Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, TValueOut> badFunc) =>
            @this.ToResultValue().
            ResultValueOkBad(okFunc, badFunc);

        /// <summary>
        /// Execute result collection function converting to result value if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultValue<TValueOut> ResultCollectionOkToValue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                             Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc) =>
            @this.ToResultValue().
            ResultValueOk(okFunc);
    }
}