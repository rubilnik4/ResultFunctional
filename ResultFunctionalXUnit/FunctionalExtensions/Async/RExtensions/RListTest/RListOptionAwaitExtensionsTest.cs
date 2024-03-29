﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class RListOptionAwaitExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListContinueBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListOptionAwait(_ => true.ToTask(),
                CollectionToStringAsync,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListContinueBindAsync_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListOptionAwait(_ => false.ToTask(),
                _ => ToTaskCollection(GetEmptyStringList()),
                _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListContinueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListOptionAwait(_ => true.ToTask(),
                _ => ToTaskCollection(GetEmptyStringList()),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListContinueBindAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListOptionAwait(_ => false.ToTask(),
                _ => ToTaskCollection(GetEmptyStringList()),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в асинхронном положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListWhereBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListWhereAwait(_ => true,
                CollectionToStringAsync,
                _ => ToTaskCollection(GetEmptyStringList()));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в асинхронном отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListWhereBindAsync_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListWhereAwait(_ => false,
                _ => ToTaskCollection(GetEmptyStringList()),
                numbers => ToTaskCollection(new List<string> { numbers.Count.ToString() }));

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListWhereBindAsync_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListWhereAwait(_ => true,
                _ => ToTaskCollection(GetEmptyStringList()),
                errors => ToTaskCollection(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListWhereBindAsync_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListWhereAwait(_ => false,
                _ => ToTaskCollection(GetEmptyStringList()),
                errors => ToTaskCollection(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public async Task RListOkBadBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListMatchAwait(
                CollectionToStringAsync,
                _ => ToTaskCollection(GetEmptyStringList()));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public async Task RListOkBadBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListMatchAwait(
                _ => ToTaskCollection(GetEmptyStringList()),
                errors => ToTaskCollection(new List<string> { errors.Count.ToString() }));

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение положительного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task RListOkBindAsync_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListSomeAwait(CollectionToStringAsync);

            Assert.True(resultAfterWhere.Success);
            Assert.True((await CollectionToStringAsync(initialCollection)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task RListOkBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListSomeAwait(CollectionToStringAsync);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public async Task RListBadBindAsync_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListNoneAwait(
                errors => ToTaskCollection(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialCollection, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public async Task RListBadBindAsync_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListNoneAwait(
                errors => ToTaskCollection(GetListByErrorsCount(errors)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение условия в положительном асинхронном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public async Task RListErrorOkBindAsync_Ok_CheckNoError()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListEnsureAwait(_ => true.ToTask(),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном асинхронном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public async Task RListErrorOkBindAsync_Ok_CheckHasError()
        {
            var initialCollection = GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListEnsureAwait(_ => false.ToTask(),
                _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListErrorOkBindAsync_Bad_CheckNoError()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListEnsureAwait(_ => true.ToTask(),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном асинхронном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public async Task RListErrorOkBindAsync_Bad_CheckHasError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListEnsureAwait(_ => false.ToTask(),
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}