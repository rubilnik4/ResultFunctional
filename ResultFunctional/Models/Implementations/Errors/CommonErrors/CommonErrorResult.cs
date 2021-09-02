using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Ошибка с параметром
    /// </summary>
    public class CommonErrorResult : ErrorBaseResult<CommonErrorType>
    {
        public CommonErrorResult(CommonErrorType commonErrorType, string description)
            : this(commonErrorType, description, null)
        { }

        public CommonErrorResult(CommonErrorType commonErrorType, string description, Exception? exception)
            : base(commonErrorType, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new CommonErrorResult(ErrorType, description, exception);
    }
}