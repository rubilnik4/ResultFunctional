using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка дублирующего поля в базе данных
    /// </summary>
    public class DatabaseValueDuplicatedErrorResult<TValue> : DatabaseValueErrorResult<TValue, IDatabaseValueDuplicatedErrorResult>, 
                                                              IDatabaseValueDuplicatedErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
    {
        public DatabaseValueDuplicatedErrorResult(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        protected DatabaseValueDuplicatedErrorResult(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotFound, value, tableName, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IDatabaseValueDuplicatedErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseValueDuplicatedErrorResult<TValue>(Value, TableName, description, exception);
    }
}