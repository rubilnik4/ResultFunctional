using System;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database not found value error
    /// </summary>
    /// <typeparam name="TValue">Database value</typeparam>
    public class RDatabaseValueNotFoundError<TValue> : RDatabaseValueError<TValue, IRDatabaseValueNotFoundError>, 
                                                       IRDatabaseValueNotFoundError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize database not found value error
        /// </summary>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        public RDatabaseValueNotFoundError(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database not found value error
        /// </summary>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RDatabaseValueNotFoundError(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotFound, value, tableName, description, exception)
        { }

        /// <summary>
        /// Initialize database not found value error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate database error</returns>
        protected override IRDatabaseValueNotFoundError InitializeType(string description, Exception? exception) =>
            new RDatabaseValueNotFoundError<TValue>(Value, TableName, description, exception);
    }
}