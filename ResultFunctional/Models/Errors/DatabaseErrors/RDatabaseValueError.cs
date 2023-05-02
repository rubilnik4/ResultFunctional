using System;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database value field error
    /// </summary>
    /// <typeparam name="TValue">Database value</typeparam>
    /// <typeparam name="TError">Database error type</typeparam>
    public abstract class RDatabaseValueError<TValue, TError> : RDatabaseError<TError>, IRDatabaseValueError
        where TValue : notnull
        where TError : IDatabaseErrorResult
    {
        /// <summary>
        /// Initialize database value field error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        protected RDatabaseValueError(DatabaseErrorType databaseErrorType, TValue value, string tableName, string description)
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
        protected RDatabaseValueError(DatabaseErrorType databaseErrorType, TValue value, string tableName,
                                      string description, Exception? exception)
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
            Value.ToString() ?? String.Empty;
    }
}