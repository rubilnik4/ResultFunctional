using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка неверного поля в базе данных
    /// </summary>
    public class DatabaseValueNotValidErrorResult<TValue> : DatabaseValueErrorResult<TValue, IDatabaseValueNotValidErrorResult>,
                                                            IDatabaseValueNotValidErrorResult
        where TValue : notnull
    {
        public DatabaseValueNotValidErrorResult(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        protected DatabaseValueNotValidErrorResult(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotValid, value, tableName, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IDatabaseValueNotValidErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseValueNotValidErrorResult<TValue>(Value, TableName, description, exception);
    }
}