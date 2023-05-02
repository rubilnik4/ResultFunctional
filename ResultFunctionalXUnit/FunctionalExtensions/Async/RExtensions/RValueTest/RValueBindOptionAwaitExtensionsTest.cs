using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Maybe;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением для задачи-объекта. Тесты
    /// </summary>
    public class RValueBindOptionAwaitExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task RValueBindContinueBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindOptionAwait(_ => true,
                number => RValueFactory.SomeTask(number.ToString()),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task RValueBindContinueBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await rValue.RValueBindOptionAwait(_ => false,
                _ => RValueFactory.SomeTask(String.Empty),
                _ => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindOptionAwait(_ => true,
                _ => RValueFactory.SomeTask(String.Empty),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindContinueBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindOptionAwait(_ => false,
                _ => RValueFactory.SomeTask(String.Empty),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task RValueBindWhereBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindWhereAwait(_ => true,
                number => RValueFactory.SomeTask(number.ToString()),
                _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task RValueBindWhereBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await rValue.RValueBindWhereAwait(_ => false,
                _ => RValueFactory.SomeTask(String.Empty),
                _ => RValueFactory.NoneTask<string>(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindWhereBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindWhereAwait(_ => true,
                _ => RValueFactory.SomeTask(String.Empty),
                _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindWhereBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindWhereAwait(_ => false,
                _ => RValueFactory.SomeTask(String.Empty),
                _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в асинхронном результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RValueBindOkBadBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindMatchAwait(
                number => RValueFactory.SomeTask(number.ToString()),
                _ => RValueFactory.SomeTask(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в асинхронном результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task RValueBindOkBadBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValue.RValueBindMatchAwait(
                _ => RValueFactory.SomeTask(String.Empty),
                errors => RValueFactory.SomeTask(errors.Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RValueBindOkBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindSomeAwait(
                number => RValueFactory.SomeTask(number.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RValueBindOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueBindSomeAwait(
                number => RValueFactory.SomeTask(number.ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RValueBindBadBindAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueBindNoneAwait(
                errors => RValueFactory.SomeTask(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task RValueBindBadBindAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValue.RValueBindNoneAwait(
                errors => RValueFactory.SomeTask(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task RValueBindErrorsOkBindAsync_NoError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);
            var rMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RValueBindEnsureAwait(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task RValueBindErrorsOkBindAsync_HasError()
        {
            int initialValue = Numbers.Number;
            var initialError = CreateErrorTest();
            var rValue = RValueFactory.SomeTask(initialValue);
            var rMaybe = RUnitFactory.None(initialError);
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RValueBindEnsureAwait(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task RValueBindErrorsBadBindAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);
            var rMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RValueBindEnsureAwait(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task RValueBindErrorsBadBindAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);
            var rMaybe = RUnitFactory.None(initialErrorToAdd);
            var resultFunctionsMock = GetNumberToError(rMaybe);

            var resultAfterWhere = await rValue.RValueBindEnsureAwait(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IRMaybe rMaybe) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>())).
                              ReturnsAsync(rMaybe));
    }
}