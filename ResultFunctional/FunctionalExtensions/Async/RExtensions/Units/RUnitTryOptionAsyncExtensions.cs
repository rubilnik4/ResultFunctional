﻿using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Units.RUnitTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
{
    /// <summary>
    /// Async exception handling result error with conditions extension methods
    /// </summary>
    public static class RUnitTryOptionAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> RUnitTrySomeAsync(this IRUnit @this, Func<Task> action,
                                                               Func<Exception, IRError> exceptionFunc) =>
            await @this.RUnitBindSomeAsync(() => RUnitTryAsync(action.Invoke, exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> RUnitTrySomeAsync(this IRUnit @this, Func<Task> action, IRError error) =>
            await @this.RUnitTrySomeAsync(action, _ => error);
    }
}