using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest
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
            var results = new List<IResultError>
            {
                new ResultError(CreateErrorListTwoTest()), 
                new ResultError(CreateErrorTest())
            };

            var result = results.ToResultError();

            Assert.True(result.Errors.SequenceEqual(results.SelectMany(resultError => resultError.Errors)));
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

            var result = results.ToResultError();

            Assert.True(result.Errors.SequenceEqual(results));
        }
    }
}