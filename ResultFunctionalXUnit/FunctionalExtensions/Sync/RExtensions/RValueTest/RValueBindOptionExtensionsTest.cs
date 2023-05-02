using System;
using System.Linq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Maybe;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением. Тесты
    /// </summary>
    public class RValueBindOptionExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void RValueBindContinue_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueBindOption(_ => true,
                number => number.ToString().ToRValue(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public void RValueBindContinue_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rValue.RValueBindOption(_ => false,
                number => number.ToString().ToRValue(),
                _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RValueBindContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueBindOption(_ => true,
                number => number.ToString().ToRValue(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RValueBindContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueBindOption(_ => false,
                number => number.ToString().ToRValue(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void RValueBindWhere_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueBindWhere(_ => true,
                number => number.ToString().ToRValue(),
                _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public void RValueBindWhere_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rValue.RValueBindWhere(_ => false,
                number => number.ToString().ToRValue(),
                _ => errorBad.ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RValueBindWhere_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueBindWhere(_ => true,
                number => number.ToString().ToRValue(),
                _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RValueBindWhere_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueBindWhere(_ => false,
                number => number.ToString().ToRValue(),
                _ => CreateErrorListTwoTest().ToRValue<string>());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия со связыванием в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void RValueBindOkBad_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueBindMatch(number => number.ToString().ToRValue(),
                                                                    _ => String.Empty.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия со связыванием в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void RValueBindOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueBindMatch(_ => String.Empty.ToRValue(),
                                                                    errors => errors.Count.ToString().ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void RValueBindOk_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueBindSome(number => number.ToString().ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void RValueBindOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueBindSome(number => number.ToString().ToRValue());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void RValueBindBad_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueBindNone(errors => errors.Count.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(rValue.GetValue(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия результирующего ответа со связыванием в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void RValueBindBad_Bad_ReturnNewValue()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueBindNone(errors => errors.Count.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением. Без ошибок
        /// </summary>
        [Fact]
        public void RValueBindErrorsOk_NoError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();
            var RMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(RMaybe);

            var resultAfterWhere = rValue.RValueBindEnsure(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением. С ошибками
        /// </summary>
        [Fact]
        public void RValueBindErrorsOk_HasError()
        {
            int initialValue = Numbers.Number;
            var initialError = CreateErrorTest();
            var rValue = initialValue.ToRValue();
            var RMaybe = initialError.ToRUnit();
            var resultFunctionsMock = GetNumberToError(RMaybe);

            var resultAfterWhere = rValue.RValueBindEnsure(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(initialError.Equals(resultAfterWhere.GetErrors().First()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(initialValue), Times.Once);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой. Без ошибок
        /// </summary>
        [Fact]
        public void RValueBindErrorsBad_NoError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();
            var RMaybe = RUnitFactory.Some();
            var resultFunctionsMock = GetNumberToError(RMaybe);

            var resultAfterWhere = rValue.RValueBindEnsure(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Добавить ошибки результирующего ответа со значением и ошибкой. С ошибками
        /// </summary>
        [Fact]
        public void RValueBindErrorsBad_HasError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var initialError = CreateErrorTest();
            var rValue = errorsInitial.ToRValue<int>();
            var RMaybe = initialError.ToRValue();
            var resultFunctionsMock = GetNumberToError(RMaybe);

            var resultAfterWhere = rValue.RValueBindEnsure(number => resultFunctionsMock.Object.NumberToResult(number));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsInitial.SequenceEqual(resultAfterWhere.GetErrors()));
            resultFunctionsMock.Verify(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Получить функцию с результирующим ответом
        /// </summary>
        private static Mock<IResultFunctions> GetNumberToError(IRMaybe RMaybe) =>
            new Mock<IResultFunctions>().
            Void(mock => mock.Setup(resultFunctions => resultFunctions.NumberToResult(It.IsAny<int>())).
                              Returns(RMaybe));
    }
}