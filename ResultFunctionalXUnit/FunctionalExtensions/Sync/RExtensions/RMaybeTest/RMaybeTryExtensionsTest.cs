using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RMaybeTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class RMaybeTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public void RMaybeTryWhere_Ok()
        {
            int initialValue = Numbers.Number;
            var numberResult = RUnitFactory.Some();

            var rMaybe = numberResult.RMaybeTrySome(() => SyncFunctions.Division(initialValue), Exceptions.ExceptionError());

            Assert.True(rMaybe.Success);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void RMaybeTryWhere_Exception()
        {
            const int initialValue = 0;
            var numberResult = RUnitFactory.Some();

            var rMaybe = numberResult.RMaybeTrySome(() => SyncFunctions.Division(initialValue), Exceptions.ExceptionError());

            Assert.True(rMaybe.Failure);
            Assert.NotNull(rMaybe.GetErrors().First().Exception);
        }
    }
}