using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;
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
        public void ToRUnit_Ok()
        {
            var results = new List<IRUnit>
            {
                RUnit.Some(),
                CreateErrorListTwoTest().ToRUnit(),
                CreateErrorTest().ToRUnit()
            };

            var result = results.ToRUnit();

            Assert.True(result.GetErrors().SequenceEqual(results.SelectMany(rMaybe => rMaybe.GetErrorsOrEmpty())));
        }

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public void ToRMaybe_Ok()
        {
            var results = new List<IRMaybe>
            {
                RUnit.Some(),
                CreateErrorListTwoTest().ToRUnit(), 
                CreateErrorTest().ToRUnit()
            };

            var result = results.ToRUnit();

            Assert.True(result.GetErrors().SequenceEqual(results.SelectMany(rMaybe => rMaybe.GetErrorsOrEmpty())));
        }

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public void ToRUnitByError_Ok()
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