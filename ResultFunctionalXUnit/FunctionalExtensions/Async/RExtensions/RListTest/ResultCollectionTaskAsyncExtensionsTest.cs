using System.Linq;
using System.Threading.Tasks;
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
    public class RListTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatRList_Ok()
        {
            var RListFirst = GetRangeNumber().ToRList();
            var RListSecond = GetRangeNumber().ToRList();
            var results = Enumerable.Empty<IRList<int>>().Append(RListFirst).Append(RListSecond).
                                     Map(Task.FromResult);

            var RList = await results.RListFoldTask();
            var numberRange = RListFirst.GetValue().Concat(RListSecond.GetValue()).ToList();

            Assert.True(RList.Success);
            Assert.True(numberRange.SequenceEqual(RList.GetValue()));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatRList_Error()
        {
            var RListFirst = GetRangeNumber().ToRList();
            var RListErrorFirst = CreateErrorTest().ToRList<int>();
            var RListErrorSecond = CreateErrorTest().ToRList<int>();
            var results = Enumerable.Empty<IRList<int>>().Append(RListFirst).Append(RListErrorFirst).Append(RListErrorSecond).
                                     Map(Task.FromResult);

            var RList = await results.RListFoldTask();

            Assert.True(RList.Failure);
            Assert.Equal(2, RList.GetErrors().Count);
        }
    }
}