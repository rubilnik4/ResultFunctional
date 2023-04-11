using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Lists;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Options;

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
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
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
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
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
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
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
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
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
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
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
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
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
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
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
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionBindOkBadTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBadTaskAsync(
                numbers => CollectionToString(numbers).ToRList(),
                errors => GetListByErrorsCountString(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionBindOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBadTaskAsync(
                numbers => CollectionToString(numbers).ToRList(),
                errors => GetListByErrorsCountString(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.GetValue()));
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
                numbers => CollectionToString(numbers).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.GetValue());
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
                numbers=> CollectionToString(numbers).ToRList());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
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
                errors => GetListByErrorsCount(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.GetValue()));
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
                errors => GetListByErrorsCount(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkTaskAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkTaskAsync(
                numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
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
            var resultError = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkTaskAsync(
                number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
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
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere =
                await resultValue.ResultCollectionBindErrorsOkTaskAsync(
                    number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
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
            var resultError = initialErrorToAdd.ToRUnit();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultCollectionBindErrorsOkTaskAsync(
                number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IROption resultError) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
            Returns(resultError));
    }
}