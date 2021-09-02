using System;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка поля в базе данных
    /// </summary>
    public abstract class DatabaseValueErrorResult<TValue> : DatabaseTableErrorResult
      where TValue : notnull
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
    }
}