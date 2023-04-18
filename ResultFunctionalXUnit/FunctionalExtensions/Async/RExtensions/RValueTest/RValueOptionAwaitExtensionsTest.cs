using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа со значением задачей-объектом. Тесты
    /// </summary>
    public class RValueOptionAwaitExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе задачей-объектом
        /// </summary>
        [Fact]
        public async Task RValueContinueBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueOptionAwait(_ => true,
                AsyncFunctions.IntToStringAsync,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном асинхронном результирующем ответе задачи-объекта без ошибки
        /// </summary>
        [Fact]
        public async Task RValueContinueBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorListTwoTestTask();
            var resultAfterWhere = await rValueTask.RValueOptionAwait(_ => false,
                _ => Task.FromResult(String.Empty),
                _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.Result.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueOptionAwait(_ => true,
                _ => Task.FromResult(String.Empty),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueContinueBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueOptionAwait(_ => false,
                _ => Task.FromResult(String.Empty),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueWhereAwait(_ => true,
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var valueBad = CreateErrorListTwoTest().Count.ToString();
            var resultAfterWhere = await rValue.RValueWhereAwait(_ => false,
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(valueBad));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(valueBad, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueWhereAwait(_ => true,
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereBindAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValue.RValueWhereAwait(_ => false,
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(CreateErrorListTwoTest().Count.ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RValueOkBadBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueMatchAwait(
                AsyncFunctions.IntToStringAsync,
                _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>      
        [Fact]
        public async Task RValueOkBadBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValueTask.RValueMatchAwait(
                _ => Task.FromResult(String.Empty),
                errors => Task.FromResult(errors.Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе задачи-объекта без ошибки
        /// </summary>   
        [Fact]
        public async Task RValueOkBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueSomeAwait(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>   
        [Fact]
        public async Task RValueOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueSomeAwait(AsyncFunctions.IntToStringAsync);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в асинхронном результирующем ответе задачи-объекта без ошибки
        /// </summary>   
        [Fact]
        public async Task RValueBadBindAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueNoneAwait(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>   
        [Fact]
        public async Task RValueBadBindAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValueTask.RValueNoneAwait(errors => Task.FromResult(errors.Count));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе задачей-объектом
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkBindAsync_Ok_CheckNoError()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueEnsureAwait(_ => true,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном асинхронном результирующем ответе задачи-объекта без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkBindAsync_Ok_CheckHasError()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorListTwoTestTask();
            var resultAfterWhere = await rValueTask.RValueEnsureAwait(_ => false,
                _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.Result.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkBindAsync_Bad_CheckNoError()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueEnsureAwait(_ => true,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkBindAsync_Bad_CheckHasError()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueEnsureAwait(_ => false,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}