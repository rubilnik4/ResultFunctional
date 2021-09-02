using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением задачей-объектом. Тесты
    /// </summary>
    public class ResultValueWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе задачей-объектом
        /// </summary>
        [Fact]
        public async Task ResultValueContinueTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValueTask = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValueTask.ResultValueContinueTaskAsync(_ => true,
                okFunc: number => number.ToString(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение асинхронного условия в отрицательном результирующем ответе задачи-объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueContinueTaskAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValueTask = ResultValueFactory.CreateTaskResultValue(initialValue);

            var errorsBad = CreateErrorEnumerableTwoTest();
            var resultAfterWhere = await resultValueTask.ResultValueContinueTaskAsync(_ => false,
                okFunc: _ => Task.FromResult(String.Empty),
                badFunc: _ => errorsBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorsBad.SequenceEqual(resultAfterWhere.Errors));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValueTask = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValueTask.ResultValueContinueTaskAsync(_ => true,
                okFunc: _ => Task.FromResult(String.Empty),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValueTask = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValueTask.ResultValueContinueTaskAsync(_ => false,
                okFunc: _ => Task.FromResult(String.Empty),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValueTask = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValueTask.ResultValueOkBadTaskAsync(
                okFunc: number => number.ToString(),
                badFunc: _ => String.Empty);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе задачи-объекта с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValueTask = ResultValueFactory.CreateTaskResultValueError<int>(errorsInitial);

            var resultAfterWhere = await resultValueTask.ResultValueOkBadTaskAsync(
                okFunc: _ => String.Empty,
                badFunc: errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueOkTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValueTask = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValueTask.ResultValueOkTaskAsync(number => number.ToString());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValueTask = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValueTask.ResultValueOkTaskAsync(number => number.ToString());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе задачи-объекта без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBadTaskAsync_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValueTask = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValueTask.ResultValueBadTaskAsync(errors => errors.Count);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialValue, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBadTaskAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValueTask = ResultValueFactory.CreateTaskResultValueError<int>(errorsInitial);

            var resultAfterWhere = await resultValueTask.ResultValueBadTaskAsync(errors => errors.Count);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value);
        }
    }
}