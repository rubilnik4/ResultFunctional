﻿using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа. Тесты
    /// </summary>
    public class RMaybeFoldTaskExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatRMaybe_Ok()
        {
            var rUnitFirst = RUnit.Some();
            var rUnitSecond = GetRangeNumber().ToRList();
            var results = Enumerable.Empty<IRMaybe>().Append(rUnitFirst).Append(rUnitSecond).
                                     Map(Task.FromResult);

            var rMaybe = await results.RMaybeFoldTask();

            Assert.True(rMaybe.Success);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatRMaybe_Error()
        {
            var rUnitFirst = RUnit.Some();
            var rUnitErrorFirst = CreateErrorTest().ToRList<int>();
            var rUnitErrorSecond = CreateErrorTest().ToRList<int>();
            var results = Enumerable.Empty<IRMaybe>().Append(rUnitFirst).Append(rUnitErrorFirst).Append(rUnitErrorSecond).
                                     Map(Task.FromResult);

            var rMaybe = await results.RMaybeFoldTask();

            Assert.True(rMaybe.Failure);
            Assert.Equal(2, rMaybe.GetErrors().Count);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task FoldRList_Ok()
        {
            var firstRange = GetRangeNumber();
            var secondRange = GetRangeNumber();
            var rListFirst = firstRange.ToRList().ToTask();
            var rListSecond = secondRange.ToRList().ToTask();
            var results = Enumerable.Empty<Task<IRList<int>>>().Append(rListFirst).Append(rListSecond);

            var rList = await results.RListFoldTask();
            var numberRange = firstRange.Concat(secondRange).ToList();

            Assert.True(rList.Success);
            Assert.True(numberRange.SequenceEqual(rList.GetValue()));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task FoldRList_Error()
        {
            var firstRange = GetRangeNumber();
            var rListFirst = firstRange.ToRList().ToTask();
            var rListErrorFirst = CreateErrorTest().ToRList<int>().ToTask();
            var rListErrorSecond = CreateErrorTest().ToRList<int>().ToTask();
            var results = Enumerable.Empty<Task<IRList<int>>>().Append(rListFirst).Append(rListErrorFirst).Append(rListErrorSecond);

            var rList = await results.RListFoldTask();

            Assert.True(rList.Failure);
            Assert.Equal(2, rList.GetErrors().Count);
        }
    }
}