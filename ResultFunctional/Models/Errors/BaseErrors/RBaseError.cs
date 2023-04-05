using System;
using System.Globalization;

namespace ResultFunctional.Models.Errors.BaseErrors
{
    /// <summary>
    /// Base error with type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// /// <typeparam name="TError">Error result type</typeparam>
    public abstract class RBaseError<TErrorType, TError> : RError, IRBaseError<TErrorType>
        where TErrorType : struct
        where TError : IRError
    {
        /// <summary>
        /// Initialize error with type
        /// </summary>
        /// <param name="errorType">Error type</param>
        /// <param name="description">Description</param>
        protected RBaseError(TErrorType errorType, string description)
            : this(errorType, description, null)
        { }

        /// <summary>
        /// Initialize error with type
        /// </summary>
        /// <param name="errorType">Error type</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RBaseError(TErrorType errorType, string description, Exception? exception)
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
        /// <typeparam name="TRErrorType">Error type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public override bool IsErrorType<TRErrorType>()
            where TRErrorType : struct =>
            typeof(TErrorType) == typeof(TRErrorType);

        /// <summary>
        /// Is error type value equal to current error type
        /// </summary>
        /// <typeparam name="TRErrorType">Error type</typeparam>
        /// <param name="errorType">Error type value</param>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public override bool IsErrorType<TRErrorType>(TRErrorType errorType)
            where TRErrorType : struct =>
            IsErrorType<TRErrorType>() && ErrorType.GetHashCode() == errorType.GetHashCode();

        /// <summary>
        /// Initialize base error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Base error initialized by derived</returns>
        protected override IRError Initialize(string description, Exception? exception) =>
            InitializeType(description, exception);

        /// <summary>
        /// Initialize derived error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Error initialized by derived</returns>
        protected abstract TError InitializeType(string description, Exception? exception);

        /// <summary>
        /// Create error with exception and base parameters.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Derived error initialized with exception</returns>
        public new TError AppendException(Exception exception) =>
            InitializeType(Description, exception);

        #region IFormattable Support
        public override string ToString() =>
            ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ErrorType.ToString() ?? String.Empty;
        #endregion
    }
}