﻿using System;
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
        public SerializeErrorResult(ConversionErrorType conversionErrorType, TValue value, string description)
          : this(conversionErrorType, value, description, null)
        { }

        protected SerializeErrorResult(ConversionErrorType conversionErrorType, TValue value, string description, Exception? exception)
            : base(conversionErrorType, description, exception)
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
            new SerializeErrorResult<TValue>(ErrorType, Value, description, exception);
    }
}