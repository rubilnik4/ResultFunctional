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

        /// <summary>
        /// Is error type equal to current error type
        /// </summary>
        /// <typeparam name="TErrorTypeCompare">Error type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        bool IsErrorType<TErrorTypeCompare>()
            where TErrorTypeCompare : struct;
    }
}