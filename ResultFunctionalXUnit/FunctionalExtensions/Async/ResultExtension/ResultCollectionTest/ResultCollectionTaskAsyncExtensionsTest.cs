﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatResultCollection_Ok()
        {
            var resultCollectionFirst = GetRangeNumber().ToRList();
            var resultCollectionSecond = GetRangeNumber().ToRList();
            var results = Enumerable.Empty<IResultCollection<int>>().Append(resultCollectionFirst).Append(resultCollectionSecond).
                                     Map(Task.FromResult);

            var resultCollection = await results.ConcatResultCollectionTaskAsync();
            var numberRange = resultCollectionFirst.Value.Concat(resultCollectionSecond.Value).ToList();

            Assert.True(resultCollection.OkStatus);
            Assert.True(numberRange.SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatResultCollection_Error()
        {
            var resultCollectionFirst = GetRangeNumber().ToRList();
            var resultCollectionErrorFirst = CreateErrorTest().ToRList<int>();
            var resultCollectionErrorSecond = CreateErrorTest().ToRList<int>();
            var results = Enumerable.Empty<IResultCollection<int>>().Append(resultCollectionFirst).Append(resultCollectionErrorFirst).Append(resultCollectionErrorSecond).
                                     Map(Task.FromResult);

            var resultCollection = await results.ConcatResultCollectionTaskAsync();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(2, resultCollection.Errors.Count);
        }
    }
}