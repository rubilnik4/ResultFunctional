using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class RListTryOptionExtensionsTest
    {
        /// <summary>
        /// Результирующий ответ с коллекцией без ошибки и отсутствие исключения
        /// </summary>
        [Fact]
        public void RListTryOk_Ok_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numberAfterTry = numbersResult.RListTrySome(DivisionByCollection, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void RListTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = numbersResult.RListTrySome(DivisionByCollection, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ с коллекцией и исключение
        /// </summary>
        [Fact]
        public void RListTryOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = initialNumbers.ToRList();

            var rList = numberResult.RListTrySome(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(rList.Failure);
            Assert.NotNull(rList.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и исключение
        /// </summary>
        [Fact]
        public void RListTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRList<int>();

            var numberAfterTry = numberResult.RListTrySome(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией без ошибки и отсутствие исключения
        /// </summary>
        [Fact]
        public void RListTryOkFunc_Ok_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numberAfterTry = numbersResult.RListTrySome(DivisionByCollection, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void RListTryOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = numbersResult.RListTrySome(DivisionByCollection, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ с коллекцией и исключение
        /// </summary>
        [Fact]
        public void RListTryOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = initialNumbers.ToRList();

            var rList = numberResult.RListTrySome(DivisionCollectionByZero, Exceptions.ExceptionFunc());

            Assert.True(rList.Failure);
            Assert.NotNull(rList.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с коллекцией с ошибкой и исключение
        /// </summary>
        [Fact]
        public void RListTryOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRList<int>();

            var numberAfterTry = numberResult.RListTrySome(DivisionCollectionByZero, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}