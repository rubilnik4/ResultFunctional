using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Result with value collection
    /// </summary>
    /// <typeparam name="TValue">Value parameter</typeparam>
    public interface IResultCollection<out TValue> : IResultValue<IReadOnlyCollection<TValue>>
    {
        /// <summary>
        /// Add error to result
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result collection with error</returns> 
        new IResultCollection<TValue> AppendError(IRError error);

        /// <summary>
        /// Add errors to result
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result collection with error</returns>       
        new IResultCollection<TValue> ConcatErrors(IEnumerable<IRError> errors);

        /// <summary>
        /// Add values and errors to current result
        /// </summary>
        /// <param name="result">Result error</param>
        /// <returns>Result collection</returns>
        new IResultCollection<TValue> ConcatResult(IResultError result);

        /// <summary>
        /// Convert to result value with collection parameter
        /// </summary>
        /// <returns>Result value with collection parameter</returns>
        IResultValue<IReadOnlyCollection<TValue>> ToResultValue();
    }
}