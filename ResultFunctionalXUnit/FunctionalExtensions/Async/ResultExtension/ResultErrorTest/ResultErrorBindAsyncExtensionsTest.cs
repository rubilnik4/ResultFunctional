using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.ResultFactory;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа для задачи-объекта. Тесты
    /// </summary>
    public class ResultErrorBindAsyncExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValue_OkStatus()
        {
            var resultNoError = ResultErrorFactory.CreateTaskResultError();
            const string value = "OkStatus";
            var resultValue = ResultValueFactory.CreateTaskResultValue(value);

            var resultValueAfter = await resultNoError.ToResultBindValueBindAsync(resultValue);

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
            var resultValue = ResultValueFactory.CreateTaskResultValue(value);

            var resultValueAfter = await resultHasError.ToResultBindValueBindAsync(resultValue);

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
            var resultValue = ResultValueFactory.CreateTaskResultValueError<string>(error);

            var resultValueAfter = await resultNoError.ToResultBindValueBindAsync(resultValue);

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
            var resultValue = ResultValueFactory.CreateTaskResultValueError<string>(errors);

            var resultValueAfter = await resultHasError.ToResultBindValueBindAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }
    }
}