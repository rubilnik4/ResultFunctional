using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением задачей-объектом. Тесты
    /// </summary>
    public class RValueOptionTaskExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе задачей-объектом
        /// </summary>
        [Fact]
        public async Task RValueContinueTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueOptionTask(_ => true,
                number => number.ToString(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе задачи-объекта без ошибки
        /// </summary>
        [Fact]
        public async Task RValueContinueTaskAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await rValueTask.RValueOptionTask(_ => false,
                _ => Task.FromResult(String.Empty),
                _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueOptionTask(_ => true,
                _ => Task.FromResult(String.Empty),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueOptionTask(_ => false,
                _ => Task.FromResult(String.Empty),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueWhereTask(_ => true,
                number => number.ToString(),
                _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereTaskAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var valueBad = CreateErrorListTwoTest().Count.ToString();
            var resultAfterWhere = await rValue.RValueWhereTask(_ => false,
                number => number.ToString(),
                _ => valueBad);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(valueBad, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueWhereTask(_ => true,
                number => number.ToString(),
                _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RValueWhereTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValue.RValueWhereTask(_ => false,
                number => number.ToString(),
                _ => CreateErrorListTwoTest().Count.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RValueOkBadTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueMatchTask(
                number => number.ToString(),
                _ => String.Empty);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе задачи-объекта с ошибкой
        /// </summary>      
        [Fact]
        public async Task RValueOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValueTask.RValueMatchTask(
                _ => String.Empty,
                errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта без ошибки
        /// </summary>   
        [Fact]
        public async Task RValueOkTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueSomeTask(number => number.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с ошибкой
        /// </summary>   
        [Fact]
        public async Task RValueOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueSomeTask(number => number.ToString());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе задачи-объекта без ошибки
        /// </summary>   
        [Fact]
        public async Task RValueBadTaskAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueNoneTask(errors => errors.Count);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с ошибкой
        /// </summary>   
        [Fact]
        public async Task RValueBadTaskAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValueTask.RValueNoneTask(errors => errors.Count);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе задачей-объектом
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkTaskAsync_Ok_CheckNoError()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueEnsureTask(_ => true,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе задачи-объекта без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkTaskAsync_Ok_CheckHasError()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rValueTask.RValueEnsureTask(_ => false,
                _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.GetErrors()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkTaskAsync_Bad_CheckNoError()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueEnsureTask(_ => true,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCheckErrorsOkTaskAsync_Bad_CheckHasError()
        {
            var errorInitial = CreateErrorTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValueTask.RValueEnsureTask(_ => false,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}