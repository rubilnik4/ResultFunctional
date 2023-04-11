using System;
using System.Linq;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Options;
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
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueBindContinue(_ => true,
                okFunc: number => number.ToString().ToRValue(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindContinue_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultValue.ResultValueBindContinue(_ => false,
                okFunc: number => number.ToString().ToRValue(),
                badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueBindContinue(_ => true,
                okFunc: number => number.ToString().ToRValue(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueBindContinue(_ => false,
                okFunc: number => number.ToString().ToRValue(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindWhere_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueBindWhere(_ => true,
                okFunc: number => number.ToString().ToRValue(),
                badFunc: _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindWhere_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultValue.ResultValueBindWhere(_ => false,
                okFunc: number => number.ToString().ToRValue(),
                badFunc: _ => errorBad.ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindWhere_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueBindWhere(_ => true,
                okFunc: number => number.ToString().ToRValue(),
                badFunc: _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueBindWhere_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueBindWhere(_ => false,
                okFunc: number => number.ToString().ToRValue(),
                badFunc: _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void ResultValueBindOkBad_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueBindOkBad(number => number.ToString().ToRValue(),
                                                                    _ => String.Empty.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void ResultValueBindOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueBindOkBad(okFunc: _ => String.Empty.ToRValue(),
                                                                    badFunc: errors => errors.Count.ToString().ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBindOk_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueBindOk(number => number.ToString().ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBindOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueBindOk(number => number.ToString().ToRValue());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBindBad_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueBindBad(errors => errors.Count.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(resultValue.GetValue(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueBindBad(errors => errors.Count.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением. Без ошибок
        /// </summary>
        [Fact]
        public void ResultValueBindErrorsOk_NoError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = resultValue.ResultValueBindErrorsOk(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
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
            var resultValue = initialValue.ToRValue();
            var resultError = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = resultValue.ResultValueBindErrorsOk(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public void ResultValueBindErrorsBad_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();
            var resultError = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = resultValue.ResultValueBindErrorsOk(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
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
            var resultValue = errorsInitial.ToRValue<int>();
            var resultError = initialError.ToRValue();
            var resultFunctionsMock = GetNumberToError(resultError);

            var resultAfterWhere = resultValue.ResultValueBindErrorsOk(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IROption resultError) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>())).
                              Returns(resultError));
    }
}