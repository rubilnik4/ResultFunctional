using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка дублирующего поля в базе данных
    /// </summary>
    public class DatabaseValueDuplicatedErrorResult<TValue> : DatabaseValueErrorResult<TValue, IDatabaseValueDuplicatedErrorResult>, IDatabaseValueDuplicatedErrorResult
        where TValue : notnull
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
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseValueDuplicatedErrorResult<TValue>(Value, TableName, description, exception);
    }
}