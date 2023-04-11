using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
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
            var numbersResult = initialNumbers.ToRList();

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = initialNumbers.ToRList();

            var resultValue = numberResult.ResultCollectionBindTryOk(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRList<int>();

            var numberAfterTry = numberResult.ResultCollectionBindTryOk(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = numbersResult.ResultCollectionBindTryOk(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = initialNumbers.ToRList();

            var resultValue = numberResult.ResultCollectionBindTryOk(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRList<int>();

            var numberAfterTry = numberResult.ResultCollectionBindTryOk(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}