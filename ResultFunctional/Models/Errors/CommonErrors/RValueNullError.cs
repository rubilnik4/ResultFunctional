using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;


namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Null argument error
    /// </summary>
    /// <typeparam name="TValue">Null argument instance</typeparam>
    public class RValueNullError<TValue> : RValueError<TValue, IRValueNullError>, IRValueNullError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize null argument error
        /// </summary>
        /// <param name="valueName">Value name</param>
        /// <param name="description">Description</param>
        public RValueNullError(string valueName, string description)
           : this(valueName, description, null)
        { }

        /// <summary>
        /// Initialize not found error
        /// </summary>
        /// <param name="valueName">Value name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RValueNullError(string valueName, string description, Exception? exception)
            : base(valueName, CommonErrorType.NullArgument, description, exception)
        { }

        /// <summary>
        /// Initialize not found error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate error</returns>
        protected override IRValueNullError InitializeType(string description, Exception? exception) =>
            new RValueNullError<TValue>(ValueName, description, exception);
    }
}