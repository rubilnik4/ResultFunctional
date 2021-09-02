using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueBindTryExtensions;

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
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultValueBindTryOk_OkResult_OkTry()
        {
            const int initialNumber = 2;
            var numberResult = new ResultValue<int>(initialNumber);

            var numberAfterTry = numberResult.ResultValueBindTryOk(number => new ResultValue<int>(Division(number)),
                                                                   Exceptions.ExceptionError());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(Division(initialNumber), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultValueBindTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = numberResult.ResultValueBindTryOk(number => new ResultValue<int>(Division(number)),
                                                                   Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void ResultValueBindTryOk_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = new ResultValue<int>(initialNumber);

            var resultValue = numberResult.ResultValueBindTryOk(number => new ResultValue<int>(Division(number)),
                                                                   Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultValueBindTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = numberResult.ResultValueBindTryOk(number => new ResultValue<int>(Division(number)), 
                                                                   Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}