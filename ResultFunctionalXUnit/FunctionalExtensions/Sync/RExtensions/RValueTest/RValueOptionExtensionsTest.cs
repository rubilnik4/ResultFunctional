using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением. Тесты
    /// </summary>
    public class RValueOptionExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public void RValueContinue_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueOption(_ => true,
                                                                   number => number.ToString(),
                                                                   _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public void RValueContinue_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rValue.RValueOption(_ => false,
                                                                   _ => String.Empty,
                                                                   _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void RValueContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueOption(_ => true,
                                                                   _ => String.Empty,
                                                                   _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void RValueContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueOption(_ => false,
                                                                   _ => String.Empty,
                                                                   _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void RValueWhere_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueWhere(_ => true,
                number => number.ToString(),
                _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public void RValueWhere_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var valueBad = CreateErrorListTwoTest().Count.ToString();
            var resultAfterWhere = rValue.RValueWhere(_ => false,
                number => number.ToString(),
                _ => valueBad);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(valueBad, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RValueWhere_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueWhere(_ => true,
                number => number.ToString(),
                _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RValueWhere_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueWhere(_ => false,
                number => number.ToString(),
                _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void RValueOkBad_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueMatch(number => number.ToString(),
                                                                _ => String.Empty);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void RValueOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueMatch(_ => String.Empty,
                                                                errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void RValueOk_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueSome(number => number.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void RValueOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueSome(number => number.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void RValueBad_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueNone(errors => errors.Count);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(rValue.GetValue(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void RValueBad_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueNone(errors => errors.Count);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public void RValueCheckErrorsOk_Ok_CheckNoError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueEnsure(_ => true,
                                                                        _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public void RValueCheckErrorsOk_Ok_CheckHasError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rValue.RValueEnsure(_ => false,
                                                                        _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void RValueCheckErrorsOk_Bad_CheckNoError()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueEnsure(_ => true,
                                                                        _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void RValueCheckErrorsOk_Bad_CheckHasError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueEnsure(_ => false,
                                                                        _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}