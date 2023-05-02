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
    public class RListOptionTaskExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе задачи-объекта с коллекцией
        /// </summary>
        [Fact]
        public async Task RListContinueTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListOptionTask(_ => true,
                CollectionToString,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListContinueTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListOptionTask(_ => false,
                _ => GetEmptyStringList(),
                _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListContinueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListOptionTask(_ => true,
                _ => GetEmptyStringList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListContinueTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListOptionTask(_ => false,
                _ => GetEmptyStringList(),
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в асинхронном положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListWhereTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListWhereTask(_ => true,
                CollectionToString,
                _ => GetEmptyStringList());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в асинхронном отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListWhereTaskAsync_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListWhereTask(_ => false,
                _ => GetEmptyStringList(),
                numbers => new List<string> { numbers.Count.ToString() });

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListWhereTaskAsync_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListWhereTask(_ => true,
                _ => GetEmptyStringList(),
                errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListWhereTaskAsync_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListWhereTask(_ => false,
                _ => GetEmptyStringList(),
                errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public async Task RListOkBadTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListMatchTask(
                CollectionToString,
                _ => GetEmptyStringList());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public async Task RListOkBadTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListMatchTask(
                _ => GetEmptyStringList(),
                errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task RListOkTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListSomeTask(CollectionToString);

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task RListOkTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListSomeTask(CollectionToString);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task RListBadTaskAsync_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListNoneTask(errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialCollection, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task RListBadTaskAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListNoneTask(errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе задачи-объекта с коллекцией
        /// </summary>
        [Fact]
        public async Task RListCheckErrorsOkTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListEnsureTask(_ => true,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе задачи-объекта с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListCheckErrorsOkTaskAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListEnsureTask(_ => false,
                _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListCheckErrorsOkTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListEnsureTask(_ => true,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе задачи-объекта с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListCheckErrorsOkTaskAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListEnsureTask(_ => false,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}