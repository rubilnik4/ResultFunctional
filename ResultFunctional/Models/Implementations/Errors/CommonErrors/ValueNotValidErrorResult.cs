using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Not valid error
    /// </summary>
    /// <typeparam name="TValue">Not valid instance</typeparam>
    public class ValueNotValidErrorResult<TValue> : ErrorBaseResult<CommonErrorType, IValueNotValidErrorResult>,
                                                           IValueNotValidErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
    {
        /// <summary>
        /// Initialize not valid error
        /// </summary>
        /// <param name="value">Not valid instance</param>
        /// <param name="description">Description</param>
        public ValueNotValidErrorResult(TValue value, string description)
           : this(value, description, null)
        { }

        /// <summary>
        /// Initialize not valid error
        /// </summary>
        /// <param name="value">Not valid instance</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected ValueNotValidErrorResult(TValue value, string description, Exception? exception)
            : base(CommonErrorType.ValueNotValid, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Not valid value
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Type of not valid value
        /// </summary>
        public Type ValueType =>
             typeof(TValue);

        /// <summary>
        /// Initialize not valid error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate error</returns>
        protected override IValueNotValidErrorResult InitializeType(string description, Exception? exception) =>
            new ValueNotValidErrorResult<TValue>(Value, description, exception);
    }
}