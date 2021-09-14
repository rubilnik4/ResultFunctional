using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Ошибка с параметром
    /// </summary>
    public class CommonErrorResult : ErrorBaseResult<CommonErrorType, CommonErrorResult>
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
        protected override CommonErrorResult InitializeType(string description, Exception? exception) =>
            new CommonErrorResult(ErrorType, description, exception);
    }
}