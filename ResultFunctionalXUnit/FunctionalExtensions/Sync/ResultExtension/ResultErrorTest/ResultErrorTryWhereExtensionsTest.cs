using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Units;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Units.ResultErrorTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class ResultErrorTryWhereExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public void ResultErrorTryWhere_Ok()
        {
            int initialValue = Numbers.Number;
            var numberResult = RUnitFactory.Some();

            var resultError = numberResult.ResultErrorTryOk(() => SyncFunctions.Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultError.Success);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultErrorTryWhere_Exception()
        {
            const int initialValue = 0;
            var numberResult = RUnitFactory.Some();

            var resultError = numberResult.ResultErrorTryOk(() => SyncFunctions.Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultError.Failure);
            Assert.NotNull(resultError.GetErrors().First().Exception);
        }
    }
}