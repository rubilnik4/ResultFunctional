using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists.ResultCollectionTryExtensions;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class ResultCollectionTryWhereExtensionsTest
    {
        /// <summary>
        /// Результирующий ответ с коллекцией без ошибки и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_Ok_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ с коллекцией и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = initialNumbers.ToRList();

            var resultCollection = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(resultCollection.Failure);
            Assert.NotNull(resultCollection.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRList<int>();

            var numberAfterTry = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией без ошибки и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOkFunc_Ok_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ с коллекцией и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = initialNumbers.ToRList();

            var resultCollection = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionFunc());

            Assert.True(resultCollection.Failure);
            Assert.NotNull(resultCollection.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRList<int>();

            var numberAfterTry = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}