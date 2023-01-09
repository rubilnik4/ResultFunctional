using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Not valid error
    /// </summary>
    /// <typeparam name="TValue">Not valid instance</typeparam>
    public class RValueNotValidError<TValue> : RBaseError<CommonErrorType, IRValueNotValidError>, IRValueNotValidError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize not valid error
        /// </summary>
        /// <param name="value">Not valid instance</param>
        /// <param name="description">Description</param>
        public RValueNotValidError(TValue value, string description)
           : this(value, description, null)
        { }

        /// <summary>
        /// Initialize not valid error
        /// </summary>
        /// <param name="value">Not valid instance</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RValueNotValidError(TValue value, string description, Exception? exception)
            : base(CommonErrorType.ValueNotValid, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Not valid value
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Initialize not valid error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate error</returns>
        protected override IRValueNotValidError InitializeType(string description, Exception? exception) =>
            new RValueNotValidError<TValue>(Value, description, exception);
    }
}