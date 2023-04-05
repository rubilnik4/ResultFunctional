using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Not found error
    /// </summary>
    /// <typeparam name="TValue">Not found instance</typeparam>
    public class RValueNotFoundError<TValue> : RBaseError<CommonErrorType, IRValueNotFoundError>, IRValueNotFoundError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize not found error
        /// </summary>
        /// <param name="description">Description</param>
        public RValueNotFoundError(string description)
           : this(description, null)
        { }

        /// <summary>
        /// Initialize not found error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RValueNotFoundError(string description, Exception? exception)
            : base(CommonErrorType.ValueNotFound, description, exception)
        { }

        /// <summary>
        /// Initialize not found error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate error</returns>
        protected override IRValueNotFoundError InitializeType(string description, Exception? exception) =>
            new RValueNotFoundError<TValue>(description, exception);
    }
}