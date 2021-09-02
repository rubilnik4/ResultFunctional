using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа для задачи-объекта. Тесты
    /// </summary>
    public class ResultErrorTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValue_OkStatus()
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
        public async Task ToResultBindValue_HasErrors()
        {
            var error = CreateErrorTest();
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
        public async Task ToResultBindValue_HasErrorsBind()
        {
            var resultNoError = ResultErrorFactory.CreateTaskResultError();
            var error = CreateErrorTest();
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
        public async Task ToResultValueBind_HasErrorsBindInitial()
        {
            var error = CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateTaskResultError(error);
            var errors = CreateErrorListTwoTest();
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
        public async Task ToResultErrorTaskAsync_Ok()
        {
            var results = new List<IResultError>
            {
                new ResultError(CreateErrorListTwoTest()),
                new ResultError(CreateErrorTest())
            };
            var taskResults = Task.FromResult((IEnumerable<IResultError>)results);

            var result =  await taskResults.ToResultErrorTaskAsync();

            Assert.True(result.Errors.SequenceEqual(results.SelectMany(resultError => resultError.Errors)));
        }

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>
        [Fact]
        public async Task ToResultErrorsTaskAsync_Ok()
        {
            var results = new List<IResultError>
            {
                new ResultError(CreateErrorListTwoTest()),
                new ResultError(CreateErrorTest())
            };
            var taskResults = results.Select(Task.FromResult);

            var result = await taskResults.ToResultErrorsTaskAsync();

            Assert.True(result.Errors.SequenceEqual(results.SelectMany(resultError => resultError.Errors)));
        }
    }
}