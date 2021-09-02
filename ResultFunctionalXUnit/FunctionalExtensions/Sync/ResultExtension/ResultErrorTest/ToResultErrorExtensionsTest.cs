using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;

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
                new ResultError(ErrorData.CreateErrorListTwoTest()), 
                new ResultError(ErrorData.CreateErrorTest())
            };

            var result = results.ToResultError();

            Assert.True(result.Errors.SequenceEqual(results.SelectMany(resultError => resultError.Errors)));
        }
    }
}