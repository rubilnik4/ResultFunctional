using System;
using System.Globalization;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.Base
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public abstract class ErrorBaseResult<TError, TErrorResult> : ErrorResult, IErrorBaseResult<TError>
        where TErrorResult : IErrorResult
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
            ErrorType.ToString() ?? String.Empty;

        /// <summary>
        /// Тип ошибки при конвертации файлов
        /// </summary>
        public TError ErrorType { get; }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        public bool HasErrorType<TErrorType>()
            where TErrorType : struct =>
            typeof(TError) == typeof(TErrorType);

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            InitializeType(description, exception);

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected abstract TErrorResult InitializeType(string description, Exception? exception);

        /// <summary>
        /// Добавить или заменить исключение
        /// </summary>
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