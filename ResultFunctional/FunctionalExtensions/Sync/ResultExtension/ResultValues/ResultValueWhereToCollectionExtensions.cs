using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value functions converting to result value
    /// </summary>
    public static class ResultValueWhereToCollectionExtensions
    {
        /// <summary>
        /// Execute result value function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns> 
        public static IResultCollection<TValueOut> ResultValueContinueToCollection<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IEnumerable<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IEnumerable<IRError>> badFunc) =>
            @this.
            ResultValueContinue(predicate, okFunc, badFunc).
            ToResultCollection();

        /// <summary>
        /// Execute result value function converting to result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static IResultCollection<TValueOut> ResultValueOkBadToCollection<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, IEnumerable<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, IEnumerable<TValueOut>> badFunc) =>
            @this.
            ResultValueOkBad(okFunc, badFunc).
            ToResultCollection();

        /// <summary>
        /// Execute result value function converting to result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultCollection<TValueOut> ResultValueOkToCollection<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, IEnumerable<TValueOut>> okFunc) =>
            @this.
            ResultValueOk(okFunc).
            ToResultCollection();
    }
}