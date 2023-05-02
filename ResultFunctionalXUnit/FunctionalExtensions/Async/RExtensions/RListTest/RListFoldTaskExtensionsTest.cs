using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class RListFoldTaskExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatRList_Ok()
        {
            var rListFirst = GetRangeNumber().ToRList();
            var rListSecond = GetRangeNumber().ToRList();
            var results = Enumerable.Empty<IRList<int>>().Append(rListFirst).Append(rListSecond).
                                     Map(Task.FromResult);

            var rList = await results.RListFoldTask();
            var numberRange = rListFirst.GetValue().Concat(rListSecond.GetValue()).ToList();

            Assert.True(rList.Success);
            Assert.True(numberRange.SequenceEqual(rList.GetValue()));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatRList_Error()
        {
            var rListFirst = GetRangeNumber().ToRList();
            var rListErrorFirst = CreateErrorTest().ToRList<int>();
            var rListErrorSecond = CreateErrorTest().ToRList<int>();
            var results = Enumerable.Empty<IRList<int>>().Append(rListFirst).Append(rListErrorFirst).Append(rListErrorSecond).
                                     Map(Task.FromResult);

            var rList = await results.RListFoldTask();

            Assert.True(rList.Failure);
            Assert.Equal(2, rList.GetErrors().Count);
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