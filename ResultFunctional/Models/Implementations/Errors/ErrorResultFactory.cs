using System;
using System.Net.Http;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Errors.CommonErrors;
using ResultFunctional.Models.Implementations.Errors.ConversionErrors;
using ResultFunctional.Models.Implementations.Errors.DatabaseErrors;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.conversionErrors;
using ResultFunctional.Models.Interfaces.Errors.ConversionErrors;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors
{
    /// <summary>
    /// Фабрика создания типов ошибок
    /// </summary>
    public static class ErrorResultFactory
    {
        /// <summary>
        /// Создать ошибку с типом данных
        /// </summary>
        public static IErrorResult ErrorType<TError>(TError error, string description)
            where TError : struct =>
            new ErrorTypeResult<TError>(error, description);

        /// <summary>
        /// Создать ошибку общего типа
        /// </summary>
        public static CommonErrorResult CommonError(CommonErrorType commonErrorType, string description) =>
            new CommonErrorResult(commonErrorType, description);

        /// <summary>
        /// Создать ошибку авторизации
        /// </summary>
        public static AuthorizeErrorResult AuthorizeError(AuthorizeErrorType authorizeErrorType, string description) =>
            new AuthorizeErrorResult(authorizeErrorType, description);

        /// <summary>
        /// Создать ошибку подключения к базе данных
        /// </summary>
        public static DatabaseConnectionErrorResult DatabaseConnectionError(string parameter, string description) =>
            new DatabaseConnectionErrorResult(parameter, description);

        /// <summary>
        /// Создать ошибку базы данных
        /// </summary>
        public static DatabaseErrorResult DatabaseError(DatabaseErrorType databaseErrorType, string description) =>
            new DatabaseErrorResult(databaseErrorType, description);

        /// <summary>
        /// Создать ошибку таблицы базы данных
        /// </summary>
        public static DatabaseTableErrorResult DatabaseTableError(string tableName, string description) =>
            new DatabaseTableErrorResult(tableName, description);

        /// <summary>
        /// Создать ошибку отсутствующего значения в базе данных
        /// </summary>
        public static IDatabaseValueNotFoundErrorResult DatabaseValueNotFoundError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new DatabaseValueNotFoundErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Создать ошибку некорректного значения в базе данных
        /// </summary>
        public static IDatabaseValueNotValidErrorResult DatabaseValueNotValidError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new DatabaseValueNotValidErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Создать ошибку дублирующего значения в базе данных
        /// </summary>
        public static IDatabaseValueDuplicatedErrorResult DatabaseValueDuplicateError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new DatabaseValueDuplicatedErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Создать ошибку сериализации
        /// </summary>
        public static ISerializeErrorResult SerializeError<TValue>(ConversionErrorType conversionErrorType, TValue value, string description)
            where TValue : notnull =>
            new SerializeErrorResult<TValue>(conversionErrorType, value, description);

        /// <summary>
        /// Создать ошибку сериализации
        /// </summary>
        public static IDeserializeErrorResult DeserializeError<TValue>(ConversionErrorType conversionErrorType, string value, string description)
            where TValue : notnull =>
            new DeserializeErrorResult<TValue>(conversionErrorType, value, description);

        /// <summary>
        /// Создать ошибку отсутствующего значения
        /// </summary>
        public static IValueNotFoundErrorResult ValueNotFoundError<TValue, TType>(TValue? value, TType parentType)
            where TValue : class
            where TType : Type =>
            new ValueNotFoundErrorResult<TValue, TType>(nameof(TValue));

        /// <summary>
        /// Создать ошибку неверного значения
        /// </summary>
        public static IValueNotValidErrorResult ValueNotValidError<TValue, TType>(TValue value, TType parentType, string description)
            where TValue : notnull
            where TType : Type =>
            new ValueNotValidErrorResult<TValue, TType>(value, nameof(TValue));

        /// <summary>
        /// Создать ошибку REST сервера
        /// </summary>
        public static RestErrorResult RestError(RestErrorType restErrorType, string host, string description) =>
            new RestErrorResult(restErrorType, host, description);

        /// <summary>
        /// Создать ошибку REST сервера с сообщением
        /// </summary>
        public static RestMessageErrorResult RestError(RestErrorType restErrorType, HttpResponseMessage httpResponseMessage,
                                                       string description) =>
            new RestMessageErrorResult(restErrorType, httpResponseMessage, description);
    }
}