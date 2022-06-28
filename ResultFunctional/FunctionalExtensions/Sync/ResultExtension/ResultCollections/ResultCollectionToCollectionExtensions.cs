using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Result collection reorder extension methods
    /// </summary>
    public static class ResultCollectionToCollectionExtensions
    {
        /// <summary>
        /// Converting result collection to ordinal collection
        /// </summary>      
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing collection</returns>
        public static IReadOnlyCollection<TValueOut> ResultCollectionToCollectionOkBad<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                    Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value).ToList()
                : badFunc.Invoke(@this.Errors).ToList();
    }
}