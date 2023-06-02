using System.Linq;
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
        public async Task FoldRMaybe_Ok()
        {
            IRMaybe rFirst = RUnit.Some();
            IRMaybe rSecond = GetRangeNumber().ToRList();
            var rUnitFirst = rFirst.ToTask();
            var rUnitSecond = rSecond.ToTask();
            var results = Enumerable.Empty<Task<IRMaybe>>().Append(rUnitFirst).Append(rUnitSecond);

            var rList = await results.RMaybeFoldTask();

            Assert.True(rList.Success);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task FoldRMaybe_Error()
        {
            IRMaybe rFirst = RUnit.Some();
            var rUnitFirst = rFirst.ToTask();
            var rUnitErrorFirst = CreateErrorTest().ToRUnit().Map(unit => (IRMaybe)unit).ToTask();
            var rUnitErrorSecond = CreateErrorTest().ToRUnit().Map(unit => (IRMaybe)unit).ToTask();
            var results = Enumerable.Empty<Task<IRMaybe>>().Append(rUnitFirst).Append(rUnitErrorFirst).Append(rUnitErrorSecond);

            var rList = await results.RMaybeFoldTask();

            Assert.True(rList.Failure);
            Assert.Equal(2, rList.GetErrors().Count);
        }
    }
}