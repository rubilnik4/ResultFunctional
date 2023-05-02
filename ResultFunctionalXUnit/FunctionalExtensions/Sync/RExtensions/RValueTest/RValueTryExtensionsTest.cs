using System.Linq;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values.RValueTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений. Тесты
    /// </summary>
    public class RValueTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void RValueTry_Ok()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueTry(() => Division(initialValue), Exceptions.ExceptionError());

            Assert.True(rValue.Success);
            Assert.Equal(Division(initialValue), rValue.GetValue());
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void RValueTry_Exception()
        {
            const int initialValue = 0;
            var rValue = RValueTry(() => Division(initialValue), Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void RValueTryFunc_Ok()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueTry(() => Division(initialValue), Exceptions.ExceptionFunc());

            Assert.True(rValue.Success);
            Assert.Equal(Division(initialValue), rValue.GetValue());
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void RValueTryFunc_Exception()
        {
            const int initialValue = 0;
            var rValue = RValueTry(() => Division(initialValue), Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }
    }
}