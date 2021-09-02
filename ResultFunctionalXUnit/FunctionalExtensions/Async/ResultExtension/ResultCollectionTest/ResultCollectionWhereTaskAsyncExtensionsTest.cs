using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.ResultFactory;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе задачи-объекта с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueTaskAsync(_ => true,
                okFunc: CollectionToString,
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionContinueTaskAsync(_ => false,
                okFunc: _ => GetEmptyStringList(),
                badFunc: _ => errorsBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueTaskAsync(_ => true,
                okFunc: _ => GetEmptyStringList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueTaskAsync(_ => false,
                okFunc: _ => GetEmptyStringList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в асинхронном положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereTaskAsync(_ => true,
                okFunc: CollectionToString,
                badFunc: _ => GetEmptyStringList());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в асинхронном отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereTaskAsync_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereTaskAsync(_ => false,
                okFunc: _ => GetEmptyStringList(),
                badFunc: numbers => new List<string> { numbers.Count.ToString() });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereTaskAsync_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereTaskAsync(_ => true,
                okFunc: _ => GetEmptyStringList(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereTaskAsync_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereTaskAsync(_ => false,
                okFunc: _ => GetEmptyStringList(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadTaskAsync(
                okFunc: CollectionToString,
                badFunc: _ => GetEmptyStringList());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadTaskAsync(
                okFunc: _ => GetEmptyStringList(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkTaskAsync(CollectionToString);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultCollectionOkTaskAsync(CollectionToString);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadTaskAsync_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBadTaskAsync(errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialCollection, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadTaskAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultCollectionBadTaskAsync(errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }
    }
}