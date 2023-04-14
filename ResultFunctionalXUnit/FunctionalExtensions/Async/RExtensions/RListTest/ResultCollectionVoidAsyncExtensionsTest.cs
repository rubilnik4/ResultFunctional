using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionVoidAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkAsync_Ok_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RListVoidSomeAsync(
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultError = initialError.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.RListVoidSomeAsync(
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidBadAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.RListVoidNoneAsync(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidBadAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.RListVoidNoneAsync(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkBadAsync_Ok()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RListVoidMatchAsync(numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers),
                                                                                _ => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialCollection, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkBadAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.RListVoidMatchAsync(
                _ => voidObjectMock.Object.TestVoidAsync(),
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkWhereAsync_Ok_OkPredicate_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RListVoidOptionAsync(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkWhereAsync_Ok_BadPredicate_NotCallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RListVoidOptionAsync(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkWhereAsync_Bad_OkPredicate_NotCallVoid()
        {
            var resultError = CreateErrorTest().ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.RListVoidOptionAsync(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkWhereAsync_Bad_BadPredicate_NotCallVoid()
        {
            var resultError = CreateErrorTest().ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.RListVoidOptionAsync(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}