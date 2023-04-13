using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection functions converting to result value
    /// </summary>
    public static class RListValueOptionExtensions
    {
        /// <summary>
        /// Execute result collection function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RListValueOption<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                               Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                               Func<IReadOnlyCollection<TValueIn>, TValueOut> someFunc,
                                                                               Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
             @this.ToRValue().
             RValueOption(predicate, someFunc, noneFunc);

        /// <summary>
        /// Execute result collection function converting to result value depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RListValueMatch<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                              Func<IReadOnlyCollection<TValueIn>, TValueOut> someFunc,
                                                                              Func<IReadOnlyCollection<IRError>, TValueOut> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.ToRValue().
            RValueMatch(someFunc, noneFunc);

        /// <summary>
        /// Execute result collection function converting to result value if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRValue<TValueOut> RListValueSome<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                             Func<IReadOnlyCollection<TValueIn>, TValueOut> someFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.ToRValue().
            RValueSome(someFunc);
    }
}