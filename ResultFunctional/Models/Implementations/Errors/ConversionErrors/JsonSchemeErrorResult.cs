using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.conversionErrors;

namespace ResultFunctional.Models.Implementations.Errors.ConversionErrors
{
    /// <summary>
    /// Json scheme error
    /// </summary>
    public class JsonSchemeErrorResult : ErrorBaseResult<ConversionErrorType, JsonSchemeErrorResult>
    {
        /// <summary>
        /// Initialize json scheme error
        /// </summary>
        /// <param name="jsonScheme">Json scheme</param>
        /// <param name="description">Description</param>
        public JsonSchemeErrorResult(string jsonScheme, string description)
            : this(jsonScheme, description, null)
        { }

        /// <summary>
        /// Initialize json scheme error
        /// </summary>
        /// <param name="jsonScheme">Json scheme</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected JsonSchemeErrorResult(string jsonScheme, string description, Exception? exception)
            : base(ConversionErrorType.JsonScheme, description, exception)
        {
            JsonScheme = jsonScheme;
        }
        /// <summary>
        /// Json scheme
        /// </summary>
        public string JsonScheme { get; }

        /// <summary>
        /// Initialize json scheme error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Json scheme error</returns>
        protected override JsonSchemeErrorResult InitializeType(string description, Exception? exception) =>
            new JsonSchemeErrorResult(JsonScheme, description, exception);
    }
}