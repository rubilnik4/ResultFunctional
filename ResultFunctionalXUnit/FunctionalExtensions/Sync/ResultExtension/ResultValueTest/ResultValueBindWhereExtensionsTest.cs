using System;
using System.Linq;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class ResultValueBindWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindContinue_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBindContinue(_ => true,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindContinue_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultValue.ResultValueBindContinue(_ => false,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueBindContinue(_ => true,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueBindContinue(_ => false,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindWhere_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBindWhere(_ => true,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => new ResultValue<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindWhere_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultValue.ResultValueBindWhere(_ => false,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => new ResultValue<string>(errorBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindWhere_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueBindWhere(_ => true,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => new ResultValue<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindWhere_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueBindWhere(_ => false,
                okFunc: number => new ResultValue<string>(number.ToString()),
                badFunc: _ => new ResultValue<string>(CreateErrorListTwoTest()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void ResultValueBindOkBad_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBindOkBad(okFunc: number => new ResultValue<string>(number.ToString()),
                                                                    badFunc: _ => new ResultValue<string>(String.Empty));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void ResultValueBindOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueBindOkBad(okFunc: _ => new ResultValue<string>(String.Empty),
                                                                    badFunc: errors => new ResultValue<string>(errors.Count.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }
        
        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBindOk_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBindOk(number => new ResultValue<string>(number.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBindOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueBindOk(number => new ResultValue<string>(number.ToString()));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBindBad_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBindBad(errors => new ResultValue<int>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(resultValue.Value, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueBindBad(errors => new ResultValue<int>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueTypeBindBad_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueTypeBindBad(errors => new ResultValueType<int, IValueNotFoundErrorResult>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.IsAssignableFrom<IResultValueType<int, IValueNotFoundErrorResult>>(resultAfterWhere);
            Assert.Equal(resultValue.Value, resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueTypeBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueTypeBindBad(errors => new ResultValueType<int, IValueNotFoundErrorResult>(errors.Count));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.IsAssignableFrom<IResultValueType<int, IValueNotFoundErrorResult>>(resultAfterWhere);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением. Без ошибок
        /// </summary>
        [Fact]
        public void ResultValueBindErrorsOk_NoError()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = resultValue.ResultValueBindErrorsOk(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением. С ошибками
        /// </summary>
        [Fact]
        public void ResultValueBindErrorsOk_HasError()
        {
            int initialValue = Numbers.Number;
            var initialError = CreateErrorTest();
            var resultValue = new ResultValue<int>(initialValue);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = resultValue.ResultValueBindErrorsOk(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(initialError.Equals(resultAfterWhere.Errors.First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public void ResultValueBindErrorsBad_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);
            var resultError = new ResultError();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = resultValue.ResultValueBindErrorsOk(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.Errors));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой. С ошибками
        /// </summary>
        [Fact]
        public void ResultValueBindErrorsBad_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialError = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorsInitial);
            var resultError = new ResultError(initialError);
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = resultValue.ResultValueBindErrorsOk(number => resultFunctionsMock.Object.NumberToResult(number));

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