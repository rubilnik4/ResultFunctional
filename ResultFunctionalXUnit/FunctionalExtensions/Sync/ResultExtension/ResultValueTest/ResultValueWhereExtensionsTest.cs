using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultCollectionWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public void ResultValueContinue_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueContinue(_ => true,
                                                                   okFunc: number => number.ToString(),
                                                                   badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public void ResultValueContinue_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultValue.ResultValueContinue(_ => false,
                                                                   okFunc: _ => String.Empty,
                                                                   badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueContinue(_ => true,
                                                                   okFunc: _ => String.Empty,
                                                                   badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueContinue(_ => false,
                                                                   okFunc: _ => String.Empty,
                                                                   badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void ResultValueWhere_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueWhere(_ => true,
                okFunc: number => number.ToString(),
                badFunc: _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public void ResultValueWhere_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var valueBad = CreateErrorListTwoTest().Count.ToString();
            var resultAfterWhere = resultValue.ResultValueWhere(_ => false,
                okFunc: number => number.ToString(),
                badFunc: _ => valueBad);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(valueBad, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueWhere_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueWhere(_ => true,
                okFunc: number => number.ToString(),
                badFunc: _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueWhere_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueWhere(_ => false,
                okFunc: number => number.ToString(),
                badFunc: _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void ResultValueOkBad_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueOkBad(okFunc: number => number.ToString(),
                                                                badFunc: _ => String.Empty);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void ResultValueOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueOkBad(okFunc: _ => String.Empty,
                                                                badFunc: errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueOk_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueOk(number => number.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueOk(number => number.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBad_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueBad(errors => errors.Count);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(resultValue.GetValue(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBad_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueBad(errors => errors.Count);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public void ResultValueCheckErrorsOk_Ok_CheckNoError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueCheckErrorsOk(_ => true,
                                                                        _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCheckErrorsOk_Ok_CheckHasError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultValue.ResultValueCheckErrorsOk(_ => false,
                                                                        _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCheckErrorsOk_Bad_CheckNoError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueCheckErrorsOk(_ => true,
                                                                        _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCheckErrorsOk_Bad_CheckHasError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.ResultValueCheckErrorsOk(_ => false,
                                                                        badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}