using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Values.ResultValueBindTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений. Тесты
    /// </summary>
    public class ResultValueBindTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultValueBindTry_Ok()
        {
            var resultValue = ResultValueBindTry(() => Division(1).ToRValue(), Exceptions.ExceptionError());

            Assert.True(resultValue.Success);
            Assert.Equal(Division(1), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueBindTry_Exception()
        {
            var resultValue = ResultValueBindTry(() => Division(0).ToRValue(), Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultValueBindTryFunc_Ok()
        {
            var resultValue = ResultValueBindTry(() => Division(1).ToRValue(), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Success);
            Assert.Equal(Division(1), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueBindTryFunc_Exception()
        {
            var resultValue = ResultValueBindTry(() => Division(0).ToRValue(), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }
    }
}