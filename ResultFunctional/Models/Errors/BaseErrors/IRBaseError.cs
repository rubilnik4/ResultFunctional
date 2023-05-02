using System;

namespace ResultFunctional.Models.Errors.BaseErrors
{
    /// <summary>
    /// Base error with type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    public interface IRBaseError<out TErrorType> : IRError, IFormattable
        where TErrorType : struct
    {
        /// <summary>
        /// Error type
        /// </summary>
        TErrorType ErrorType { get; }
    }
}