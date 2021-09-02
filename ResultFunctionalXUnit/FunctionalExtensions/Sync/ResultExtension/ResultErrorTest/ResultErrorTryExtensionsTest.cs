using System.Linq;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors.ResultErrorTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class ResultErrorTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public void ResultErrorTry_Ok()
        {
            int initialValue = Numbers.Number;
            var resultError = ResultErrorTry(() => SyncFunctions.Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultError.OkStatus);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultErrorTry_Exception()
        {
            const int initialValue = 0;
            var resultError = ResultErrorTry(() => SyncFunctions.Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultError.HasErrors);
            Assert.NotNull(resultError.Errors.First().Exception);
        }
    }
}