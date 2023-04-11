using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Options;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

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
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionBindContinue(_ => true,
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindContinue_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultCollection.ResultCollectionBindContinue(_ => false,
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionBindContinue(_ => true,
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionBindContinue(_ => false,
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindWhere_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionBindWhere(_ => true,
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindWhere_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultCollection.ResultCollectionBindWhere(_ => false,
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => errorBad.ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindWhere_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionBindWhere(_ => true,
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionBindWhere_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionBindWhere(_ => false,
                okFunc: numbers => CollectionToString(numbers).ToRList(),
                badFunc: _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void ResultCollectionBindOkBad_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionBindOkBad(numbers => CollectionToString(numbers).ToRList(),
                                                                              errors => GetListByErrorsCountString(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void ResultCollectionBindOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionBindOkBad(numbers => CollectionToString(numbers).ToRList(),
                                                                              errors => GetListByErrorsCountString(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionBindOk_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionBindOk(numbers => CollectionToString(numbers).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionBindOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionBindOk(numbers => CollectionToString(numbers).ToRList());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionBindBad_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionBindBad(GetResultCollectionByErrorCount);

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionBindBad(GetResultCollectionByErrorCount);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsOk_NoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumbersToError(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
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
            var resultCollection = initialCollection.ToRList();
            var resultError = initialError.ToRUnit();
            var resultFunctionsMock = GetNumbersToError(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(initialCollection), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public void ResultCollectionBindErrorsBad_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumbersToError(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
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
            var resultCollection = errorsInitial.ToRList<int>();
            var resultError = initialError.ToRUnit();
            var resultFunctionsMock = GetNumbersToError(resultError);

            var resultAfterWhere = resultCollection.ResultCollectionBindErrorsOk(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumbersToError(IROption resultError) =>
            new Mock<IResultFunctions>().
                Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
                                  Returns(resultError));

        /// <summary>
        /// Получить результирующую коллекцию по количеству ошибок
        /// </summary>
        private static IRList<int> GetResultCollectionByErrorCount(IReadOnlyCollection<IRError> errors) =>
           GetListByErrorsCount(errors).ToRList();
    }
}