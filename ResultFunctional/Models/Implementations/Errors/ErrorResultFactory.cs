using System;
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
    /// Error factory
    /// </summary>
    public static class ErrorResultFactory
    {
        /// <summary>
        /// Create simple error
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns>Simple error</returns>
        public static SimpleErrorResult SimpleErrorType(string description) =>
            new SimpleErrorResult(description);

        /// <summary>
        /// Create common error type
        /// </summary>
        /// <param name="commonErrorType">Common error type</param>
        /// <param name="description">Description</param>
        /// <returns>Common error type</returns>
        public static CommonErrorResult CommonError(CommonErrorType commonErrorType, string description) =>
            new CommonErrorResult(commonErrorType, description);

        /// <summary>
        /// Create authorize error
        /// </summary>
        /// <param name="authorizeErrorType">Authorize error type</param>
        /// <param name="description">Description</param>
        /// <returns>Authorize error</returns>
        public static AuthorizeErrorResult AuthorizeError(AuthorizeErrorType authorizeErrorType, string description) =>
            new AuthorizeErrorResult(authorizeErrorType, description);

        /// <summary>
        /// Create database save transaction error
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns>Database save transaction error</returns>
        public static DatabaseSaveErrorResult DatabaseSaveError(string description) =>
            new DatabaseSaveErrorResult(description);

        /// <summary>
        /// Create database connection error
        /// </summary>
        /// <param name="parameter">Database parameter name</param>
        /// <param name="description">Description</param>
        /// <returns>Database connection error</returns>
        public static DatabaseConnectionErrorResult DatabaseConnectionError(string parameter, string description) =>
            new DatabaseConnectionErrorResult(parameter, description);

        /// <summary>
        /// Create table access database error
        /// </summary>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <returns>Table access database error</returns>
        public static DatabaseAccessErrorResult DatabaseAccessError(string tableName, string description) =>
            new DatabaseAccessErrorResult(tableName, description);

        /// <summary>
        /// Create database not found value error
        /// </summary>
        /// <typeparam name="TValue">Database value</typeparam>
        /// <param name="value">>Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <returns>Database not found value error</returns>
        public static IDatabaseValueNotFoundErrorResult DatabaseValueNotFoundError<TValue>(TValue value, string tableName, string description)
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            where TValue : notnull
#endif
            =>
            new DatabaseValueNotFoundErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Create database not valid value error
        /// </summary>
        /// <typeparam name="TValue">Database value</typeparam>
        /// <param name="value">>Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <returns>Database not valid value error</returns>
        public static IDatabaseValueNotValidErrorResult DatabaseValueNotValidError<TValue>(TValue value, string tableName, string description)
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            where TValue : notnull
#endif
            =>
            new DatabaseValueNotValidErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Create database duplicate value error
        /// </summary>
        /// <typeparam name="TValue">Database value</typeparam>
        /// <param name="value">>Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <returns>Database duplicate value error</returns>
        public static IDatabaseValueDuplicatedErrorResult DatabaseValueDuplicateError<TValue>(TValue value, string tableName, string description)
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            where TValue : notnull
#endif
            =>
            new DatabaseValueDuplicatedErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Create serialize error
        /// </summary>
        /// <typeparam name="TValue">Serialize type</typeparam>
        /// <param name="value">Serialize instance</param>
        /// <param name="description">Description</param>
        /// <returns>Serialize error</returns>
        public static ISerializeErrorResult SerializeError<TValue>(TValue value, string description)
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            where TValue : notnull
#endif
            =>
            new SerializeErrorResult<TValue>(value, description);

        /// <summary>
        /// Create deserialize error
        /// </summary>
        /// <typeparam name="TValue">Deserialize type</typeparam>
        /// <param name="value">Deserialize instance</param>
        /// <param name="description">Description</param>
        /// <returns>Deserialize error</returns>
        public static IDeserializeErrorResult DeserializeError<TValue>(string value, string description)
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            where TValue : notnull
#endif
            =>
            new DeserializeErrorResult<TValue>(value, description);

        /// <summary>
        /// Create json scheme error
        /// </summary>
        /// <param name="jsonScheme">Json scheme</param>
        /// <param name="description">Description</param>
        /// <returns>Json scheme error</returns>
        public static JsonSchemeErrorResult JsonSchemeError(string jsonScheme, string description) =>
            new JsonSchemeErrorResult(jsonScheme, description);

        /// <summary>
        /// Create not found error
        /// </summary>
        /// <typeparam name="TValue">Not found instance</typeparam>
        /// <param name="description">Description</param>
        /// <returns>Not found error</returns>
        public static IValueNotFoundErrorResult ValueNotFoundError<TValue>(string description)
            where TValue : class =>
            new ValueNotFoundErrorResult<TValue>(description);

        /// <summary>
        /// Create not valid error
        /// </summary>
        /// <typeparam name="TValue">Not valid instance</typeparam>
        /// <param name="value">>Not valid instance</param>
        /// <param name="description">Description</param>
        /// <returns></returns>
        public static IValueNotValidErrorResult ValueNotValidError<TValue>(TValue value, string description)
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            where TValue : notnull
#endif
            =>
            new ValueNotValidErrorResult<TValue>(value, nameof(TValue));

        /// <summary>
        /// Create duplicate error
        /// </summary>
        /// <typeparam name="TValue">Not valid instance</typeparam>
        /// <param name="value">>Not valid instance</param>
        /// <param name="description">Description</param>
        /// <returns>Duplicate error</returns>
        public static IValueDuplicateErrorResult ValueDuplicateError<TValue>(TValue value, string description)
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            where TValue : notnull
#endif
            =>
            new ValueDuplicateErrorResult<TValue>(value, nameof(TValue));

        /// <summary>
        /// Create host connection error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="host">Host</param>
        /// <param name="description">Description</param>
        /// <returns>Host connection error</returns>
        public static RestHostErrorResult RestHostError(RestErrorType restErrorType, string host, string description) =>
            new RestHostErrorResult(restErrorType, host, description);

        /// <summary>
        /// Create timeout rest error
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="timeout">Timeout</param>
        /// <param name="description">Description</param>
        /// <returns>Timeout rest error</returns>
        public static RestTimeoutErrorResult RestTimeoutError(string host, TimeSpan timeout, string description) =>
            new RestTimeoutErrorResult(host, timeout, description);

        /// <summary>
        /// Create rest error with response message
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        /// <returns> Rest error with response message</returns>
        public static RestMessageErrorResult RestError(RestErrorType restErrorType, string reasonPhrase, string description) =>
            new RestMessageErrorResult(restErrorType, reasonPhrase, description);
    }
}