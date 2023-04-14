using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Options;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class ResultCollectionBindWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.ResultValueBindContinueAsync(_ => true,
                okFunc: number => RValueFactory.SomeTask(number.ToString()),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.ResultValueBindContinueAsync(_ => false,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.ResultValueBindContinueAsync(_ => true,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindContinueAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.ResultValueBindContinueAsync(_ => false,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueBindWhereAsync(_ => true,
                okFunc: number => RValueFactory.SomeTask(number.ToString()),
                badFunc: _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.RValueBindWhereAsync(_ => false,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => RValueFactory.NoneTask<string>(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueBindWhereAsync(_ => true,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindWhereAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueBindWhereAsync(_ => false,
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: _ => RValueFactory.NoneTask<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в асинхронном результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueBindOkBadAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueBindMatchAsync(
                okFunc: number => RValueFactory.SomeTask(number.ToString()),
                badFunc: _ => RValueFactory.SomeTask(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в асинхронном результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueBindOkBadAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueBindMatchAsync(
                okFunc: _ => RValueFactory.SomeTask(String.Empty),
                badFunc: errors => RValueFactory.SomeTask(errors.Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueBindSomeAsync(
                number => RValueFactory.SomeTask(number.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueBindSomeAsync(RValueFactory.SomeTask);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueBindNoneAsync(
                errors => RValueFactory.SomeTask(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBindBadAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueBindNoneAsync(
                errors => RValueFactory.SomeTask(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkAsync_NoError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.RValueBindEnsureAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsOkAsync_HasError()
        {
            int initialValue = Numbers.Number;
            var initialError = CreateErrorTest();
            var resultValue = initialValue.ToRValue();
            var resultError = initialError.ToRValue<int>();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.RValueBindEnsureAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadAsync_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.RValueBindEnsureAsync(
                number => resultFunctionsMock.Object.NumberToResultAsync(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResultAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа со значением и ошибкой. С ошибками
        /// </summary>
        [Fact]
        public async Task ResultValueBindErrorsBadAsync_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialErrorToAdd = CreateErrorTest();
            var resultValue = errorsInitial.ToRValue<int>();
            var resultError = initialErrorToAdd.ToRUnit();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = await resultValue.RValueBindEnsureAsync(
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