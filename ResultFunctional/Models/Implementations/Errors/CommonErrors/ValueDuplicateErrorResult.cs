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
    public class ValueDuplicateErrorResult<TValue, TType> : ErrorBaseResult<CommonErrorType, IValueDuplicateErrorResult>,
                                                           IValueDuplicateErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
        where TType : Type
    {
        public ValueDuplicateErrorResult(TValue value, string description)
           : this(value, description, null)
        { }

        protected ValueDuplicateErrorResult(TValue value, string description, Exception? exception)
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
        protected override IValueDuplicateErrorResult InitializeType(string description, Exception? exception) =>
            new ValueDuplicateErrorResult<TValue, TType>(Value, description, exception);
    }
}