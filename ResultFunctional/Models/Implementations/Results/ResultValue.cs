using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Result with value
    /// </summary>
    /// <typeparam name="TValue">Value parameter</typeparam>
    public class ResultValue<TValue> : ResultError, IResultValue<TValue>
    {
        /// <summary>
        /// Initializing by error
        /// </summary>
        /// <param name="error">Error</param>
        public ResultValue(IErrorResult error)
            : this(error.AsEnumerable()) { }

        /// <summary>
        /// Initializing by errors
        /// </summary>
        /// <param name="errors">Errors</param>
        public ResultValue(IEnumerable<IErrorResult> errors)
            : this(default!, errors)
        { }

        /// <summary>
        /// Initializing by value
        /// </summary>
        /// <param name="value">Value</param>
        public ResultValue(TValue value)
            : this(value, Enumerable.Empty<IErrorResult>())
        { }

        /// <summary>
        /// Initializing by value and errors
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="errors">Errors</param>
        protected ResultValue(TValue value, IEnumerable<IErrorResult> errors)
            : base(errors)
        {
            if (value == null && !Errors.Any()) throw new ArgumentNullException(nameof(errors));
            Value = value;
        }

        /// <summary>
        /// Value
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Add error to result
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result value with error</returns>
        public new IResultValue<TValue> AppendError(IErrorResult error) =>
            base.AppendError(error).
            ToResultValue(Value);

        /// <summary>
        /// Add errors to result
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result value with error</returns>  
        public new IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors) =>
            base.ConcatErrors(errors).
            ToResultValue(Value);

        /// <summary>
        /// Add values and errors to current result
        /// </summary>
        /// <param name="result">Result error</param>
        /// <returns>Result value</returns> 
        public new IResultValue<TValue> ConcatResult(IResultError result) =>
            ConcatErrors(result.Errors);
    }
}