﻿using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующей коллекции со значением. Тесты
    /// </summary>
    public class ResultCollectionExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ConcatResultCollection_Ok()
        {
            var resultCollectionFirst = GetRangeNumber().ToRList();
            var resultCollectionSecond = GetRangeNumber().ToRList();
            var results = Enumerable.Empty<IRList<int>>().Append(resultCollectionFirst).Append(resultCollectionSecond);

            var resultCollection = results.ConcatResultCollection();
            var numberRange = resultCollectionFirst.GetValue().Concat(resultCollectionSecond.GetValue()).ToList();

            Assert.True(resultCollection.Success);
            Assert.True(numberRange.SequenceEqual(resultCollection.GetValue()));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ConcatResultCollection_Error()
        {
            var resultCollectionFirst = GetRangeNumber().ToRList();
            var resultCollectionErrorFirst = CreateErrorTest().ToRList<int>();
            var resultCollectionErrorSecond = CreateErrorTest().ToRList<int>();
            var results = Enumerable.Empty<IRList<int>>().Append(resultCollectionFirst).Append(resultCollectionErrorFirst).Append(resultCollectionErrorSecond);

            var resultCollection = results.ConcatResultCollection();

            Assert.True(resultCollection.Failure);
            Assert.Equal(2, resultCollection.GetErrors().Count);
        }
    }
}