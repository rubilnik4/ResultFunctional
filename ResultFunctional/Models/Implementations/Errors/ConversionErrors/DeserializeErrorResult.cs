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
    /// Deserialize error
    /// </summary>
    /// <typeparam name="TValue">Deserialize type</typeparam>
    public class DeserializeErrorResult<TValue> : ErrorBaseResult<ConversionErrorType, IDeserializeErrorResult>, 
                                                  IDeserializeErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
    {
        /// <summary>
        /// Initialize deserialize error
        /// </summary>
        /// <param name="value">Deserialize instance</param>
        /// <param name="description">Description</param>
        public DeserializeErrorResult(string value, string description)
            : this(value, description, null)
        { }

        /// <summary>
        /// Initialize deserialize error
        /// </summary>
        /// <param name="value">Deserialize instance</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected DeserializeErrorResult(string value, string description, Exception? exception)
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
        protected override IDeserializeErrorResult InitializeType(string description, Exception? exception) =>
            new DeserializeErrorResult<TValue>(Value, description, exception);
    }
}