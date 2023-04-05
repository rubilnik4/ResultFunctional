using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
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
            var results = new List<IResultError>
            {
                new ResultError(ErrorData.CreateErrorListTwoTest()),
                new ResultError(ErrorData.CreateErrorTest())
            };
            var taskResults = Task.FromResult((IEnumerable<IResultError>)results);

            var result =  await taskResults.ToResultErrorTaskAsync();

            Assert.True(result.Errors.SequenceEqual(results.SelectMany(resultError => resultError.Errors)));
        }

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public async Task ToResultErrorTaskAsync_FromErrors_Ok()
        {
            var results = new List<IResultError>
            {
                new ResultError(ErrorData.CreateErrorListTwoTest()),
                new ResultError(ErrorData.CreateErrorTest())
            };
            var taskResults = results.Select(Task.FromResult);

            var result = await taskResults.ToResultErrorTaskAsync();

            Assert.True(result.Errors.SequenceEqual(results.SelectMany(resultError => resultError.Errors)));
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

            var result = await resultsTask.ToResultErrorTaskAsync();

            Assert.True(result.Errors.SequenceEqual(results));
        }
    }
}