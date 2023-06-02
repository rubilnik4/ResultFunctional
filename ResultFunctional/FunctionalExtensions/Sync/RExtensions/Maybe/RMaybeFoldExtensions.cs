using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe
{
    /// <summary>
    /// Result error collection extension methods
    /// </summary>
    public static class RMaybeFoldExtensions
    {
        /// <summary>
        /// Aggregate collection of result errors
        /// </summary>
        /// <param name="this">Incoming collection of result errors</param>
        /// <returns>Outgoing result error</returns>
        public static IRMaybe RMaybeFold(this IEnumerable<IRMaybe> @this) =>
             RMaybeFold(@this.ToList());

        /// <summary>
        /// Aggregate collection of result errors
        /// </summary>
        /// <param name="this">Incoming collection of result errors</param>
        /// <returns>Outgoing result error</returns>
        public static IRMaybe RMaybeFold(this IReadOnlyCollection<IRMaybe> @this)=>
             @this.All(collection => collection.Success)
                ? RUnit.Some()
                : @this.SelectMany(collection => collection.GetErrorsOrEmpty()).ToRUnit();
    }
}