using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RUnitTest
{
    /// <summary>
    /// Преобразование в результирующий ответ. Тесты
    /// </summary>
    public class ToResultErrorExtensionsTest
    {
        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public void ToResultError_Ok()
        {
            var results = new List<IROption>
            {
                CreateErrorListTwoTest().ToRUnit(), 
                CreateErrorTest().ToRUnit()
            };

            var result = results.ToRUnit();

            Assert.True(result.GetErrors().SequenceEqual(results.SelectMany(resultError => resultError.GetErrors())));
        }

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public void ToResultErrorByError_Ok()
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