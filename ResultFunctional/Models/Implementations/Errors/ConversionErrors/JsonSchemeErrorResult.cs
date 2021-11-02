using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.conversionErrors;

namespace ResultFunctional.Models.Implementations.Errors.ConversionErrors
{
    /// <summary>
    /// Ошибка json схемы
    /// </summary>
    public class JsonSchemeErrorResult : ErrorBaseResult<ConversionErrorType, JsonSchemeErrorResult>
    {
        public JsonSchemeErrorResult(string jsonScheme, string description)
            : this(jsonScheme, description, null)
        { }

        protected JsonSchemeErrorResult(string jsonScheme, string description, Exception? exception)
            : base(ConversionErrorType.JsonScheme, description, exception)
        {
            JsonScheme = jsonScheme;
        }

        /// <summary>
        /// Конвертируемый параметр
        /// </summary>
        public string JsonScheme { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override JsonSchemeErrorResult InitializeType(string description, Exception? exception) =>
            new JsonSchemeErrorResult(JsonScheme, description, exception);
    }
}