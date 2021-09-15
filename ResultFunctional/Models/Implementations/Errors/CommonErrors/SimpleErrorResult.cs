using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Простая ошибка
    /// </summary>
    public class SimpleErrorResult : ErrorResult
    {
        public SimpleErrorResult(string description)
            : this(description, null)
        { }

        public SimpleErrorResult(string description, Exception? exception)
            : base(description, exception)
        { }

        /// <summary>
        /// Идентификатор ошибки
        /// </summary>
        public override string Id =>
            GetType().Name;

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new SimpleErrorResult(description, exception);
    }
}