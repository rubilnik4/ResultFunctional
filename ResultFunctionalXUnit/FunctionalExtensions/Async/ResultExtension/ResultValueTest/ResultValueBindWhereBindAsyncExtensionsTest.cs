using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
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
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindContinueBindAsync(_ => true,
                okFunc: number => ResultValueFactory.CreateTaskResultValue(number.ToString()),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.ResultValueBindContinueBindAsync(_ => false,
                okFunc: _ => ResultValueFactory.CreateTaskResultValue(String.Empty),
                badFunc: _ => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.Errors));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindContinueBindAsync(_ => true,
                okFunc: _ => ResultValueFactory.CreateTaskResultValue(String.Empty),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindContinueBindAsync(_ => false,
                okFunc: _ => ResultValueFactory.CreateTaskResultValue(String.Empty),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindWhereBindAsync(_ => true,
                okFunc: number => ResultValueFactory.CreateTaskResultValue(number.ToString()),
                badFunc: _ => ResultValueFactory.CreateTaskResultValueError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.ResultValueBindWhereBindAsync(_ => false,
                okFunc: _ => ResultValueFactory.CreateTaskResultValue(String.Empty),
                badFunc: _ => ResultValueFactory.CreateTaskResultValueError<string>(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.Errors));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindWhereBindAsync(_ => true,
                okFunc: _ => ResultValueFactory.CreateTaskResultValue(String.Empty),
                badFunc: _ => ResultValueFactory.CreateTaskResultValueError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindWhereBindAsync(_ => false,
                okFunc: _ => ResultValueFactory.CreateTaskResultValue(String.Empty),
                badFunc: _ => ResultValueFactory.CreateTaskResultValueError<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в асинхронном результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueBindOkBadBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkBadBindAsync(
                okFunc: number => ResultValueFactory.CreateTaskResultValue(number.ToString()),
                badFunc: _ => ResultValueFactory.CreateTaskResultValue(String.Empty));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в асинхронном результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueBindOkBadBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkBadBindAsync(
                okFunc: _ => ResultValueFactory.CreateTaskResultValue(String.Empty),
                badFunc: errors => ResultValueFactory.CreateTaskResultValue(errors.Count.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkBindAsync(
                number => ResultValueFactory.CreateTaskResultValue(number.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkBindAsync(
                number => ResultValueFactory.CreateTaskResultValue(number.ToString()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа со связыванием в результирующем ответе без ошибки для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadBindAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindBadBindAsync(
                errors => ResultValueFactory.CreateTaskResultValue(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа со связыванием в результирующем ответе с ошибкой для задачи-объекта
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadBindAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueBindBadBindAsync(
                errors => ResultValueFactory.CreateTaskResultValue(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkBindAsync_NoError()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);
            var resultError =new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
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
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadBindAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorsInitial);
            var resultError =new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
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
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorsInitial);
            var resultError = new ResultError(initialErrorToAdd);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.ResultValueBindErrorsOkBindAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IResultError resultError) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>())).
                              ReturnsAsync(resultError));
    }
}