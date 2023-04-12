using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values.RValueBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value monad function with conditions and exception handling
    /// </summary>
    public static class RValueBindTryOptionExtensions
    {
        /// <summary>
        /// Execute result value function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueBindTrySome<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                Func<TValueIn, IRValue<TValueOut>> func,
                                                                                Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.RValueBindSome(value => RValueBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute result value function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Monad result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueBindTrySome<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                Func<TValueIn, IRValue<TValueOut>> func,
                                                                                IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.RValueBindSome(value => RValueBindTry(() => func.Invoke(value), error));
    }
}