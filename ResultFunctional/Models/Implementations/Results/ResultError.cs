using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Result error without value
    /// </summary>
    public class ResultError : IResultError
    {
        /// <summary>
        /// Initializing without error
        /// </summary>
        public ResultError()
            : this(Enumerable.Empty<IErrorResult>())
        { }

        /// <summary>
        /// Initializing by error
        /// </summary>
        /// <param name="error">Error</param>
        public ResultError(IErrorResult error)
           : this(error.AsEnumerable())
        { }

        /// <summary>
        /// Initializing by errors
        /// </summary>
        /// <param name="errors">Errors</param>
        public ResultError(IEnumerable<IErrorResult> errors)
        {
            Errors = errors.ToList().AsReadOnly();
        }

        /// <summary>
        /// Errors
        /// </summary>
        public IReadOnlyCollection<IErrorResult> Errors { get; }

        /// <summary>
        /// Has no errors
        /// </summary>
        public bool OkStatus =>
            !HasErrors;

        /// <summary>
        /// Has error
        /// </summary>
        public bool HasErrors =>
            Errors.Any();

        /// <summary>
        /// Is error result type equal to current type
        /// </summary>
        /// <typeparam name="TError">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the current type; otherwise <see langword="false"/></returns>
        public bool IsError<TError>()
            where TError : IErrorResult =>
            Errors.Any(error => error.IsErrorResult<TError>());

        /// <summary>
        /// Is error result type equal to current or base type
        /// </summary>
        /// <typeparam name="TError">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal or derived to the current type; otherwise <see langword="false"/></returns>
        public bool HasError<TError>()
            where TError : IErrorResult =>
            Errors.Any(error => error.HasErrorResult<TError>());

        /// <summary>
        /// Get type of errors
        /// </summary>
        /// <returns>Errors types</returns>
        public IReadOnlyCollection<Type> GetErrorByTypes() =>
            Errors.
            Select(error => error.GetType()).
            ToList();

        /// <summary>
        /// Is error type equal to current or base error type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
        public bool HasErrorType<TErrorType>()
            where TErrorType : struct =>
            Errors.Any(error => error is IErrorBaseResult<TErrorType>);

        /// <summary>
        /// Is error type equal to current or base error type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <param name="errorType">Error type</param>
        /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
        public bool HasErrorType<TErrorType>(TErrorType errorType)
            where TErrorType : struct =>
            Errors.OfType<IErrorBaseResult<TErrorType>>().
                   Any(error => error.ErrorType.Equals(errorType));

        /// <summary>
        /// Get error filtered by error type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <returns>Base error result filtered by error type</returns>
        public IErrorBaseResult<TErrorType>? GetErrorByType<TErrorType>()
            where TErrorType : struct =>
            Errors.
            OfType<IErrorBaseResult<TErrorType>>().
            FirstOrDefault();

        /// <summary>
        /// Get errors filtered by error type
        /// </summary>
        /// <typeparam name="TErrorType">Error type</typeparam>
        /// <returns>Base errors result filtered by error type</returns>
        public IReadOnlyCollection<IErrorBaseResult<TErrorType>> GetErrorsByTypes<TErrorType>()
            where TErrorType : struct =>
            Errors.
            OfType<IErrorBaseResult<TErrorType>>().
            ToList();

        /// <summary>
        /// Add error to result
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result with error</returns>    
        public IResultError AppendError(IErrorResult error) =>
            new ResultError(Errors.Concat(error));

        /// <summary>
        /// Add errors to result
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result with error</returns>  
        public IResultError ConcatErrors(IEnumerable<IErrorResult> errors) =>
            new ResultError(Errors.Concat(errors));

        /// <summary>
        /// Add values and errors to current result
        /// </summary>
        /// <param name="result">Result error</param>
        /// <returns>Result error</returns>
        public IResultError ConcatResult(IResultError result) =>
            new ResultError(Errors.Concat(result.Errors));
    }
}