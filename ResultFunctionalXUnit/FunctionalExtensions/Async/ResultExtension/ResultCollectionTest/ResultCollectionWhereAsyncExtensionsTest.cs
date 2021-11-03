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

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueAsync(_ => true,
                okFunc: CollectionToStringAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionContinueAsync(_ => false,
                okFunc: _ => ToTaskEnumerable(GetEmptyStringList()),
                badFunc: _ => ToTaskEnumerable(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueAsync(_ => true,
                okFunc: _ => ToTaskEnumerable(GetEmptyStringList()),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueAsync(_ => false,
                okFunc: _ => ToTaskEnumerable(GetEmptyStringList()),
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в асинхронном положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereAsync(_ => true,
                okFunc: CollectionToStringAsync,
                badFunc: _ => ToTaskEnumerable(GetEmptyStringList()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в асинхронном отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereAsync_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereAsync(_ => false,
                okFunc: _ => ToTaskEnumerable(GetEmptyStringList()),
                badFunc: numbers => ToTaskEnumerable(new List<string> { numbers.Count.ToString() }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereAsync_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereAsync(_ => true,
                okFunc: _ => ToTaskEnumerable(GetEmptyStringList()),
                badFunc: errors => ToTaskEnumerable(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereAsync_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionWhereAsync(_ => false,
                okFunc: _ => ToTaskEnumerable(GetEmptyStringList()),
                badFunc: errors => ToTaskEnumerable(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadAsync(
                okFunc: CollectionToStringAsync,
                badFunc: _ => ToTaskEnumerable(GetEmptyStringList()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadAsync(
                okFunc: _ => ToTaskEnumerable(GetEmptyStringList()),
                badFunc: errors => ToTaskEnumerable(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkAsync(CollectionToStringAsync);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultCollectionOkAsync(CollectionToStringAsync);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadAsync_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionBadAsync(
                errors => ToTaskEnumerable(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialCollection, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultValue.ResultCollectionBadAsync(
                errors => ToTaskEnumerable(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }
    }
}