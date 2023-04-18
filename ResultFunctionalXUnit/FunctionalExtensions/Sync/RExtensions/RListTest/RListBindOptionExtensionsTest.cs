using System.Collections.Generic;
using System.Linq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Maybe;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа с коллекцией. Тесты
    /// </summary>
    public class RListBindOptionExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией со связыванием
        /// </summary>
        [Fact]
        public void RListBindContinue_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListBindOption(_ => true,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки со связыванием
        /// </summary>
        [Fact]
        public void RListBindContinue_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rList.RListBindOption(_ => false,
                numbers => CollectionToString(numbers).ToRList(),
                _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RListBindContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = rList.RListBindOption(_ => true,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RListBindContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListBindOption(_ => false,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией со связыванием
        /// </summary>
        [Fact]
        public void RListBindWhere_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListBindWhere(_ => true,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки со связыванием
        /// </summary>
        [Fact]
        public void RListBindWhere_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rList.RListBindWhere(_ => false,
                numbers => CollectionToString(numbers).ToRList(),
                _ => errorBad.ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RListBindWhere_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = rList.RListBindWhere(_ => true,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RListBindWhere_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListBindWhere(_ => false,
                numbers => CollectionToString(numbers).ToRList(),
                _ => CreateErrorListTwoTest().ToRList<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void RListBindOkBad_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListBindMatch(numbers => CollectionToString(numbers).ToRList(),
                                                                              errors => GetListByErrorsCountString(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void RListBindOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListBindMatch(numbers => CollectionToString(numbers).ToRList(),
                                                                              errors => GetListByErrorsCountString(errors).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(GetListByErrorsCountString(errorsInitial).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void RListBindOk_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListBindSome(numbers => CollectionToString(numbers).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void RListBindOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = rList.RListBindSome(numbers => CollectionToString(numbers).ToRList());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void RListBindBad_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListBindNone(GetRListByErrorCount);

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void RListBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListBindNone(GetRListByErrorCount);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public void RListBindErrorsOk_NoError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();
            var RMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumbersToError(RMaybe);

            var resultAfterWhere = rList.RListBindEnsure(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(initialCollection), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public void RListBindErrorsOk_HasError()
        {
            var initialCollection = GetRangeNumber();
            var initialError = CreateErrorTest();
            var rList = initialCollection.ToRList();
            var RMaybe = initialError.ToRUnit();
            var resultFunctionsMock = GetNumbersToError(RMaybe);

            var resultAfterWhere = rList.RListBindEnsure(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(initialCollection), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. Без ошибок
        /// </summary>
        [Fact]
        public void RListBindErrorsBad_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();
            var RMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumbersToError(RMaybe);

            var resultAfterWhere = rList.RListBindEnsure(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа с коллекцией. С ошибками
        /// </summary>
        [Fact]
        public void RListBindErrorsBad_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialError = CreateErrorTest();
            var rList = errorsInitial.ToRList<int>();
            var RMaybe = initialError.ToRUnit();
            var resultFunctionsMock = GetNumbersToError(RMaybe);

            var resultAfterWhere = rList.RListBindEnsure(numbers => resultFunctionsMock.Object.NumbersToResult(numbers));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumbersToError(IRMaybe RMaybe) =>
            new Mock<IResultFunctions>().
                Void(mock => mock.Setup(resultFunctions => resultFunctions.NumbersToResult(It.IsAny<IReadOnlyCollection<int>>())).
                                  Returns(RMaybe));

        /// <summary>
        /// Получить результирующую коллекцию по количеству ошибок
        /// </summary>
        private static IRList<int> GetRListByErrorCount(IReadOnlyCollection<IRError> errors) =>
           GetListByErrorsCount(errors).ToRList();
    }
}