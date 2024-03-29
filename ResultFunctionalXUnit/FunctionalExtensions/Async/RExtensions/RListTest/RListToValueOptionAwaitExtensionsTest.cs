﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с коллекцией с возвращением к значению. Тесты
    /// </summary>
    public class RListToValueOptionAwaitExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueBindAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await rList.RListToValueOptionAwait(_ => true,
                Collections.AggregateToStringAsync,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueBindAsync_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = RListFactory.SomeTask(numberCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListToValueOptionAwait(_ => false,
                Collections.AggregateToStringAsync,
                _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueBindAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListToValueOptionAwait(_ => true,
                Collections.AggregateToStringAsync,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueBindAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListToValueOptionAwait(_ => false,
                Collections.AggregateToStringAsync,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task RListOkBadToValueBindAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await rList.RListToValueMatchAwait(
                Collections.AggregateToStringAsync,
                _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task RListOkBadToValueBindAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListToValueMatchAwait(
                Collections.AggregateToStringAsync,
                errors => Task.FromResult(errors.Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task RListOkToValueBindAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await rList.RListToValueSomeAwait(Collections.AggregateToStringAsync);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task RListOkToValueBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rList.RListToValueSomeAwait(Collections.AggregateToStringAsync);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}