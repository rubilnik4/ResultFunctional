using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists.RListTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
{
    /// <summary>
    /// Exception handling result collection with conditions extension methods
    /// </summary>
    public static class RListTryOptionExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> RListTrySome<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                          Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> func,
                                                                          Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.RListBindSome(value => RListTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> RListTrySome<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                          Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> func,
                                                                          IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.RListBindSome(value => RListTry(() => func.Invoke(value), error));
    }
}