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
    public class RListVoidAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RListVoidOkAsync_Ok_CallVoid()
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
        public async Task RListVoidOkAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var rMaybe = initialError.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidSomeAsync(
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task RListVoidBadAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidNoneAsync(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task RListVoidBadAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidNoneAsync(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RListVoidOkBadAsyncPart_Ok()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RListVoidMatchAsync(numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers),
                                                                                _ => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialCollection, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RListVoidOkBadAsyncPart_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidMatchAsync(
                _ => voidObjectMock.Object.TestVoidAsync(),
                errors => voidObjectMock.Object.TestNumbersVoid(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RListVoidOkBadAsync_Ok()
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
        public async Task RListVoidOkBadAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidMatchAsync(
                _ => voidObjectMock.Object.TestVoidAsync(),
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RListVoidOkWhereAsync_Ok_OkPredicate_CallVoid()
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
        public async Task RListVoidOkWhereAsync_Ok_BadPredicate_NotCallVoid()
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
        public async Task RListVoidOkWhereAsync_Bad_OkPredicate_NotCallVoid()
        {
            var rMaybe = CreateErrorTest().ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidOptionAsync(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RListVoidOkWhereAsync_Bad_BadPredicate_NotCallVoid()
        {
            var rMaybe = CreateErrorTest().ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidOptionAsync(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}