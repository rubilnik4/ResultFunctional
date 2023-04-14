using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
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
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.RListOptionTask(_ => true,
                okFunc: CollectionToString,
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.RListOptionTask(_ => false,
                okFunc: _ => GetEmptyStringList(),
                badFunc: _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.RListOptionTask(_ => true,
                okFunc: _ => GetEmptyStringList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.RListOptionTask(_ => false,
                okFunc: _ => GetEmptyStringList(),
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в асинхронном положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.RListWhereTask(_ => true,
                okFunc: CollectionToString,
                badFunc: _ => GetEmptyStringList());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в асинхронном отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereTaskAsync_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.RListWhereTask(_ => false,
                okFunc: _ => GetEmptyStringList(),
                badFunc: numbers => new List<string> { numbers.Count.ToString() });

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereTaskAsync_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.RListWhereTask(_ => true,
                okFunc: _ => GetEmptyStringList(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionWhereTaskAsync_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.RListWhereTask(_ => false,
                okFunc: _ => GetEmptyStringList(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.RListMatchTask(
                okFunc: CollectionToString,
                badFunc: _ => GetEmptyStringList());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.RListMatchTask(
                okFunc: _ => GetEmptyStringList(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.RListSomeTask(CollectionToString);

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.RListSomeTask(CollectionToString);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadTaskAsync_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.RListNoneTask(errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialCollection, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultCollectionBadTaskAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.RListNoneTask(errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе задачи-объекта с коллекцией
        /// </summary>
        [Fact]
        public async Task ResultCollectionCheckErrorsOkTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await resultCollection.RListEnsureTask(_ => true,
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task ResultCollectionCheckErrorsOkTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.RListEnsureTask(_ => false,
                badFunc: _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionCheckErrorsOkTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.RListEnsureTask(_ => true,
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionCheckErrorsOkTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.RListEnsureTask(_ => false,
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}