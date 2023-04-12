using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
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

            Assert.True(resultString.Success);
            Assert.Equal(initialString, resultString.GetValue());
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

            Assert.True(resultString.Failure);
            Assert.True(resultString.GetErrors().First().Equals(initialError.Result));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckBindAsync_Ok()
        {
            const string initialString = "NotNull";

            var result = await initialString.ToResultValueNullCheckAsync(CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(initialString, result.GetValue());
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

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError.Result));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckStructBindAsync_Ok()
        {
            int? initialInt = 1;

            var result = await initialInt.ToResultValueNullCheckAsync(CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(initialInt, result.GetValue());
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

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError.Result));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueBindAsync_OkStatus()
        {
            var resultNoError = RUnitFactory.SomeTask();
            const string value = "OkStatus";
            var resultValue = RValueFactory.SomeTask(value);

            var resultValueAfter = await resultNoError.ToResultBindValueBindAsync(resultValue);

            Assert.True(resultValueAfter.Success);
            Assert.Equal(value, resultValueAfter.GetValue());
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueBindAsync_HasErrors()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = RUnitFactory.NoneTask(error);
            const string value = "BadStatus";
            var resultValue = RValueFactory.SomeTask(value);

            var resultValueAfter = await resultHasError.ToResultBindValueBindAsync(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueBindAsync_HasErrorsBind()
        {
            var resultNoError = RUnitFactory.SomeTask();
            var error = ErrorData.CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<string>(error);

            var resultValueAfter = await resultNoError.ToResultBindValueBindAsync(resultValue);
            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultValueBindBindAsync_HasErrorsBindInitial()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = RUnitFactory.NoneTask(error);
            var errors = ErrorData.CreateErrorListTwoTest();
            var resultValue = RValueFactory.NoneTask<string>(errors);

            var resultValueAfter = await resultHasError.ToResultBindValueBindAsync(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }
    }
}