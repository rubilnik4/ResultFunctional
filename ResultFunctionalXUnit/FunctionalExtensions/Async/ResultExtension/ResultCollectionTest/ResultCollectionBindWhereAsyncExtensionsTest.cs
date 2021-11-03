using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class ResultCollectionBindWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueAsync(_ => true,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueAsync(_ => false,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ToTaskEnumerable(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueAsync(_ => true,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueAsync(_ => false,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereAsync(_ => true,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ResultCollectionFactory.CreateTaskResultCollectionError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereAsync(_ => false,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ResultCollectionFactory.CreateTaskResultCollectionError<string>(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereAsync(_ => true,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ResultCollectionFactory.CreateTaskResultCollectionError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereAsync(_ => false,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ResultCollectionFactory.CreateTaskResultCollectionError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkAsync(
                numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkAsync(
                numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadAsync(
                errors => ResultCollectionFactory.CreateTaskResultCollection(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadAsync(
                errors => ResultCollectionFactory.CreateTaskResultCollection(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkAsync_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var resultValue = new ResultCollection<int>(initialCollection);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultCollection<int>(errorsInitial);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialError = CreateErrorTest();
            var resultValue = new ResultCollection<int>(errorsInitial);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IResultError resultError) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                         ReturnsAsync(resultError));
    }
}