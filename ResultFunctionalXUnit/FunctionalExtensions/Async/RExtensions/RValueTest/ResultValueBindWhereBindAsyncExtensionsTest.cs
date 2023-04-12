using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Options;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionBindWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindContinueBindAsync(_ => true,
                okFunc: number => RValueFactory.SomeTask(number.ToString()),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.ResultValueBindContinueBindAsync(_ => false,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindContinueBindAsync(_ => true,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindContinueBindAsync(_ => false,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindWhereBindAsync(_ => true,
                okFunc: number => RValueFactory.SomeTask(number.ToString()),
                badFunc: _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.ResultValueBindWhereBindAsync(_ => false,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => RValueFactory.NoneTask<string>(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindWhereBindAsync(_ => true,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindWhereBindAsync(_ => false,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в асинхронном результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueBindOkBadBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkBadBindAsync(
                okFunc: number => RValueFactory.SomeTask(number.ToString()),
                badFunc: _ => RValueFactory.SomeTask(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в асинхронном результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueBindOkBadBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkBadBindAsync(
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: errors => RValueFactory.SomeTask(errors.Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkBindAsync(
                number => RValueFactory.SomeTask(number.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkBindAsync(
                number => RValueFactory.SomeTask(number.ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadBindAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindBadBindAsync(
                errors => RValueFactory.SomeTask(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadBindAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBindBadBindAsync(
                errors => RValueFactory.SomeTask(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkBindAsync_NoError()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkBindAsync_HasError()
        {
            int initialValue = Numbers.Number;
            var initialError = CreateErrorTest();
            var resultValue = RValueFactory.SomeTask(initialValue);
            var resultError = RUnitFactory.None(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadBindAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = RValueFactory.NoneTask<int>(errorsInitial);
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadBindAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorsInitial);
            var resultError = RUnitFactory.None(initialErrorToAdd);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IROption resultError) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>())).
                              ReturnsAsync(resultError));
    }
}