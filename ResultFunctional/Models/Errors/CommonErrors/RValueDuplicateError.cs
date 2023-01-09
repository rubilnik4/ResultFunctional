using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Duplicate error
    /// </summary>
    /// <typeparam name="TValue">Duplicate instance</typeparam>
    public class RValueDuplicateError<TValue> : RBaseError<CommonErrorType, IRValueDuplicateError>, IRValueDuplicateError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize duplicate error
        /// </summary>
        /// <param name="value">Duplicate instance</param>
        /// <param name="description">Description</param>
        public RValueDuplicateError(TValue value, string description)
           : this(value, description, null)
        { }

        /// <summary>
        /// Initialize duplicate error
        /// </summary>
        /// <param name="value">Duplicate instance</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RValueDuplicateError(TValue value, string description, Exception? exception)
            : base(CommonErrorType.ValueDuplicated, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Duplicate instance
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Initialize duplicate error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate error</returns>
        protected override IRValueDuplicateError InitializeType(string description, Exception? exception) =>
            new RValueDuplicateError<TValue>(Value, description, exception);
    }
}