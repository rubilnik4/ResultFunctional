using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;
using System;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Value error
    /// </summary>
    /// <typeparam name="TValue">Value instance</typeparam>
    /// <typeparam name="TError">Error result type</typeparam>
    public abstract class RValueError<TValue, TError> : RBaseError<CommonErrorType, TError>, IRValueError
        where TValue : notnull
        where TError : IRError
    {
        /// <summary>
        /// Initialize value error
        /// </summary>
        /// <param name="valueName">Value name</param>
        /// <param name="commonErrorType">Common error type</param>
        /// <param name="description">Description</param>
        protected RValueError(string valueName, CommonErrorType commonErrorType, string description)
           : this(valueName, commonErrorType, description, null)
        { }

        /// <summary>
        /// Initialize value error
        /// </summary>
        /// <param name="valueName">Value name</param>
        /// <param name="commonErrorType">Common error type</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RValueError(string valueName, CommonErrorType commonErrorType, string description, Exception? exception)
            : base(commonErrorType, description, exception)
        {
            ValueName = valueName;
        }

        /// <summary>
        /// Value name
        /// </summary>
        public string ValueName { get; }

        /// <summary>
        /// Value type
        /// </summary>
        public Type ValueType =>
            typeof(TValue);
    }
}