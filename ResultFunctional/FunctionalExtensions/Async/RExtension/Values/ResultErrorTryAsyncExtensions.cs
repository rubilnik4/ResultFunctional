﻿using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values
{
    /// <summary>
    /// Exception handling result error async extension methods
    /// </summary>
    public static class ResultErrorTryAsyncExtensions
    {
        /// <summary>
        /// Execute async action and handle exception with result error converting
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ResultErrorTryAsync(Func<Task> action, Func<Exception, IRError> exceptionFunc)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRUnit();
            }

            return RUnit.Some();
        }

        /// <summary>
        /// Execute async action and handle exception with result error converting
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ResultErrorTryAsync(Func<Task> action, IRError error) =>
            await ResultErrorTryAsync(action, error.AppendException);
    }
}