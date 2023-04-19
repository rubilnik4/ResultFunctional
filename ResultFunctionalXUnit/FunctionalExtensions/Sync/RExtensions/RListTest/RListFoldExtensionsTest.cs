using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующей коллекции со значением. Тесты
    /// </summary>
    public class RListFoldExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ConcatRList_Ok()
        {
            var rListFirst = GetRangeNumber().ToRList();
            var rListSecond = GetRangeNumber().ToRList();
            var results = Enumerable.Empty<IRList<int>>().Append(rListFirst).Append(rListSecond);

            var rList = results.RListFold();
            var numberRange = rListFirst.GetValue().Concat(rListSecond.GetValue()).ToList();

            Assert.True(rList.Success);
            Assert.True(numberRange.SequenceEqual(rList.GetValue()));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ConcatRList_Error()
        {
            var rListFirst = GetRangeNumber().ToRList();
            var rListErrorFirst = CreateErrorTest().ToRList<int>();
            var rListErrorSecond = CreateErrorTest().ToRList<int>();
            var results = Enumerable.Empty<IRList<int>>().Append(rListFirst).Append(rListErrorFirst).Append(rListErrorSecond);

            var rList = results.RListFold();

            Assert.True(rList.Failure);
            Assert.Equal(2, rList.GetErrors().Count);
        }
    }
}