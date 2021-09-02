using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений. Тесты
    /// </summary>
    public class ResultValueTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultValueTry_Ok()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueTry(() => Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(Division(initialValue), resultValue.Value);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueTry_Exception()
        {
            const int initialValue = 0;
            var resultValue = ResultValueTry(() => Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultValueTryOk_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = new ResultValue<int>(initialValue);

            var numberAfterTry = numberResult.ResultValueTryOk(Division, CreateErrorTest());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(Division(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultValueTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = numberResult.ResultValueTryOk(Division, CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void ResultValueTryOk_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = new ResultValue<int>(initialValue);

            var resultValue = numberResult.ResultValueTryOk(Division, Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultValueTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = numberResult.ResultValueTryOk(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}