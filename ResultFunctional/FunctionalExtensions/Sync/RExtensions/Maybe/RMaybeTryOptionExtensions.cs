﻿using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe.RMaybeTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe
{
    /// <summary>
    /// Exception handling result error with conditions extension methods
    /// </summary>
    public static class RMaybeTryOptionExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static IRMaybe RMaybeTrySome(this IRMaybe @this, Action action,
                                           Func<Exception, IRError> exceptionFunc) =>
            @this.RMaybeBindSome(() => RMaybeTry(action.Invoke, exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static IRMaybe RMaybeTrySome(this IRMaybe @this, Action action, IRError error) =>
            @this.RMaybeTrySome(action, _ => error);
    }
}