using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.AuthorizeErrors;
using ResultFunctional.Models.Errors.CommonErrors;
using ResultFunctional.Models.Errors.ConversionErrors;
using ResultFunctional.Models.Errors.DatabaseErrors;
using ResultFunctional.Models.Errors.RestErrors;

namespace ResultFunctional.Models.Factories
{
    /// <summary>
    /// Error factory
    /// </summary>
    public static class RErrorFactory
    {
        /// <summary>
        /// Create simple error
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns>Simple error</returns>
        public static RSimpleError SimpleError(string description) =>
            new(description);

        /// <summary>
        /// Create error by custom structure type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <param name="errorType">Error subtype</param>
        /// <param name="description">Description</param>
        /// <returns>Common error type</returns>
        public static RTypeError<TErrorType> ErrorByType<TErrorType>(TErrorType errorType, string description)
            where TErrorType : struct =>
            new(errorType, description);

        /// <summary>
        /// Create common error type
        /// </summary>
        /// <param name="commonErrorType">Common error type</param>
        /// <param name="description">Description</param>
        /// <returns>Common error type</returns>
        public static RCommonError CommonError(CommonErrorType commonErrorType, string description) =>
            new(commonErrorType, description);

        /// <summary>
        /// Create authorize error
        /// </summary>
        /// <param name="authorizeErrorType">Authorize error type</param>
        /// <param name="description">Description</param>
        /// <returns>Authorize error</returns>
        public static RAuthorizeError AuthorizeError(AuthorizeErrorType authorizeErrorType, string description) =>
            new(authorizeErrorType, description);

        /// <summary>
        /// Create database save transaction error
        /// </summary>
        /// <param name="description">Description</param>
        /// <returns>Database save transaction error</returns>
        public static RDatabaseSaveError DatabaseSaveError(string description) =>
            new(description);

        /// <summary>
        /// Create database connection error
        /// </summary>
        /// <param name="parameter">Database parameter name</param>
        /// <param name="description">Description</param>
        /// <returns>Database connection error</returns>
        public static RDatabaseConnectionError DatabaseConnectionError(string parameter, string description) =>
            new(parameter, description);

        /// <summary>
        /// Create table access database error
        /// </summary>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <returns>Table access database error</returns>
        public static RDatabaseAccessError DatabaseAccessError(string tableName, string description) =>
            new(tableName, description);

        /// <summary>
        /// Create database not found value error
        /// </summary>
        /// <typeparam name="TValue">Database value</typeparam>
        /// <param name="value">>Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <returns>Database not found value error</returns>
        public static IRDatabaseValueNotFoundError DatabaseValueNotFoundError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new RDatabaseValueNotFoundError<TValue>(value, tableName, description);

        /// <summary>
        /// Create database not valid value error
        /// </summary>
        /// <typeparam name="TValue">Database value</typeparam>
        /// <param name="value">>Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <returns>Database not valid value error</returns>
        public static IRDatabaseValueNotValidError DatabaseValueNotValidError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new RDatabaseValueNotValidError<TValue>(value, tableName, description);

        /// <summary>
        /// Create database duplicate value error
        /// </summary>
        /// <typeparam name="TValue">Database value</typeparam>
        /// <param name="value">>Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <returns>Database duplicate value error</returns>
        public static IRDatabaseValueDuplicatedError DatabaseValueDuplicateError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new RDatabaseValueDuplicatedError<TValue>(value, tableName, description);

        /// <summary>
        /// Create serialize error
        /// </summary>
        /// <typeparam name="TValue">Serialize type</typeparam>
        /// <param name="value">Serialize instance</param>
        /// <param name="description">Description</param>
        /// <returns>Serialize error</returns>
        public static IRSerializeError SerializeError<TValue>(TValue value, string description)
            where TValue : notnull =>
            new RSerializeError<TValue>(value, description);

        /// <summary>
        /// Create deserialize error
        /// </summary>
        /// <typeparam name="TValue">Deserialize type</typeparam>
        /// <param name="value">Deserialize instance</param>
        /// <param name="description">Description</param>
        /// <returns>Deserialize error</returns>
        public static IRDeserializeError DeserializeError<TValue>(string value, string description)
            where TValue : notnull =>
            new RDeserializeError<TValue>(value, description);

        /// <summary>
        /// Create json scheme error
        /// </summary>
        /// <param name="jsonScheme">Json scheme</param>
        /// <param name="description">Description</param>
        /// <returns>Json scheme error</returns>
        public static RJsonSchemeError JsonSchemeError(string jsonScheme, string description) =>
            new(jsonScheme, description);

        /// <summary>
        /// Create not found error
        /// </summary>
        /// <typeparam name="TValue">Not found instance</typeparam>
        /// <param name="description">Description</param>
        /// <returns>Not found error</returns>
        public static IRValueNotFoundError ValueNotFoundError<TValue>(string description)
            where TValue : class =>
            new RValueNotFoundError<TValue>(description);

        /// <summary>
        /// Create not valid error
        /// </summary>
        /// <typeparam name="TValue">Not valid instance</typeparam>
        /// <param name="value">>Not valid instance</param>
        /// <param name="description">Description</param>
        /// <returns></returns>
        public static IRValueNotValidError ValueNotValidError<TValue>(TValue value, string description)
            where TValue : notnull =>
            new RValueNotValidError<TValue>(value, nameof(TValue));

        /// <summary>
        /// Create duplicate error
        /// </summary>
        /// <typeparam name="TValue">Not valid instance</typeparam>
        /// <param name="value">>Not valid instance</param>
        /// <param name="description">Description</param>
        /// <returns>Duplicate error</returns>
        public static IRValueDuplicateError ValueDuplicateError<TValue>(TValue value, string description)
            where TValue : notnull =>
            new RValueDuplicateError<TValue>(value, nameof(TValue));

        /// <summary>
        /// Create host connection error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="host">Host</param>
        /// <param name="description">Description</param>
        /// <returns>Host connection error</returns>
        public static RRestHostError RestHostError(RestErrorType restErrorType, string host, string description) =>
            new(restErrorType, host, description);

        /// <summary>
        /// Create timeout rest error
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="timeout">Timeout</param>
        /// <param name="description">Description</param>
        /// <returns>Timeout rest error</returns>
        public static RRestTimeoutError RestTimeoutError(string host, TimeSpan timeout, string description) =>
            new(host, timeout, description);

        /// <summary>
        /// Create rest error with response message
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        /// <returns> Rest error with response message</returns>
        public static RRestMessageError RestError(RestErrorType restErrorType, string reasonPhrase, string description) =>
            new(restErrorType, reasonPhrase, description);
    }
}