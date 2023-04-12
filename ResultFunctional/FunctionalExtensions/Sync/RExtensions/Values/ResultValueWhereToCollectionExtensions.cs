using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
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
        public static IRList<TValueOut> ResultValueContinueToCollection<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                             Func<TValueIn, bool> predicate,
                                                                                             Func<TValueIn, IReadOnlyCollection<TValueOut>> okFunc,
                                                                                             Func<TValueIn, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.
            ResultValueContinue(predicate, okFunc, badFunc).
            ToRList();

        /// <summary>
        /// Execute result value function converting to result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static IRList<TValueOut> ResultValueOkBadToCollection<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                    Func<TValueIn, IReadOnlyCollection<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.
            ResultValueOkBad(okFunc, badFunc).
            ToRList();

        /// <summary>
        /// Execute result value function converting to result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRList<TValueOut> ResultValueOkToCollection<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                             Func<TValueIn, IReadOnlyCollection<TValueOut>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.
            ResultValueOk(okFunc).
            ToRList();
    }
}