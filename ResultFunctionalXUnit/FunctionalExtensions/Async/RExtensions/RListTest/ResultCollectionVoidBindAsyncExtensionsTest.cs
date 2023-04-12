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
    public class ResultCollectionVoidBindAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkBindAsync_Ok_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultCollectionVoidOkBindAsync(
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkBindAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultErrorTask = RListFactory.NoneTask< int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidOkBindAsync(
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionBadBindAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidBadBindAsync(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionBadBindAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidBadBindAsync(
                errors => voidObjectMock.Object.TestNumbersVoidAsync(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkBadBindAsync_Ok()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ResultCollectionVoidOkBadBindAsync(numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers),
                                                                     _ => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            Assert.Equal(initialCollection, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(initialCollection), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkBadBindAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultCollectionVoidOkBadBindAsync(
                _ => voidObjectMock.Object.TestVoidAsync(),
                errors => voidObjectMock.Object.TestNumbersVoidAsync(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkWhereBindAsync_Ok_OkPredicate_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultCollectionVoidOkWhereBindAsync(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkWhereBindAsync_Ok_BadPredicate_NotCallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = RListFactory.SomeTask(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultCollectionVoidOkWhereBindAsync(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkWhereBindAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidOkWhereBindAsync(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkWhereBindAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = RListFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidOkWhereBindAsync(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoidAsync(numbers));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoidAsync(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}