using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.AuthorizeErrors
{
    /// <summary>
    /// Ошибка при авторизации
    /// </summary>
    public class AuthorizeErrorResult : ErrorBaseResult<AuthorizeErrorType>
    {
        public AuthorizeErrorResult(AuthorizeErrorType authorizeErrorType, string description)
            : this(authorizeErrorType, description, null)
        { }

        protected AuthorizeErrorResult(AuthorizeErrorType authorizeErrorType, string description, Exception? exception)
            : base(authorizeErrorType, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new AuthorizeErrorResult(ErrorType, description, exception);
    }
}