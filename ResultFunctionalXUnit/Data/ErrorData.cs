using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;

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
        public static IRError CreateErrorTest() =>
            RErrorFactory.Common(CommonErrorType.Unknown, "Test error");

        /// <summary>
        /// Создать тестовый экземпляр списка ошибок
        /// </summary>
        public static IReadOnlyList<IRError> CreateErrorListTwoTest() =>
            new List<IRError>
            {
                CreateErrorTest(),
                CreateErrorTest(),
            };

        /// <summary>
        /// Создать тестовый экземпляр множества ошибок
        /// </summary>
        public static IReadOnlyCollection<IRError> CreateErrorEnumerableTwoTest() =>
            CreateErrorListTwoTest();

        /// <summary>
        /// Создать тестовый экземпляр ошибки в задаче
        /// </summary>
        public static Task<IRError> CreateErrorTestTask() =>
            Task.FromResult(CreateErrorTest());

        /// <summary>
        /// Создать тестовый экземпляр списка ошибок
        /// </summary>
        public static Task<IReadOnlyCollection<IRError>> CreateErrorListTwoTestTask() =>
            Task.FromResult(CreateErrorEnumerableTwoTest());
    }
}