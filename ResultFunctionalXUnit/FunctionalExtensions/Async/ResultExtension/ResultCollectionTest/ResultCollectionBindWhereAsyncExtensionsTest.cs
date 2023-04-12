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
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Options;

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
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueAsync(_ => true,
                okFunc: numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueAsync(_ => false,
                okFunc: numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                badFunc: _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueAsync(_ => true,
                okFunc: numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindContinueAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionBindContinueAsync(_ => false,
                okFunc: numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereAsync(_ => true,
                okFunc: numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                badFunc: _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereAsync(_ => false,
                okFunc: numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                badFunc: _ => RListFactory.NoneTask<string>(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereAsync(_ => true,
                okFunc: numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                badFunc: _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindWhereAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionBindWhereAsync(_ => false,
                okFunc: numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                badFunc: _ => RListFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionBindOkBadAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBadAsync(numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                                                                                   errors => RListFactory.SomeTask(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionBindOkBadAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkBadAsync(numbers => RListFactory.SomeTask(CollectionToString(numbers)),
                                                                              errors => RListFactory.SomeTask(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkAsync_Ok_ReturnNewValue()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = numberCollection.ToRList();

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkAsync(
                numbers => RListFactory.SomeTask(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await CollectionToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionBindOkAsync(
                numbers => RListFactory.SomeTask(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadAsync_Ok_ReturnInitial()
        {
            var numberCollection = GetRangeNumber();
            var resultCollection = numberCollection.ToRList();

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadAsync(
                errors => RListFactory.SomeTask(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.True(numberCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBindBadAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionBindBadAsync(
                errors => RListFactory.SomeTask(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsOkAsync_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultCollection.ResultCollectionBindErrorsOkAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
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
            var resultValue = initialCollection.ToRList();
            var resultError = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultCollectionBindErrorsOkAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа с коллекцией и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindErrorsBadAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRList<int>();
            var resultError =RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultCollectionBindErrorsOkAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
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
            var resultValue = errorsInitial.ToRList<int>();
            var resultError = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultCollectionBindErrorsOkAsync(
                numbers => resultFunctionsMock.Object.NumbersToResultAsync(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IROption rOption) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResultAsync(It.IsAny<IReadOnlyCollection<int>>())).
                         ReturnsAsync(rOption));
    }
}