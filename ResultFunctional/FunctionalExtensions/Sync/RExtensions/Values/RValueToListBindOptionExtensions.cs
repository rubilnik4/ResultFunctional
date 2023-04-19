using System;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value monad function converting to result collection
    /// </summary>
    public static class RValueToListBindOptionExtensions
    {
        /// <summary>
        /// Execute monad result value function converting to result collection if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> RValueToListBindSome<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                Func<TValueIn, IRList<TValueOut>> someFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.
            RValueBindSome(value => someFunc(value).ToRValue()).
            ToRList();
    }
}