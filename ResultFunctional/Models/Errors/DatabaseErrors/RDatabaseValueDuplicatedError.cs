using System;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database duplicate value error
    /// </summary>
    /// <typeparam name="TValue">Database value</typeparam>
    public class RDatabaseValueDuplicatedError<TValue> : RDatabaseValueError<TValue, IRDatabaseValueDuplicatedError>,
                                                         IRDatabaseValueDuplicatedError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize database duplicate value error
        /// </summary>
        /// <param name="value">Duplicated value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        public RDatabaseValueDuplicatedError(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database duplicate error
        /// </summary>
        /// <param name="value">Duplicated value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RDatabaseValueDuplicatedError(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotFound, value, tableName, description, exception)
        { }

        /// <summary>
        /// Initialize database duplicate value error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate value database error</returns>
        protected override IRDatabaseValueDuplicatedError InitializeType(string description, Exception? exception) =>
            new RDatabaseValueDuplicatedError<TValue>(Value, TableName, description, exception);
    }
}