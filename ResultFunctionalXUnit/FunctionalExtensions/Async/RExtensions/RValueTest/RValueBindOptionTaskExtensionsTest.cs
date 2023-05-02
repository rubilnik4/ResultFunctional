using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Maybe;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением для задачи-объекта. Тесты
    /// </summary>
    public class RValueBindOptionTaskExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task RValueBindContinueTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindOptionTask(_ => true,
                number => number.ToString().ToRValue(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task RValueBindContinueTaskAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await rValue.RValueBindOptionTask(_ => false,
                _ => String.Empty.ToRValue(),
                _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindOptionTask(_ => true,
                _ => String.Empty.ToRValue(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindOptionTask(_ => false,
                _ => String.Empty.ToRValue(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task RValueBindWhereTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindWhereTask(_ => true,
                number => number.ToString().ToRValue(),
                _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task RValueBindWhereTaskAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await rValue.RValueBindWhereTask(_ => false,
                _ => String.Empty.ToRValue(),
                _ => errorsBad.ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindWhereTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindWhereTask(_ => true,
                _ => String.Empty.ToRValue(),
                _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindWhereTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindWhereTask(_ => false,
                _ => String.Empty.ToRValue(),
                _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в асинхронном результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RValueBindOkBadTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindMatchTask(
                number => number.ToString().ToRValue(),
                _ => String.Empty.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в асинхронном результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task RValueBindOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValue.RValueBindMatchTask(
                _ => String.Empty.ToRValue(),
                errors => errors.Count.ToString().ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RValueBindOkTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindSomeTask(number => number.ToString().ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RValueBindOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindSomeTask(number => number.ToString().ToRValue());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RValueBindBadTaskAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindNoneTask(errors => errors.Count.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RValueBindBadTaskAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValue.RValueBindNoneTask(errors => errors.Count.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task RValueBindErrorsOkTaskAsync_NoError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);
            var rMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RValueBindEnsureTask(
                number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task RValueBindErrorsOkTaskAsync_HasError()
        {
            int initialValue = Numbers.Number;
            var initialError = CreateErrorTest();
            var rValue = RValueFactory.SomeTask(initialValue);
            var rMaybe = RUnitFactory.None(initialError);
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RValueBindEnsureTask(
                number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task RValueBindErrorsBadTaskAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);
            var rMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RValueBindEnsureTask(
                number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task RValueBindErrorsBadTaskAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);
            var rMaybe = RUnitFactory.None(initialErrorToAdd);
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere =
                await rValue.RValueBindEnsureTask(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IRMaybe rMaybe) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>())).
                              Returns(rMaybe));
    }
}