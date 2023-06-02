using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe
{
    /// <summary>
    /// Result error collection extension methods
    /// </summary>
    public static class RMaybeFoldTaskExtensions
    {
        /// <summary>
        /// Aggregate collection of result errors
        /// </summary>
        /// <param name="this">Incoming collection of result errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeFoldTask(this Task<IEnumerable<IRMaybe>> @this) =>
             await @this
               .MapTask(thisAwaited => thisAwaited.RMaybeFold());

        /// <summary>
        /// Aggregate collection of result errors
        /// </summary>
        /// <param name="this">Incoming collection of result errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeFoldTask(this Task<IReadOnlyCollection<IRMaybe>> @this)=>
            await @this
               .MapTask(thisAwaited => thisAwaited.RMaybeFold());
    }
}