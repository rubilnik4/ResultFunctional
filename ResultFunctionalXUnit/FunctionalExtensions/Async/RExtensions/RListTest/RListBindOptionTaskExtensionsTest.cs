﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Maybe;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа с коллекцией для задачи-объекта. Тесты
    /// </summary>
    public class RListBindOptionTaskExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListBindContinueTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListBindOptionTask(_ => true,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListBindContinueTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListBindOptionTask(_ => false,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListBindOptionTask(_ => true,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListBindOptionTask(_ => false,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListBindWhereTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListBindWhereTask(_ => true,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListBindWhereTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListBindWhereTask(_ => false,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindWhereTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListBindWhereTask(_ => true,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindWhereTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListBindWhereTask(_ => false,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RListBindOkBadTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListBindMatchTask(
                numbers => CollectionToString(numbers).ToRList(),
                errors => GetListByErrorsCountString(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task RListBindOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListBindMatchTask(
                numbers => CollectionToString(numbers).ToRList(),
                errors => GetListByErrorsCountString(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RListBindOkTaskAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await rList.RListBindSomeTask(
                numbers => CollectionToString(numbers).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RListBindOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListBindSomeTask(
                numbers=> CollectionToString(numbers).ToRList());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа с коллекцией со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RListBindBadTaskAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await rList.RListBindNoneTask(
                errors => GetListByErrorsCount(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа с коллекцией со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RListBindBadTaskAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListBindNoneTask(
                errors => GetListByErrorsCount(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task RListBindErrorsOkTaskAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);
            var rMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rList.RListBindEnsureTask(
                numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task RListBindErrorsOkTaskAsync_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var rList = RListFactory.SomeTask(initialCollection);
            var rMaybe = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rList.RListBindEnsureTask(
                number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task RListBindErrorsBadTaskAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = RListFactory.NoneTask<int>(errorsInitial);
            var rMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere =
                await rValue.RListBindEnsureTask(
                    number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task RListBindErrorsBadTaskAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var rValue = RListFactory.NoneTask<int>(errorsInitial);
            var rMaybe = initialErrorToAdd.ToRUnit();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RListBindEnsureTask(
                number => resultFunctionsMock.Object.NumbersToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IRMaybe rMaybe) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
            Returns(rMaybe));
    }
}