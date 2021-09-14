using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка отсутствующего поля в базе данных
    /// </summary>
    public class DatabaseValueNotFoundErrorResult<TValue> : DatabaseValueErrorResult<TValue, IDatabaseValueNotFoundErrorResult>, 
                                                            IDatabaseValueNotFoundErrorResult
        where TValue : notnull
    {
        public DatabaseValueNotFoundErrorResult(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        protected DatabaseValueNotFoundErrorResult(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotFound, value, tableName, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IDatabaseValueNotFoundErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseValueNotFoundErrorResult<TValue>(Value, TableName, description, exception);
    }
}