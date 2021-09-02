using System;
using System.Globalization;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.Base
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public abstract class ErrorBaseResult<TError> : ErrorResult, IErrorBaseResult<TError>
        where TError : struct
    {
        protected ErrorBaseResult(TError errorType, string description)
            : this(errorType, description, null) 
        { }

        protected ErrorBaseResult(TError errorType, string description, Exception? exception)
            : base(description, exception)
        {
            ErrorType = errorType;
        }

        /// <summary>
        /// Идентификатор ошибки
        /// </summary>
        public override string Id =>
            ErrorType.ToString();

        /// <summary>
        /// Тип ошибки при конвертации файлов
        /// </summary>
        public TError ErrorType { get; }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        public override bool HasErrorType<TErrorType>()
            where TErrorType : struct =>
            typeof(TError) == typeof(TErrorType);

        #region IFormattable Support
        public override string ToString() =>
            ToString(String.Empty, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ErrorType.ToString();
        #endregion
    }
}