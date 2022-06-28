using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Result error extension methods
    /// </summary>  
    public static class ToResultErrorExtensions
    {
        /// <summary>
        /// Merge result errors collection
        /// </summary>
        /// <param name="this">Result error collection</param>
        /// <returns>Result error</returns>
        public static IResultError ToResultError(this IEnumerable<IResultError> @this) =>
            new ResultError(@this.SelectMany(result => result.Errors));

        /// <summary>
        /// Merge errors collection
        /// </summary>
        /// <param name="this">Error collection</param>
        /// <returns>Result error</returns>
        public static IResultError ToResultError(this IEnumerable<IErrorResult> @this) =>
            new ResultError(@this);
    }
}