using System.Linq;
using ResultFunctional.Models.Implementations.Results;
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
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ с коллекцией и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = new ResultCollection<int>(initialNumbers);

            var resultCollection = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(resultCollection.HasErrors);
            Assert.NotNull(resultCollection.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией без ошибки и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOkFunc_Ok_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void ResultCollectionTryOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numbersResult.ResultCollectionTryOk(DivisionByCollection, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ с коллекцией и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = new ResultCollection<int>(initialNumbers);

            var resultCollection = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionFunc());

            Assert.True(resultCollection.HasErrors);
            Assert.NotNull(resultCollection.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и исключение
        /// </summary>
        [Fact]
        public void ResultCollectionTryOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultCollection<int>(initialError);

            var numberAfterTry = numberResult.ResultCollectionTryOk(DivisionCollectionByZero, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}