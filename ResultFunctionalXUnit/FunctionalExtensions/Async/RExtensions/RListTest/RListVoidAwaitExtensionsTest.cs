using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа с коллекцией задачей-объектом.Тесты
    /// </summary>
    public class RListVoidAwaitExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RListOkBindAsync_Ok_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.RListVoidSomeAwait(
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task RListOkBindAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var rMaybeTask = RListFactory.NoneTask< int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RListVoidSomeAwait(
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task RListBadBindAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybeTask = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RListVoidNoneAwait(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task RListBadBindAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybeTask = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RListVoidNoneAwait(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RListVoidOkBadBindAsyncPart_Ok()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RListVoidMatchAwait(numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers),
                                                                     _ => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            Assert.Equal(initialCollection, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(initialCollection), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RListVoidOkBadBindAsyncPart_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidMatchAwait(
                _ => voidObjectMock.Object.TestVoidAsync(),
                errors => voidObjectMock.Object.TestNumbersVoid(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RListVoidOkBadBindAsync_Ok()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RListVoidMatchAwait(numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers),
                                                                     _ => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            Assert.Equal(initialCollection, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(initialCollection), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RListVoidOkBadBindAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RListVoidMatchAwait(
                _ => voidObjectMock.Object.TestVoidAsync(),
                errors => voidObjectMock.Object.TestNumbersVoidAsync(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RListOkWhereBindAsync_Ok_OkPredicate_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.RListVoidOptionAwait(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RListOkWhereBindAsync_Ok_BadPredicate_NotCallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.RListVoidOptionAwait(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RListOkWhereBindAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybeTask = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RListVoidOptionAwait(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RListOkWhereBindAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybeTask = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RListVoidOptionAwait(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}