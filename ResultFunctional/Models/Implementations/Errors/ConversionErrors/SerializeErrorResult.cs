using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.conversionErrors;
using ResultFunctional.Models.Interfaces.Errors.ConversionErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.ConversionErrors
{
    /// <summary>
    /// Ошибка сериализации
    /// </summary>
    public class SerializeErrorResult<TValue> : ErrorBaseResult<ConversionErrorType, ISerializeErrorResult>, ISerializeErrorResult
       where TValue : notnull
    {
        public SerializeErrorResult(TValue value, string description)
          : this(value, description, null)
        { }

        protected SerializeErrorResult(TValue value, string description, Exception? exception)
            : base(ConversionErrorType.JsonConversion, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Конвертируемый параметр
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override ISerializeErrorResult InitializeType(string description, Exception? exception) =>
            new SerializeErrorResult<TValue>(Value, description, exception);
    }
}