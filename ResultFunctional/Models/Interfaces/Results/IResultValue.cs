using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Result with value
    /// </summary>
    /// <typeparam name="TValue">Value parameter</typeparam>
    public interface IResultValue<out TValue>: IResultError
    {
        /// <summary>
        /// Value
        /// </summary>
        TValue Value { get; }

        /// <summary>
        /// Add error to result
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result value with error</returns>
        new IResultValue<TValue> AppendError(IErrorResult error);

        /// <summary>
        /// Add errors to result
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result value with error</returns>  
        new IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors);

        /// <summary>
        /// Add values and errors to current result
        /// </summary>
        /// <param name="result">Result error</param>
        /// <returns>Result value</returns> 
        new IResultValue<TValue> ConcatResult(IResultError result);
    }
}