using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values.RValueBindTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений. Тесты
    /// </summary>
    public class RValueBindTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void RValueBindTry_Ok()
        {
            var rValue = RValueBindTry(() => Division(1).ToRValue(), Exceptions.ExceptionError());

            Assert.True(rValue.Success);
            Assert.Equal(Division(1), rValue.GetValue());
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void RValueBindTry_Exception()
        {
            var rValue = RValueBindTry(() => Division(0).ToRValue(), Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void RValueBindTryFunc_Ok()
        {
            var rValue = RValueBindTry(() => Division(1).ToRValue(), Exceptions.ExceptionFunc());

            Assert.True(rValue.Success);
            Assert.Equal(Division(1), rValue.GetValue());
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void RValueBindTryFunc_Exception()
        {
            var rValue = RValueBindTry(() => Division(0).ToRValue(), Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }
    }
}