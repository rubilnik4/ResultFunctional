using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RUnitTest
{
    /// <summary>
    /// Преобразование в результирующий ответ. Тесты
    /// </summary>
    public class ToRUnitExtensionsTest
    {
        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public void ToRMaybe_Ok()
        {
            var results = new List<IRMaybe>
            {
                CreateErrorListTwoTest().ToRUnit(), 
                CreateErrorTest().ToRUnit()
            };

            var result = results.ToRUnit();

            Assert.True(result.GetErrors().SequenceEqual(results.SelectMany(RMaybe => RMaybe.GetErrors())));
        }

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public void ToRMaybeByError_Ok()
        {
            var results = new List<IRError>
            {
                CreateErrorTest(),
                CreateErrorTest()
            };

            var result = results.ToRUnit();

            Assert.True(result.GetErrors().SequenceEqual(results));
        }
    }
}