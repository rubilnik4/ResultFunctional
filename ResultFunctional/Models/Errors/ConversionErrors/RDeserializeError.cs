using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.ConversionErrors
{
    /// <summary>
    /// Deserialize error
    /// </summary>
    /// <typeparam name="TValue">Deserialize type</typeparam>
    public class RDeserializeError<TValue> : RBaseError<ConversionErrorType, IRDeserializeError>, IRDeserializeError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize deserialize error
        /// </summary>
        /// <param name="value">Deserialize instance</param>
        /// <param name="description">Description</param>
        public RDeserializeError(string value, string description)
            : this(value, description, null)
        { }

        /// <summary>
        /// Initialize deserialize error
        /// </summary>
        /// <param name="value">Deserialize instance</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RDeserializeError(string value, string description, Exception? exception)
            : base(ConversionErrorType.JsonConversion, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Deserialize value
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Type of not valid value
        /// </summary>
        public Type ValueType =>
             typeof(TValue);

        /// <summary>
        /// Initialize deserialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Deserialize error</returns>
        protected override IRDeserializeError InitializeType(string description, Exception? exception) =>
            new RDeserializeError<TValue>(Value, description, exception);
    }
}