using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Database value field error
    /// </summary>
    /// <typeparam name="TValue">Database value</typeparam>
    /// <typeparam name="TErrorResult">Database error type</typeparam>
    public abstract class DatabaseValueErrorResult<TValue, TErrorResult> : DatabaseErrorResult<TErrorResult>, IDatabaseValueErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
            where TValue : notnull
#endif
        where TErrorResult : IDatabaseErrorResult
    {
        /// <summary>
        /// Initialize database value field error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        protected DatabaseValueErrorResult(DatabaseErrorType databaseErrorType, TValue value, string tableName, string description)
          : this(databaseErrorType, value, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database value field error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected DatabaseValueErrorResult(DatabaseErrorType databaseErrorType, TValue value, string tableName, string description, Exception? exception)
           : base(databaseErrorType, tableName, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Database value
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Database value in string type
        /// </summary>
        public string ValueToString =>
            Value?.ToString() ?? String.Empty;
    }
}