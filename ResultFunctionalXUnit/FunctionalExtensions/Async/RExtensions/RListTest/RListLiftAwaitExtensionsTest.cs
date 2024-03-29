﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Extensions.TaskExtensions;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию. Тесты
    /// </summary>
    public class RListLiftAwaitExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task RListToCollectionAsync_Ok_ReturnNewValue()
        {
            var initialCollection = Collections.GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListLiftMatchAwait(
                Collections.CollectionToStringAsync,
                _ => Collections.GetEmptyStringList());

            Assert.True(Collections.CollectionToString(initialCollection).SequenceEqual(resultAfterWhere));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RListToCollectionAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListLiftMatchAwait(
                Collections.CollectionToStringAsync,
                errors => new List<string> { errors.Count.ToString() });

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.First());
        }
    }
}