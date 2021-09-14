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
    /// Ошибка отсутствующего значения
    /// </summary>
    public class ValueNotFoundErrorResult<TValue, TType> : ErrorBaseResult<CommonErrorType, IValueNotFoundErrorResult>,
                                                           IValueNotFoundErrorResult
        where TValue : notnull
        where TType : Type
    {
        public ValueNotFoundErrorResult(string description)
           : this(description, null)
        { }

        protected ValueNotFoundErrorResult(string description, Exception? exception)
            : base(CommonErrorType.ValueNotFound, description, exception)
        { }

        /// <summary>
        /// Параметр
        /// </summary>
        public Type ValueType =>
             typeof(TValue);

        /// <summary>
        /// Родительский класс
        /// </summary>
        public Type ParentClass =>
            typeof(TType);

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new ValueNotFoundErrorResult<TValue, TType>(description, exception);

    }
}