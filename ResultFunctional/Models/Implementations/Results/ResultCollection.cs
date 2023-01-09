using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Result with value collection
    /// </summary>
    /// <typeparam name="TValue">Value parameter</typeparam>
    public class ResultCollection<TValue> : ResultValue<IReadOnlyCollection<TValue>>, IResultCollection<TValue>
    {
        /// <summary>
        /// Initializing by error
        /// </summary>
        /// <param name="error">Error</param>
        public ResultCollection(IRError error)
            : this(error.AsEnumerable()) { }

        /// <summary>
        /// Initializing by errors
        /// </summary>
        /// <param name="errors">Errors</param>
        public ResultCollection(IEnumerable<IRError> errors)
            : this(Enumerable.Empty<TValue>(), errors)
        { }

        /// <summary>
        /// Initializing by values
        /// </summary>
        /// <param name="values">Value collection</param>
        public ResultCollection(IEnumerable<TValue> values)
            : this(values, Enumerable.Empty<IRError>())
        { }

        /// <summary>
        /// Initializing by values and errors
        /// </summary>
        /// <param name="values">Value collection</param>
        /// <param name="errors">Errors</param>
        protected ResultCollection(IEnumerable<TValue> values, IEnumerable<IRError> errors)
            : base(values.ToList().AsReadOnly(), errors)
        { }

        /// <summary>
        /// Add error to result
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result collection with error</returns>
        public new IResultCollection<TValue> AppendError(IRError error) =>
            base.AppendError(error).
            ToResultCollection(Value);

        /// <summary>
        /// Add errors to result
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result collection with error</returns>  
        public new IResultCollection<TValue> ConcatErrors(IEnumerable<IRError> errors) =>
            base.ConcatErrors(errors).
            ToResultCollection(Value);

        /// <summary>
        /// Add values and errors to current result
        /// </summary>
        /// <param name="result">Result error</param>
        /// <returns>Result collection</returns>
        public new IResultCollection<TValue> ConcatResult(IResultError result) =>
            ConcatErrors(result.Errors);

        /// <summary>
        /// Convert to result value with collection parameter
        /// </summary>
        /// <returns>Result value with collection parameter</returns>
        public IResultValue<IReadOnlyCollection<TValue>> ToResultValue() =>
            this;
    }
}