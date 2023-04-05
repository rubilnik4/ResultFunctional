using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Values.ResultValueBindTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
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
            var resultValue = ResultValueBindTry(() => new ResultValue<int>(Division(1)), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(Division(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueBindTry_Exception()
        {
            var resultValue = ResultValueBindTry(() => new ResultValue<int>(Division(0)), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultValueBindTryFunc_Ok()
        {
            var resultValue = ResultValueBindTry(() => new ResultValue<int>(Division(1)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(Division(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueBindTryFunc_Exception()
        {
            var resultValue = ResultValueBindTry(() => new ResultValue<int>(Division(0)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }
    }
}