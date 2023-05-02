using System.Collections.Generic;
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
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class RListBindOptionAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListBindContinueAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = await rList.RListBindOptionAsync(_ => true,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListBindContinueAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListBindOptionAsync(_ => false,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListBindOptionAsync(_ => true,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindContinueAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListBindOptionAsync(_ => false,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListBindWhereAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = await rList.RListBindWhereAsync(_ => true,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListBindWhereAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListBindWhereAsync(_ => false,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => RListFactory.NoneTask<string>(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindWhereAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListBindWhereAsync(_ => true,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListBindWhereAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListBindWhereAsync(_ => false,
                numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RListBindOkBadAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = await rList.RListBindMatchAsync(numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                                                                                   errors => RListFactory.SomeTask(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task RListBindOkBadAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListBindMatchAsync(numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                                                                              errors => RListFactory.SomeTask(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task RListBindOkAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var rList = numberCollection.ToRList();

            var resultAfterWhere = await rList.RListBindSomeAsync(
                numbers => RListFactory.SomeTask(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task RListBindOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListBindSomeAsync(
                numbers => RListFactory.SomeTask(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task RListBindBadAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var rList = numberCollection.ToRList();

            var resultAfterWhere = await rList.RListBindNoneAsync(
                errors => RListFactory.SomeTask(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task RListBindBadAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListBindNoneAsync(
                errors => RListFactory.SomeTask(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public async Task RListBindErrorsOkAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();
            var rMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rList.RListBindEnsureAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public async Task RListBindErrorsOkAsync_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var rValue = initialCollection.ToRList();
            var rMaybe = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RListBindEnsureAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public async Task RListBindErrorsBadAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRList<int>();
            var rMaybe =RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RListBindEnsureAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой. С ошибками
        /// </summary>
        [Fact]
        public async Task RListBindErrorsBadAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialError = CreateErrorTest();
            var rValue = errorsInitial.ToRList<int>();
            var rMaybe = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RListBindEnsureAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IRMaybe irMaybe) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                         ReturnsAsync(irMaybe));
    }
}