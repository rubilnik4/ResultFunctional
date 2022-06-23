using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
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
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueTaskAsync_OkStatus()
        {
            var resultNoError = ResultErrorFactory.CreateTaskResultError();
            const string value = "OkStatus";
            var resultValue = new ResultValue<string>(value);

            var resultValueAfter = await resultNoError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.OkStatus);
            Assert.Equal(value, resultValueAfter.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueTaskAsync_HasErrors()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateTaskResultError(error);
            const string value = "BadStatus";
            var resultValue = new ResultValue<string>(value);

            var resultValueAfter = await resultHasError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueTaskAsync_HasErrorsBind()
        {
            var resultNoError = ResultErrorFactory.CreateTaskResultError();
            var error = ErrorData.CreateErrorTest();
            var resultValue = new ResultValue<string>(error);

            var resultValueAfter = await resultNoError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultValueBindTaskAsync_HasErrorsBindInitial()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateTaskResultError(error);
            var errors = ErrorData.CreateErrorListTwoTest();
            var resultValue = new ResultValue<string>(errors);

            var resultValueAfter = await resultHasError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

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
            var results = new List<IErrorResult>
            {
                ErrorData.CreateErrorTest(),
                ErrorData.CreateErrorTest()
            };
            var resultsTask = Task.FromResult((IEnumerable<IErrorResult>)results);

            var result = await resultsTask.ToResultErrorTaskAsync();

            Assert.True(result.Errors.SequenceEqual(results));
        }
    }
}