using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionBindWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindContinue_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionBindContinue(_ => true,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindContinue_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultCollection.ResultCollectionBindContinue(_ => false,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindContinue(_ => true,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindContinue(_ => false,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindWhere_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionBindWhere(_ => true,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => new ResultCollection<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindWhere_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultCollection.ResultCollectionBindWhere(_ => false,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => new ResultCollection<string>(errorBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindWhere_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindWhere(_ => true,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => new ResultCollection<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindWhere_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindWhere(_ => false,
                okFunc: numbers => new ResultCollection<string>(CollectionToString(numbers)),
                badFunc: _ => new ResultCollection<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void ResultCollectionBindOkBad_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionBindOkBad(numbers => new ResultCollection<string>(CollectionToString(numbers)),
                                                                              errors => new ResultCollection<string>(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void ResultCollectionBindOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindOkBad(numbers => new ResultCollection<string>(CollectionToString(numbers)),
                                                                              errors => new ResultCollection<string>(GetListByErrorsCountString(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionBindOk_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionBindOk(numbers => new ResultCollection<string>(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionBindOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindOk(numbers => new ResultCollection<string>(CollectionToString(numbers)));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionBindBad_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionBindBad(GetResultCollectionByErrorCount);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionBindBad(GetResultCollectionByErrorCount);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsOk_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumbersToError(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(initialCollection), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsOk_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(initialCollection);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumbersToError(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(initialCollection), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsBad_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumbersToError(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsBad_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialError = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumbersToError(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumbersToError(IResultError resultError) =>
            new Mock<IResultFunctions>().
                Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
                                  Returns(resultError));

        /// <summary>
        /// Получить результирующую коллекцию по количеству ошибок
        /// </summary>
        private static IResultCollection<int> GetResultCollectionByErrorCount(IReadOnlyCollection<IRError> errors) =>
            new ResultCollection<int>(GetListByErrorsCount(errors));
    }
}