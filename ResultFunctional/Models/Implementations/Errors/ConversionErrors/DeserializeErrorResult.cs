using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.conversionErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.ConversionErrors
{
    /// <summary>
    /// Ошибка десериализации
    /// </summary>
    public class DeserializeErrorResult<TValue> : ErrorBaseResult<ConversionErrorType, IDeserializeErrorResult>, 
                                                  IDeserializeErrorResult
       where TValue : notnull
    {
        public DeserializeErrorResult(ConversionErrorType conversionErrorType, string value, string description)
            : this(conversionErrorType, value, description, null)
        { }

        protected DeserializeErrorResult(ConversionErrorType conversionErrorType, string value, string description, Exception? exception)
            : base(conversionErrorType, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Конвертируемый параметр
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Тип конвертации
        /// </summary>
        public Type DeserializeType => 
            typeof(TValue);

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IDeserializeErrorResult InitializeType(string description, Exception? exception) =>
            new DeserializeErrorResult<TValue>(ErrorType, Value, description, exception);
    }
}