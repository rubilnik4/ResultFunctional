using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RMaybeTest
{
    /// <summary>
    /// Методы расширения для результирующей коллекции со значением. Тесты
    /// </summary>
    public class RMaybeFoldExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ConcatRMaybe_Ok()
        {
            var rUnitFirst = RUnit.Some();
            var rUnitSecond = GetRangeNumber().ToRList();
            var results = Enumerable.Empty<IRMaybe>().Append(rUnitFirst).Append(rUnitSecond);

            var rMaybe = results.RMaybeFold();

            Assert.True(rMaybe.Success);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ConcatRMaybe_Error()
        {
            var rUnitFirst = RUnit.Some();
            var rUnitErrorFirst = CreateErrorTest().ToRList<int>();
            var rUnitErrorSecond = CreateErrorTest().ToRList<int>();
            var results = Enumerable.Empty<IRMaybe>().Append(rUnitFirst).Append(rUnitErrorFirst).Append(rUnitErrorSecond);

            var rMaybe = results.RMaybeFold();

            Assert.True(rMaybe.Failure);
            Assert.Equal(2, rMaybe.GetErrors().Count);
        }
    }
}