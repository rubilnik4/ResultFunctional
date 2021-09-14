using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка поля в базе данных
    /// </summary>
    public abstract class DatabaseValueErrorResult<TValue, TErrorResult> : DatabaseErrorResult<TErrorResult>, IDatabaseValueErrorResult
        where TValue : notnull
        where TErrorResult : IDatabaseErrorResult
    {
        protected DatabaseValueErrorResult(DatabaseErrorType databaseErrorType, TValue value, string tableName, string description)
          : this(databaseErrorType, value, tableName, description, null)
        { }

        protected DatabaseValueErrorResult(DatabaseErrorType databaseErrorType, TValue value, string tableName, string description, Exception? exception)
           : base(databaseErrorType, tableName, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Значение в строковом виде
        /// </summary>
        public string ValueToString =>
            Value.ToString();
    }
}