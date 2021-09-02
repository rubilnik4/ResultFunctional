using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа с коллекцией для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionBindWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueBindAsync(_ => true,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueBindAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueBindAsync(_ => false,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ToTaskEnumerable(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueBindAsync(_ => true,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueBindAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueBindAsync(_ => false,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereBindAsync(_ => true,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ResultCollectionFactory.CreateTaskResultCollectionError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereBindAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereBindAsync(_ => false,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ResultCollectionFactory.CreateTaskResultCollectionError<string>(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereBindAsync(_ => true,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ResultCollectionFactory.CreateTaskResultCollectionError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereBindAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereBindAsync(_ => false,
                okFunc: numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)),
                badFunc: _ => ResultCollectionFactory.CreateTaskResultCollectionError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkBindAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBindAsync(
                numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBindAsync(
                numbers => ResultCollectionFactory.CreateTaskResultCollection(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadBindAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadBindAsync(
                errors => ResultCollectionFactory.CreateTaskResultCollection(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadBindAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadBindAsync(
                errors => ResultCollectionFactory.CreateTaskResultCollection(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkBindAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkBindAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkBindAsync_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadBindAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);
            var resultError =  new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultCollectionBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadBindAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var resultValue = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);
            var resultError = new ResultError(initialErrorToAdd);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultCollectionBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
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