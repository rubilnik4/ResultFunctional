using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Преобразование в результирующий ответ
    /// </summary>  
    public static class ToResultErrorExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>  
        public static IResultError ToResultError(this IEnumerable<IResultError> @this) =>
            new ResultError(@this.SelectMany(result => result.Errors));
    }
}