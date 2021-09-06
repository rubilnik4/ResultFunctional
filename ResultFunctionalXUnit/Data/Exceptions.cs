﻿using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

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
        public static IErrorResult ExceptionError() =>
            new ErrorTypeResult<CommonErrorType>(CommonErrorType.Unknown, "Деление на ноль", new DivideByZeroException());

        /// <summary>
        /// Вернуть ошибку на основании исключения
        /// </summary>
        public static Func<Exception, IErrorResult> ExceptionFunc() =>
            _ => new ErrorTypeResult<CommonErrorType>(CommonErrorType.Unknown, "Деление на ноль", new DivideByZeroException());
    }
}