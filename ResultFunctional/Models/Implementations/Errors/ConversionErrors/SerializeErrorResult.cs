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
    /// Serialize error
    /// </summary>
    /// <typeparam name="TValue">Serialize instance</typeparam>
    public class SerializeErrorResult<TValue> : ErrorBaseResult<ConversionErrorType, ISerializeErrorResult>, ISerializeErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
    {
        /// <summary>
        /// Initialize serialize error
        /// </summary>
        /// <param name="value">Deserialize instance</param>
        /// <param name="description">Description</param>
        public SerializeErrorResult(TValue value, string description)
          : this(value, description, null)
        { }

        /// <summary>
        /// Initialize serialize error
        /// </summary>
        /// <param name="value">Deserialize instance</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected SerializeErrorResult(TValue value, string description, Exception? exception)
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
        protected override ISerializeErrorResult InitializeType(string description, Exception? exception) =>
            new SerializeErrorResult<TValue>(Value, description, exception);
    }
}