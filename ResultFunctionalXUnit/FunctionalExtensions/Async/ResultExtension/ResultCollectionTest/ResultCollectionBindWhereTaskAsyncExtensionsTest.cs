using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using ResultFunctional.Models.Factories;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа с коллекцией для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionBindWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueTaskAsync(_ => true,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueTaskAsync(_ => false,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueTaskAsync(_ => true,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueTaskAsync(_ => false,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereTaskAsync(_ => true,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => new ResultCollection<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereTaskAsync(_ => false,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => new ResultCollection<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereTaskAsync(_ => true,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => new ResultCollection<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereTaskAsync(_ => false,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => new ResultCollection<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionBindOkBadTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBadTaskAsync(numbers => new ResultCollection<string>(CollectionToString(numbers)),
                                                                                             errors => new ResultCollection<string>(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionBindOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBadTaskAsync(numbers => new ResultCollection<string>(CollectionToString(numbers)),
                                                                              errors => new ResultCollection<string>(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkTaskAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkTaskAsync(
                numbers => new ResultCollection<string>(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkTaskAsync(
                numbers=> new ResultCollection<string>(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadTaskAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadTaskAsync(
                errors => new ResultCollection<int>(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadTaskAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadTaskAsync(
                errors => new ResultCollection<int>(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkTaskAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkTaskAsync(
                numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkTaskAsync_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var resultCollection = RListFactory.SomeTask(initialCollection);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkTaskAsync(
                number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadTaskAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = RListFactory.NoneTask<int>(errorsInitial);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere =
                await resultValue.ResultCollectionBindErrorsOkTaskAsync(
                    number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadTaskAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var resultValue = RListFactory.NoneTask<int>(errorsInitial);
            var resultError = new ResultError(initialErrorToAdd);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultCollectionBindErrorsOkTaskAsync(
                number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IResultError resultError) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
            Returns(resultError));
    }
}