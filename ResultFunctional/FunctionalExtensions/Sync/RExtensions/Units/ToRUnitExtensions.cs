﻿using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units
{
    /// <summary>
    /// Result error extension methods
    /// </summary>  
    public static class ToRUnitExtensions
    {
        /// <summary>
        /// Merge result errors collection
        /// </summary>
        /// <param name="this">Result error collection</param>
        /// <returns>Result error</returns>
        public static IRUnit ToRUnit(this IEnumerable<IRUnit> @this) =>
            @this.SelectMany(result => result.GetErrors()).ToRUnit();

        /// <summary>
        /// Merge result errors collection
        /// </summary>
        /// <param name="this">Result error collection</param>
        /// <returns>Result error</returns>
        public static IRUnit ToRUnit(this IEnumerable<IROption> @this) =>
            @this.SelectMany(result => result.GetErrors()).ToRUnit();

        /// <summary>
        /// Merge errors collection
        /// </summary>
        /// <param name="this">Error collection</param>
        /// <returns>Result error</returns>
        public static IRUnit ToRUnit(this IEnumerable<IRError> @this) =>
            RUnitFactory.None(@this.ToList());
    }
}