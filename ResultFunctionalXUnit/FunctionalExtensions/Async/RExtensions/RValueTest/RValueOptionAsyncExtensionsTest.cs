using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
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
    public class RValueOptionAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task RValueContinueAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = await rValue.RValueOptionAsync(_ => true.ToTask(),
                                                                  AsyncFunctions.IntToStringAsync,
                                                                  _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task RValueContinueAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await rValue.RValueOptionAsync(_ => false.ToTask(),
                                                                              _ => Task.FromResult(String.Empty),
                                                                              _ => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueOptionAsync(_ => true.ToTask(),
                                                                              _ => Task.FromResult(String.Empty),
                                                                              _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueContinueAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueOptionAsync(_ => false.ToTask(),
                                                                              _ => Task.FromResult(String.Empty),
                                                                              _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = await rValue.RValueWhereAsync(_ => true,
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var valueBad = CreateErrorListTwoTest().Count.ToString();
            var resultAfterWhere = await rValue.RValueWhereAsync(_ => false,
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(valueBad));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(valueBad, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueWhereAsync(_ => true,
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueWhereAsync(_ => false,
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RValueOkBadAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = await rValue.RValueMatchAsync(AsyncFunctions.IntToStringAsync,
                                                                           _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task RValueOkBadAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueMatchAsync(_ => Task.FromResult(String.Empty),
                                                                           errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task RValueOkAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = await rValue.RValueSomeAsync(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task RValueOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueSomeAsync(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение асинхронного отрицательного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task RValueBadAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = await rValue.RValueNoneAsync(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(rValue.GetValue(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task RValueBadAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueNoneAsync(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в положительном результирующем ответе
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkAsync_Ok_CheckNoErrors()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = await rValue.RValueEnsureAsync(_ => true.ToTask(),
                                                                              _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkAsync_Ok_CheckHasError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await rValue.RValueEnsureAsync(_ => false.ToTask(),
                                                                              _ => Task.FromResult(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в положительном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkAsync_Bad_CheckNoErrors()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueEnsureAsync(_ => true.ToTask(),
                                                                              _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkAsync_Bad_CheckHasError()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueEnsureAsync(_ => false.ToTask(),
                                                                              _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}