using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Database duplicate error
    /// </summary>
    /// <typeparam name="TValue">Database value</typeparam>
    public class DatabaseValueDuplicatedErrorResult<TValue> : DatabaseValueErrorResult<TValue, IDatabaseValueDuplicatedErrorResult>,
                                                              IDatabaseValueDuplicatedErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
    {
        /// <summary>
        /// Initialize database duplicate error
        /// </summary>
        /// <param name="value">Duplicated value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        public DatabaseValueDuplicatedErrorResult(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database duplicate error
        /// </summary>
        /// <param name="value">Duplicated value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected DatabaseValueDuplicatedErrorResult(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotFound, value, tableName, description, exception)
        { }

        /// <summary>
        /// Initialize database duplicate error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate database error</returns>
        protected override IDatabaseValueDuplicatedErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseValueDuplicatedErrorResult<TValue>(Value, TableName, description, exception);
    }
}