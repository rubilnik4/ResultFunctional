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
    /// Not found error
    /// </summary>
    /// <typeparam name="TValue">Not found instance</typeparam>
    public class ValueNotFoundErrorResult<TValue> : ErrorBaseResult<CommonErrorType, IValueNotFoundErrorResult>, 
                                                           IValueNotFoundErrorResult
        where TValue : notnull
    {
        /// <summary>
        /// Initialize not found error
        /// </summary>
        /// <param name="description">Description</param>
        public ValueNotFoundErrorResult(string description)
           : this(description, null)
        { }

        /// <summary>
        /// Initialize not found error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected ValueNotFoundErrorResult(string description, Exception? exception)
            : base(CommonErrorType.ValueNotFound, description, exception)
        { }

        /// <summary>
        /// Type of not found value
        /// </summary>
        public Type ValueType =>
             typeof(TValue);

        /// <summary>
        /// Initialize not found error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate error</returns>
        protected override IValueNotFoundErrorResult InitializeType(string description, Exception? exception) =>
            new ValueNotFoundErrorResult<TValue>(description, exception);
    }
}