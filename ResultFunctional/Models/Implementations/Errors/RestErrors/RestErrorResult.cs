using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.RestErrors;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера
    /// </summary>
    public abstract class RestErrorResult<TErrorResult> : ErrorBaseResult<RestErrorType, TErrorResult>, IRestErrorResult
        where TErrorResult : RestErrorResult<TErrorResult>
    {
        protected RestErrorResult(RestErrorType restErrorType, string description)
          : this(restErrorType, description, null)
        { }

        protected RestErrorResult(RestErrorType restErrorType, string description, Exception? exception)
            : base(restErrorType, description, exception)
        { }
    }
}