using System;
using System.Collections.Generic;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Duplicate error subtype
    /// </summary>
    /// <typeparam name="TValue">Duplicate instance</typeparam>
    /// <typeparam name="TType">Type of duplicate instance</typeparam>
    public class ValueDuplicateErrorResult<TValue, TType> : ErrorBaseResult<CommonErrorType, IValueDuplicateErrorResult>,
                                                            IValueDuplicateErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
        where TType : Type
    {
        /// <summary>
        /// Initialize duplicate error
        /// </summary>
        /// <param name="value">Duplicate instance</param>
        /// <param name="description">Description</param>
        public ValueDuplicateErrorResult(TValue value, string description)
           : this(value, description, null)
        { }

        /// <summary>
        /// Initialize duplicate error
        /// </summary>
        /// <param name="value">Duplicate instance</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected ValueDuplicateErrorResult(TValue value, string description, Exception? exception)
            : base(CommonErrorType.ValueDuplicated, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Duplicate instance
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Type of duplicate value
        /// </summary>
        public Type ValueType =>
             typeof(TValue);

        /// <summary>
        /// Initialize duplicate error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate error</returns>
        protected override IValueDuplicateErrorResult InitializeType(string description, Exception? exception) =>
            new ValueDuplicateErrorResult<TValue, TType>(Value, description, exception);
    }
}