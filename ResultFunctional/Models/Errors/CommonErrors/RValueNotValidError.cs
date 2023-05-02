using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Not valid error
    /// </summary>
    /// <typeparam name="TValue">Not valid instance</typeparam>
    public class RValueNotValidError<TValue> : RValueError<TValue, IRValueNotValidError>, IRValueNotValidError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize not valid error
        /// </summary>
        /// <param name="value">Not valid instance</param>
        /// <param name="valueName">Value name</param>
        /// <param name="description">Description</param>
        public RValueNotValidError(TValue value, string valueName, string description)
           : this(value, valueName, description, null)
        { }

        /// <summary>
        /// Initialize not valid error
        /// </summary>
        /// <param name="value">Not valid instance</param>
        /// <param name="valueName">Value name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RValueNotValidError(TValue value, string valueName, string description, Exception? exception)
            : base(valueName, CommonErrorType.ValueNotValid, description, exception)
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
            new RValueNotValidError<TValue>(Value, ValueName, description, exception);
    }
}