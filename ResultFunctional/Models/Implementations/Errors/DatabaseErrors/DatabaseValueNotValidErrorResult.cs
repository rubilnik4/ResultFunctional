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
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
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