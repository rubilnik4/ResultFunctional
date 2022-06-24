using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ToResultValueTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckTaskAsync_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var resultString = await initialString!.ToResultValueNullCheckTaskAsync(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString.Result, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckTaskAsync_ErrorNull()
        {
            var initialString = Task.FromResult<string?>(null);
            var initialError = CreateErrorTest();
            var resultString = await initialString.ToResultValueNullCheckTaskAsync(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckValueTaskAsync_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var resultString = await initialString.ToResultValueNullValueCheckTaskAsync(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString.Result, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckValueTaskAsync_ErrorNull()
        {
            var initialString = Task.FromResult<string>(null!);
            var initialError = CreateErrorTest();
            var resultString = await initialString.ToResultValueNullValueCheckTaskAsync(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }

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
    }
}