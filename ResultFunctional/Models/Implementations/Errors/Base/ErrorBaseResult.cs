using System;
using System.Globalization;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.Base
{
    /// <summary>
    /// Base error with type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// /// <typeparam name="TErrorResult">Error result type</typeparam>
    public abstract class ErrorBaseResult<TErrorType, TErrorResult> : ErrorResult, IErrorBaseResult<TErrorType>
        where TErrorType : struct
        where TErrorResult : IErrorResult
    {
        /// <summary>
        /// Initialize error with type
        /// </summary>
        /// <param name="errorType">Error type</param>
        /// <param name="description">Description</param>
        protected ErrorBaseResult(TErrorType errorType, string description)
            : this(errorType, description, null)
        { }

        /// <summary>
        /// Initialize error with type
        /// </summary>
        /// <param name="errorType">Error type</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected ErrorBaseResult(TErrorType errorType, string description, Exception? exception)
            : base(description, exception)
        {
            ErrorType = errorType;
        }

        /// <summary>
        /// ID as error type
        /// </summary>
        public override string Id =>
            ErrorType.ToString() ?? String.Empty;

        /// <summary>
        /// Error type
        /// </summary>
        public TErrorType ErrorType { get; }

        /// <summary>
        /// Is error type equal to current error type
        /// </summary>
        /// <typeparam name="TErrorTypeCompare">Error type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public override bool IsErrorType<TErrorTypeCompare>()
            where TErrorTypeCompare : struct =>
            typeof(TErrorType) == typeof(TErrorTypeCompare);

        /// <summary>
        /// Is error type value equal to current error type
        /// </summary>
        /// <typeparam name="TErrorTypeCompare">Error type</typeparam>
        /// <param name="errorType">Error type value</param>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public override bool IsErrorType<TErrorTypeCompare>(TErrorTypeCompare errorType)
            where TErrorTypeCompare : struct =>
            IsErrorType<TErrorTypeCompare>() && ErrorType.GetHashCode() == errorType.GetHashCode();

        /// <summary>
        /// Initialize base error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Base error initialized by derived</returns>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            InitializeType(description, exception);

        /// <summary>
        /// Initialize derived error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Error initialized by derived</returns>
        protected abstract TErrorResult InitializeType(string description, Exception? exception);

        /// <summary>
        /// Create error with exception and base parameters.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Derived error initialized with exception</returns>
        public new TErrorResult AppendException(Exception exception) =>
            InitializeType(Description, exception);

        #region IFormattable Support
        public override string ToString() =>
            ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ErrorType.ToString() ?? String.Empty;
        #endregion
    }
}