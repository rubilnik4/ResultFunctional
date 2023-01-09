using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;
using ResultFunctional.Models.Factories;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueBindAsync(_ => true,
                okFunc: CollectionToStringAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueBindAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionContinueBindAsync(_ => false,
                okFunc: _ => ToTaskCollection(GetEmptyStringList()),
                badFunc: _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueBindAsync(_ => true,
                okFunc: _ => ToTaskCollection(GetEmptyStringList()),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueBindAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueBindAsync(_ => false,
                okFunc: _ => ToTaskCollection(GetEmptyStringList()),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в асинхронном положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereBindAsync(_ => true,
                okFunc: CollectionToStringAsync,
                badFunc: _ => ToTaskCollection(GetEmptyStringList()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в асинхронном отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereBindAsync_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereBindAsync(_ => false,
                okFunc: _ => ToTaskCollection(GetEmptyStringList()),
                badFunc: numbers => ToTaskCollection(new List<string> { numbers.Count.ToString() }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereBindAsync_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereBindAsync(_ => true,
                okFunc: _ => ToTaskCollection(GetEmptyStringList()),
                badFunc: errors => ToTaskCollection(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereBindAsync_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereBindAsync(_ => false,
                okFunc: _ => ToTaskCollection(GetEmptyStringList()),
                badFunc: errors => ToTaskCollection(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadBindAsync(
                okFunc: CollectionToStringAsync,
                badFunc: _ => ToTaskCollection(GetEmptyStringList()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadBindAsync(
                okFunc: _ => ToTaskCollection(GetEmptyStringList()),
                badFunc: errors => ToTaskCollection(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBindAsync(CollectionToStringAsync);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBindAsync(CollectionToStringAsync);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadBindAsync_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBadBindAsync(
                errors => ToTaskCollection(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialCollection, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadBindAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionBadBindAsync(
                errors => ToTaskCollection(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionErrorOkBindAsync_Ok_CheckNoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionCheckErrorsOkBindAsync(_ => true,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionErrorOkBindAsync_Ok_CheckHasError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionCheckErrorsOkBindAsync(_ => false,
                badFunc: _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionErrorOkBindAsync_Bad_CheckNoError()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionCheckErrorsOkBindAsync(_ => true,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionErrorOkBindAsync_Bad_CheckHasError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionCheckErrorsOkBindAsync(_ => false,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }
    }
}