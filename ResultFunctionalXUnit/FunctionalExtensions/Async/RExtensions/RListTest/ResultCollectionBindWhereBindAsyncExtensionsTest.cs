using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Maybe;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа с коллекцией для задачи-объекта. Тесты
    /// </summary>
    public class RListBindWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListBindContinueBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var RList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await RList.RListBindOptionAwait(_ => true,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListBindContinueBindAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var RList = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await RList.RListBindOptionAwait(_ => false,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var RList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await RList.RListBindOptionAwait(_ => true,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindContinueBindAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var RList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await RList.RListBindOptionAwait(_ => false,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListBindWhereBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var RList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await RList.RListBindOptionWhereAwait(_ => true,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListBindWhereBindAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var RList = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await RList.RListBindOptionWhereAwait(_ => false,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => RListFactory.NoneTask<string>(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindWhereBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var RList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await RList.RListBindOptionWhereAwait(_ => true,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindWhereBindAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var RList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await RList.RListBindOptionWhereAwait(_ => false,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RListBindOkBadBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var RList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await RList.RListBindMatchAwait(numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                                                                                   errors => RListFactory.SomeTask(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task RListBindOkBadBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await RList.RListBindMatchAwait(numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                                                                              errors => RListFactory.SomeTask(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RListBindOkBindAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var RList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await RList.RListBindSomeAwait(
                numbers => RListFactory.SomeTask(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RListBindOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var RList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await RList.RListBindSomeAwait(
                numbers => RListFactory.SomeTask(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RListBindBadBindAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var RList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await RList.RListBindNoneAwait(
                errors => RListFactory.SomeTask(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RListBindBadBindAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await RList.RListBindNoneAwait(
                errors => RListFactory.SomeTask(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task RListBindErrorsOkBindAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var RList = RListFactory.SomeTask(initialCollection);
            var RMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(RMaybe);

            var resultAfterWhere = await RList.RListBindEnsureAwait(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task RListBindErrorsOkBindAsync_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var RList = RListFactory.SomeTask(initialCollection);
            var RMaybe = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(RMaybe);

            var resultAfterWhere = await RList.RListBindEnsureAwait(
                number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task RListBindErrorsBadBindAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RValue = RListFactory.NoneTask<int>(errorsInitial);
            var RMaybe =  RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(RMaybe);

            var resultAfterWhere = await RValue.RListBindEnsureAwait(
                number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task RListBindErrorsBadBindAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var RValue = RListFactory.NoneTask<int>(errorsInitial);
            var RMaybe = initialErrorToAdd.ToRUnit();
            var resultFunctionsMock = GetNumberToError(RMaybe);

            var resultAfterWhere = await RValue.RListBindEnsureAwait(
                number => resultFunctionsMock.Object.NumbersToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IRMaybe RMaybe) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                         ReturnsAsync(RMaybe));
    }
}