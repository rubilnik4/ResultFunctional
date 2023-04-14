using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueContinueAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.ResultValueContinueAsync(_ => true,
                                                                              okFunc: AsyncFunctions.IntToStringAsync,
                                                                              badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueContinueAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.ResultValueContinueAsync(_ => false,
                                                                              okFunc: _ => Task.FromResult(String.Empty),
                                                                              badFunc: _ => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.ResultValueContinueAsync(_ => true,
                                                                              okFunc: _ => Task.FromResult(String.Empty),
                                                                              badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueContinueAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.ResultValueContinueAsync(_ => false,
                                                                              okFunc: _ => Task.FromResult(String.Empty),
                                                                              badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task ResultValueWhereAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueWhereAsync(_ => true,
                okFunc: AsyncFunctions.IntToStringAsync,
                badFunc: _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public async Task ResultValueWhereAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var valueBad = CreateErrorListTwoTest().Count.ToString();
            var resultAfterWhere = await resultValue.RValueWhereAsync(_ => false,
                okFunc: AsyncFunctions.IntToStringAsync,
                badFunc: _ => Task.FromResult(valueBad));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(valueBad, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task ResultValueWhereAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueWhereAsync(_ => true,
                okFunc: AsyncFunctions.IntToStringAsync,
                badFunc: _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task ResultValueWhereAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueWhereAsync(_ => false,
                okFunc: AsyncFunctions.IntToStringAsync,
                badFunc: _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueMatchAsync(okFunc: AsyncFunctions.IntToStringAsync,
                                                                           badFunc: _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueMatchAsync(okFunc: _ => Task.FromResult(String.Empty),
                                                                           badFunc: errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueOkAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueSomeAsync(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueSomeAsync(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение асинхронного отрицательного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBadAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueNoneAsync(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(resultValue.GetValue(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBadAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueNoneAsync(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task ResultValueCheckErrorsOkAsync_Ok_CheckNoErrors()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.RValueEnsureAsync(_ => true,
                                                                              badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCheckErrorsOkAsync_Ok_CheckHasError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValue.RValueEnsureAsync(_ => false,
                                                                              badFunc: _ => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCheckErrorsOkAsync_Bad_CheckNoErrors()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueEnsureAsync(_ => true,
                                                                              badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCheckErrorsOkAsync_Bad_CheckHasError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.RValueEnsureAsync(_ => false,
                                                                              badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}