using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.ConversionErrors
{
    /// <summary>
    /// Json scheme error
    /// </summary>
    public class RJsonSchemeError : RBaseError<ConversionErrorType, RJsonSchemeError>
    {
        /// <summary>
        /// Initialize json scheme error
        /// </summary>
        /// <param name="jsonScheme">Json scheme</param>
        /// <param name="description">Description</param>
        public RJsonSchemeError(string jsonScheme, string description)
            : this(jsonScheme, description, null)
        { }

        /// <summary>
        /// Initialize json scheme error
        /// </summary>
        /// <param name="jsonScheme">Json scheme</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RJsonSchemeError(string jsonScheme, string description, Exception? exception)
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
        protected override RJsonSchemeError InitializeType(string description, Exception? exception) =>
            new (JsonScheme, description, exception);
    }
}