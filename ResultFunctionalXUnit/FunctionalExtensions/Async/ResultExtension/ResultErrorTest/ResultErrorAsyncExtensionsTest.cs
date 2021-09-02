using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа. Тесты
    /// </summary>
    public class ResultErrorAsyncExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultValueTaskAsync_OkStatus()
        {
            var resultNoError = ResultErrorFactory.CreateTaskResultError();
            const string value = "OkStatus";

            var resultValue = await resultNoError.ToResultValueTaskAsync(value);

            Assert.True(resultValue.OkStatus);
            Assert.Equal(value, resultValue.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateTaskResultError(error);
            const string value = "BadStatus";

            var resultValue = await resultHasError.ToResultValueTaskAsync(value);

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValue_OkStatus()
        {
            var resultNoError = new ResultError();
            const string value = "OkStatus";
            var resultValue = ResultValueFactory.CreateTaskResultValue(value);

            var resultValueAfter = await resultNoError.ToResultBindValueAsync(resultValue);

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
            var resultHasError = new ResultError(error);
            const string value = "BadStatus";
            var resultValue = ResultValueFactory.CreateTaskResultValue(value);

            var resultValueAfter = await resultHasError.ToResultBindValueAsync(resultValue);

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
            var resultNoError = new ResultError();
            var error = CreateErrorTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<string>(error);

            var resultValueAfter = await resultNoError.ToResultBindValueAsync(resultValue);

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
            var resultHasError = new ResultError(error);
            var errors = CreateErrorListTwoTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<string>(errors);

            var resultValueAfter = await resultHasError.ToResultBindValueAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }
    }
}