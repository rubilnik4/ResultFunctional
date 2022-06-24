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
    public class ToResultValueAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullValueCheckBindAsync_Ok()
        {
            const string initialString = "NotNull";

            var resultString = await initialString.ToResultValueNullValueCheckAsync(CreateErrorTestTask());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullValueCheckBindAsync_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTestTask();
            var resultString = await initialString!.ToResultValueNullValueCheckAsync(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError.Result));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckBindAsync_Ok()
        {
            const string initialString = "NotNull";

            var result = await initialString.ToResultValueNullCheckAsync(CreateErrorTestTask());

            Assert.True(result.OkStatus);
            Assert.Equal(initialString, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckBindAsync_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTestTask();
            var result = await initialString.ToResultValueNullCheckAsync(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError.Result));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckStructBindAsync_Ok()
        {
            int? initialInt = 1;

            var result = await initialInt.ToResultValueNullCheckAsync(CreateErrorTestTask());

            Assert.True(result.OkStatus);
            Assert.Equal(initialInt, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckStructBindAsync_ErrorNull()
        {
            int? initialInt = null;
            var initialError = CreateErrorTestTask();
            var result = await initialInt.ToResultValueNullCheckAsync(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError.Result));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueBindAsync_OkStatus()
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
        public async Task ToResultBindValueBindAsync_HasErrors()
        {
            var error = ErrorData.CreateErrorTest();
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
        public async Task ToResultBindValueBindAsync_HasErrorsBind()
        {
            var resultNoError = ResultErrorFactory.CreateTaskResultError();
            var error = ErrorData.CreateErrorTest();
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
        public async Task ToResultValueBindBindAsync_HasErrorsBindInitial()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = ResultErrorFactory.CreateTaskResultError(error);
            var errors = ErrorData.CreateErrorListTwoTest();
            var resultValue = ResultValueFactory.CreateTaskResultValueError<string>(errors);

            var resultValueAfter = await resultHasError.ToResultBindValueBindAsync(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }
    }
}