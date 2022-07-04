using System;

namespace ResultFunctional.Models.Interfaces.Errors.Base
{
    /// <summary>
    /// Base error with type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    public interface IErrorBaseResult<out TErrorType> : IErrorResult, IFormattable
        where TErrorType : struct
    {
        /// <summary>
        /// Error type
        /// </summary>
        TErrorType ErrorType { get; }
    }
}