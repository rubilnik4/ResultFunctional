using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;

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
            RErrorFactory.ByType(CommonErrorType.Unknown, "Деление на ноль").AppendException(new DivideByZeroException());

        /// <summary>
        /// Вернуть ошибку на основании исключения
        /// </summary>
        public static Func<Exception, IRError> ExceptionFunc() =>
            _ => ExceptionError();
    }
}