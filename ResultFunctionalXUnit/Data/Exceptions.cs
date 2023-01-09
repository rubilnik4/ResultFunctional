using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctionalXUnit.Data
{
    /// <summary>
    /// Тестовые экземпляры исключений
    /// </summary>
    public static class Exceptions
    {
        /// <summary>
        /// Вернуть ошибку на основании исключения
        /// </summary>
        public static IRError ExceptionError() =>
            new CommonErrorResult(CommonErrorType.Unknown, "Деление на ноль", new DivideByZeroException());

        /// <summary>
        /// Вернуть ошибку на основании исключения
        /// </summary>
        public static Func<Exception, IRError> ExceptionFunc() =>
            _ => new CommonErrorResult(CommonErrorType.Unknown, "Деление на ноль", new DivideByZeroException());
    }
}