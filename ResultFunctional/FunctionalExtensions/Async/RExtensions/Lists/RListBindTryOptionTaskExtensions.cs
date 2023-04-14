using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists.RListBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection monad task function with conditions and exception handling
    /// </summary>
    public static class RListBindTryOptionTaskExtensions
    {
        /// <summary>
        /// Execute result collection task function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Monad result collection function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindTrySomeAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IEnumerable<TValueIn>, IRList<TValueOut>> func,
                                                                                                            Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            RListBindSomeTask(value => RListBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute result collection task function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Monad result collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindTrySomeAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                       Func<IEnumerable<TValueIn>, IRList<TValueOut>> func,
                                                                                                       IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            RListBindSomeTask(value => RListBindTry(() => func.Invoke(value), error));
    }
}