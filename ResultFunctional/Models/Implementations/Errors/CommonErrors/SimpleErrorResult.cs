using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Error without subtype
    /// </summary>
    public class SimpleErrorResult : ErrorResult
    {
        public SimpleErrorResult(string description)
            : this(description, null)
        { }

        public SimpleErrorResult(string description, Exception? exception)
            : base(description, exception)
        { }

        /// <summary>
        /// ID as type name 
        /// </summary>
        public override string Id =>
            GetType().Name;

        /// <summary>
        /// Initialize simple error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Simple error</returns>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new SimpleErrorResult(description, exception);

        /// <summary>
        /// Is error type equal to current error type
        /// </summary>
        /// <typeparam name="TErrorTypeCompare">Error type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public override bool IsErrorType<TErrorTypeCompare>()
            where TErrorTypeCompare : struct =>
            false;

        /// <summary>
        /// Is error type value equal to current error type
        /// </summary>
        /// <typeparam name="TErrorTypeCompare">Error type</typeparam>
        /// <param name="errorType">Error type value</param>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public override bool IsErrorType<TErrorTypeCompare>(TErrorTypeCompare errorType)
            where TErrorTypeCompare : struct =>
            false;
    }
}