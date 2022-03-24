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
    /// Ошибка неверного значения
    /// </summary>
    public class ValueNotValidErrorResult<TValue, TType> : ErrorBaseResult<CommonErrorType, IValueNotValidErrorResult>,
                                                           IValueNotValidErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
        where TType : Type
    {
        public ValueNotValidErrorResult(TValue value, string description)
           : this(value, description, null)
        { }

        protected ValueNotValidErrorResult(TValue value, string description, Exception? exception)
            : base(CommonErrorType.ValueNotValid, description, exception)
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
        protected override IValueNotValidErrorResult InitializeType(string description, Exception? exception) =>
            new ValueNotValidErrorResult<TValue, TType>(Value, description, exception);
    }
}