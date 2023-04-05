using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using ResultFunctional.Models.Factories;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionBindWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindContinueTaskAsync(_ => true,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueTaskAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.ResultValueBindContinueTaskAsync(_ => false,
                okFunc: _ => new ResultValue<string>(String.Empty),
                badFunc: _ => errorsBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.Errors));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindContinueTaskAsync(_ => true,
                okFunc: _ => new ResultValue<string>(String.Empty),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindContinueTaskAsync(_ => false,
                okFunc: _ => new ResultValue<string>(String.Empty),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindWhereTaskAsync(_ => true,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => new ResultValue<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereTaskAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.ResultValueBindWhereTaskAsync(_ => false,
                okFunc: _ => new ResultValue<string>(String.Empty),
                badFunc: _ => new ResultValue<string>(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.Errors));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindWhereTaskAsync(_ => true,
                okFunc: _ => new ResultValue<string>(String.Empty),
                badFunc: _ => new ResultValue<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindWhereTaskAsync(_ => false,
                okFunc: _ => new ResultValue<string>(String.Empty),
                badFunc: _ => new ResultValue<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в асинхронном результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueBindOkBadTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkBadTaskAsync(
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => new ResultValue<string>(String.Empty));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в асинхронном результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueBindOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkBadTaskAsync(
                okFunc: _ => new ResultValue<string>(String.Empty),
                badFunc: errors => new ResultValue<string>(errors.Count.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkTaskAsync(number => new ResultValue<string>(number.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkTaskAsync(number => new ResultValue<string>(number.ToString()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadTaskAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindBadTaskAsync(errors => new ResultValue<int>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadTaskAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBindBadTaskAsync(errors => new ResultValue<int>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkTaskAsync_NoError()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkTaskAsync(
                number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkTaskAsync_HasError()
        {
            int initialValue = Numbers.Number;
            var initialError = CreateErrorTest();
            var resultValue = RValueFactory.SomeTask(initialValue);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkTaskAsync(
                number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadTaskAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = RValueFactory.NoneTask<int>(errorsInitial);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkTaskAsync(
                number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой для задачи-объекта. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadTaskAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorsInitial);
            var resultError = new ResultError(initialErrorToAdd);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere =
                await resultValue.ResultValueBindErrorsOkTaskAsync(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IResultError resultError) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>())).
                              Returns(resultError));
    }
}