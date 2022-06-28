using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа. Тесты
    /// </summary>
    public class ToResultValueBindAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullValueCheckAsync_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var resultString = await initialString.ToResultValueNullValueCheckBindAsync(CreateErrorTestTask());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString.Result, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullValueCheckAsync_ErrorNull()
        {
            var initialString = Task.FromResult<string?>(null);
            var initialError = CreateErrorTestTask();

            var resultString = await initialString.ToResultValueNullValueCheckBindAsync(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError.Result));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckBind_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var result = await initialString!.ToResultValueNullCheckBindAsync(CreateErrorTestTask());

            Assert.True(result.OkStatus);
            Assert.Equal(initialString.Result, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckBind_ErrorNull()
        {
            var initialString = Task.FromResult<string?>(null);
            var initialError = CreateErrorTestTask();
            var result = await initialString.ToResultValueNullCheckBindAsync(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError.Result));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueAsync_OkStatus()
        {
            var resultNoError = ResultErrorFactory.CreateResultError();
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
        public async Task ToResultBindValueAsync_HasErrors()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateResultError(error);
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
        public async Task ToResultBindValueAsync_HasErrorsBind()
        {
            var resultNoError = ResultErrorFactory.CreateResultError();
            var error = ErrorData.CreateErrorTest();
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
        public async Task ToResultValueBindAsync_HasErrorsBindInitial()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateResultError(error);
            var errors = ErrorData.CreateErrorListTwoTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<string>(errors);

            var resultValueAfter = await resultHasError.ToResultBindValueAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }
    }
}