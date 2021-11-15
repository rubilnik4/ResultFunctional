using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Ошибка дублирующего значения
    /// </summary>
    public class ValueDuplicatedErrorResult<TValue, TType> : ErrorBaseResult<CommonErrorType, IValueDuplicatedErrorResult>,
                                                           IValueDuplicatedErrorResult
        where TValue : notnull
        where TType : Type
    {
        public ValueDuplicatedErrorResult(TValue value, string description)
           : this(value, description, null)
        { }

        protected ValueDuplicatedErrorResult(TValue value, string description, Exception? exception)
            : base(CommonErrorType.ValueDuplicated, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Родительский класс
        /// </summary>
        public Type ParentClass =>
            typeof(TType);

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IValueDuplicatedErrorResult InitializeType(string description, Exception? exception) =>
            new ValueDuplicatedErrorResult<TValue, TType>(Value, description, exception);
    }
}