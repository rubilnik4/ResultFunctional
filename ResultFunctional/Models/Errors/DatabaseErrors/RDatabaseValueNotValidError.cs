using System;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database not valid value error
    /// </summary>
    /// <typeparam name="TValue">Database value</typeparam>
    public class RDatabaseValueNotValidError<TValue> : RDatabaseValueError<TValue, IRDatabaseValueNotValidError>,
                                                       IRDatabaseValueNotValidError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize database not valid value error
        /// </summary>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        public RDatabaseValueNotValidError(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database not valid value error
        /// </summary>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RDatabaseValueNotValidError(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotValid, value, tableName, description, exception)
        { }

        /// <summary>
        /// Initialize database not valid value error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate database error</returns>
        protected override IRDatabaseValueNotValidError InitializeType(string description, Exception? exception) =>
            new RDatabaseValueNotValidError<TValue>(Value, TableName, description, exception);
    }
}