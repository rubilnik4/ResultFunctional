using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.ConversionErrors
{
    /// <summary>
    /// Serialize error
    /// </summary>
    /// <typeparam name="TValue">Serialize type</typeparam>
    public class RSerializeError<TValue> : RBaseError<ConversionErrorType, IRSerializeError>, IRSerializeError
        where TValue : notnull
    {
        /// <summary>
        /// Initialize serialize error
        /// </summary>
        /// <param name="value">Serialize instance</param>
        /// <param name="description">Description</param>
        public RSerializeError(TValue value, string description)
          : this(value, description, null)
        { }

        /// <summary>
        /// Initialize serialize error
        /// </summary>
        /// <param name="value">Serialize instance</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RSerializeError(TValue value, string description, Exception? exception)
            : base(ConversionErrorType.JsonConversion, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Serialize value
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Type of not valid value
        /// </summary>
        public Type ValueType =>
             typeof(TValue);

        /// <summary>
        /// Initialize serialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Serialize error</returns>
        protected override IRSerializeError InitializeType(string description, Exception? exception) =>
            new RSerializeError<TValue>(Value, description, exception);
    }
}