using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctionalXUnit.Data
{
    /// <summary>
    /// Инициализация объектов для тестирования
    /// </summary>
    public static class ErrorData
    {
        /// <summary>
        /// Создать тестовый экземпляр ошибки
        /// </summary>
        public static IErrorResult CreateErrorTest() =>
            ErrorResultFactory.CommonError(CommonErrorType.Unknown, "Test error");

        /// <summary>
        /// Создать тестовый экземпляр списка ошибок
        /// </summary>
        public static IReadOnlyList<IErrorResult> CreateErrorListTwoTest() =>
            new List<IErrorResult>
            {
                CreateErrorTest(),
                CreateErrorTest(),
            };

        /// <summary>
        /// Создать тестовый экземпляр множества ошибок
        /// </summary>
        public static IEnumerable<IErrorResult> CreateErrorEnumerableTwoTest() =>
            CreateErrorListTwoTest();

        /// <summary>
        /// Создать тестовый экземпляр ошибки в задаче
        /// </summary>
        public static Task<IErrorResult> CreateErrorTestTask() =>
            Task.FromResult(CreateErrorTest());

        /// <summary>
        /// Создать тестовый экземпляр списка ошибок
        /// </summary>
        public static Task<IEnumerable<IErrorResult>> CreateErrorListTwoTestTask() =>
            Task.FromResult(CreateErrorEnumerableTwoTest());
    }
}