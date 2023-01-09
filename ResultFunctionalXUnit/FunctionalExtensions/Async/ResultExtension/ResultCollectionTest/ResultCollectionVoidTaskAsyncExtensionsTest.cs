using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using ResultFunctional.Models.Factories;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа с коллекцией задачей-объектом.Тесты
    /// </summary>
    public class ResultCollectionVoidTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkBindAsync_Ok_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultCollectionVoidOkTaskAsync(
                numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.Value));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkBindAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultErrorTask = ResultCollectionFactory.CreateTaskResultCollectionError<int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidOkTaskAsync(
                numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(resultAfterVoid.Errors.Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionBadBindAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidBadTaskAsync(
                errors => voidObjectMock.Object.TestNumbersVoid(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionBadBindAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidBadTaskAsync(
                errors => voidObjectMock.Object.TestNumbersVoid(GetListByErrorsCount(errors)));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkBadTaskAsync_Ok()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ResultCollectionVoidOkBadTaskAsync(numbers => voidObjectMock.Object.TestNumbersVoid(numbers),
                                                                     _ => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            Assert.Equal(initialCollection, resultAfterVoid.Value);
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(initialCollection), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task ResultCollectionVoidOkBadTaskAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultCollectionVoidOkBadTaskAsync(
                _ => voidObjectMock.Object.TestVoid(),
                errors => voidObjectMock.Object.TestNumbersVoid(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkWhereBindAsync_Ok_OkPredicate_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultCollectionVoidOkWhereTaskAsync(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.Value));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkWhereBindAsync_Ok_BadPredicate_NotCallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOkTask = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultCollectionVoidOkWhereTaskAsync(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.Value));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkWhereBindAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidOkWhereTaskAsync(_ => true,
                numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultCollectionOkWhereBindAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultCollectionVoidOkWhereTaskAsync(_ => false,
                numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}