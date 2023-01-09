using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа с значением с возвращением к коллекции. Тесты
    /// </summary>
    public class ResultValueWhereToCollectionBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task ResultValueContinueToCollectionBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValue.ResultValueContinueToCollectionBindAsync(_ => true,
                okFunc: NumberToCollectionAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await NumberToCollectionAsync(initialValue)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение асинхронного негативного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task ResultValueContinueToCollectionBindAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var errorsBad = CreateErrorListTwoTestTask();
            var resultAfterWhere = await resultValue.ResultValueContinueToCollectionBindAsync(_ => false,
                okFunc: NumberToCollectionAsync,
                badFunc: _ => errorsBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Result.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task ResultValueContinueToCollectionBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueContinueToCollectionBindAsync(_ => true,
                okFunc: NumberToCollectionAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в отрицательном результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task ResultValueContinueToCollectionBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueContinueToCollectionBindAsync(_ => false,
                okFunc: NumberToCollectionAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadToCollectionBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValue.ResultValueOkBadToCollectionBindAsync(
                okFunc: NumberToCollectionAsync,
                badFunc: _ => Task.FromResult((IReadOnlyCollection<int>)new List<int>()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await NumberToCollectionAsync(initialValue)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение асинхронного негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadToCollectionBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultValueOkBadToCollectionBindAsync(
                okFunc: NumberToCollectionAsync,
                badFunc: errors => Task.FromResult((IReadOnlyCollection<int>)new List<int> { errors.Count }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueOkToCollectionBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValue.ResultValueOkToCollectionBindAsync(NumberToCollectionAsync);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await NumberToCollectionAsync(initialValue)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueOkToCollectionBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueOkToCollectionBindAsync(NumberToCollectionAsync);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }
    }
}