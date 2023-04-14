using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Units;
using ResultFunctionalXUnit.Data;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RUnitTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа для задачи-объекта. Тесты
    /// </summary>
    public class ToResultErrorTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public async Task ToResultErrorTaskAsync_FromResult_Ok()
        {
            var results = new List<IRUnit>
            {
                RUnitFactory.None(ErrorData.CreateErrorListTwoTest()),
                RUnitFactory.None(ErrorData.CreateErrorTest())
            };
            var taskResults = Task.FromResult((IEnumerable<IRUnit>)results);

            var result =  await taskResults.ToRUnitTask();

            Assert.True(result.GetErrors().SequenceEqual(results.SelectMany(resultError => resultError.GetErrors())));
        }

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public async Task ToResultErrorTaskAsync_FromErrors_Ok()
        {
            var results = new List<IRUnit>
            {
                RUnitFactory.None(ErrorData.CreateErrorListTwoTest()),
                RUnitFactory.None(ErrorData.CreateErrorTest())
            };
            var taskResults = results.Select(Task.FromResult);

            var result = await taskResults.ToRUnitTask();

            Assert.True(result.GetErrors().SequenceEqual(results.SelectMany(resultError => resultError.GetErrors())));
        }

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public async Task ToResultErrorByError_Ok()
        {
            var results = new List<IRError>
            {
                ErrorData.CreateErrorTest(),
                ErrorData.CreateErrorTest()
            };
            var resultsTask = Task.FromResult((IEnumerable<IRError>)results);

            var result = await resultsTask.ToRUnitTask();

            Assert.True(result.GetErrors().SequenceEqual(results));
        }
    }
}