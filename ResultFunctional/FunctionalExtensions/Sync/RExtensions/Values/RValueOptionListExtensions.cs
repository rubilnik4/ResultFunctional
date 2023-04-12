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
    public static class RValueOptionListExtensions
    {
        /// <summary>
        /// Execute result value function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns> 
        public static IRList<TValueOut> RValueListOption<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                             Func<TValueIn, bool> predicate,
                                                                                             Func<TValueIn, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                             Func<TValueIn, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.
            RValueOption(predicate, someFunc, noneFunc).
            ToRList();

        /// <summary>
        /// Execute result value function converting to result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static IRList<TValueOut> RValueListMatch<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                    Func<TValueIn, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.
            RValueMatch(someFunc, noneFunc).
            ToRList();

        /// <summary>
        /// Execute result value function converting to result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRList<TValueOut> RValueListSome<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                             Func<TValueIn, IReadOnlyCollection<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.
            RValueSome(someFunc).
            ToRList();
    }
}