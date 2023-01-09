using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Result error without value
    /// </summary>
    public interface IResultError
    {
        /// <summary>
        /// Errors
        /// </summary>
        IReadOnlyCollection<IRError> Errors { get; }

        /// <summary>
        /// hasn't errors
        /// </summary>
        bool OkStatus { get; }

        /// <summary>
        /// Has error
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Is error result type equal to current type
        /// </summary>
        /// <typeparam name="TError">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the current type; otherwise <see langword="false"/></returns>
        bool IsError<TError>() where TError : IRError;

        /// <summary>
        /// Is error result type equal to current or base type
        /// </summary>
        /// <typeparam name="TError">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal or derived to the current type; otherwise <see langword="false"/></returns>
        bool HasError<TError>() where TError : IRError;

        /// <summary>
        /// Get type of errors
        /// </summary>
        /// <returns>Errors types</returns>
        IReadOnlyCollection<Type> GetErrorByTypes();

        /// <summary>
        /// Is error type equal to current or base error type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
        bool HasErrorType<TErrorType>()
            where TErrorType : struct;

        /// <summary>
        /// Is error type equal to current or base error type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <param name="errorType">Error type</param>
        /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
        bool HasErrorType<TErrorType>(TErrorType errorType)
            where TErrorType : struct;

        /// <summary>
        /// Get error filtered by error type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <returns>Base error result filtered by error type</returns>
        IRBaseError<TErrorType>? GetErrorByType<TErrorType>()
            where TErrorType : struct;

        /// <summary>
        /// Get errors filtered by error type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <returns>Base errors result filtered by error type</returns>   
        IReadOnlyCollection<IRBaseError<TErrorType>> GetErrorsByTypes<TErrorType>()
            where TErrorType : struct;

        /// <summary>
        /// Add error to result
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result with error</returns>      
        IResultError AppendError(IRError error);

        /// <summary>
        /// Add errors to result
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result with error</returns>   
        IResultError ConcatErrors(IEnumerable<IRError> errors);

        /// <summary>
        /// Add values and errors to current result
        /// </summary>
        /// <param name="result">Result error</param>
        /// <returns>Result error</returns>
        IResultError ConcatResult(IResultError result);
    }
}