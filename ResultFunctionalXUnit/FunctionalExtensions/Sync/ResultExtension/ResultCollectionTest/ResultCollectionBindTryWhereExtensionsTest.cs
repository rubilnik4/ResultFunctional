using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists.ResultCollectionBindTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с со связыванием коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class ResultCollectionBindTryWhereExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = new ResultCollection<int>(initialNumbers);

            var resultValue = numberResult.ResultCollectionBindTryOk(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numberResult.ResultCollectionBindTryOk(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = new ResultCollection<int>(initialNumbers);

            var resultValue = numberResult.ResultCollectionBindTryOk(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numberResult.ResultCollectionBindTryOk(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}