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

            Assert.True(resultString.Success);
            Assert.Equal(initialString.Result, resultString.GetValue());
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullValueCheckAsync_ErrorNull()
        {
            var initialString = Task.FromResult<string>(null!);
            var initialError = CreateErrorTestTask();

            var resultString = await initialString.ToResultValueNullValueCheckBindAsync(initialError);

            Assert.True(resultString.Failure);
            Assert.True(resultString.GetErrors().First().Equals(initialError.Result));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckBind_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var result = await initialString!.ToResultValueNullCheckBindAsync(CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(initialString.Result, result.GetValue());
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

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError.Result));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueAsync_OkStatus()
        {
            var resultNoError = RUnitFactory.Some();
            const string value = "OkStatus";
            var resultValue = RValueFactory.SomeTask(value);

            var resultValueAfter = await resultNoError.ToResultBindValueAsync(resultValue);

            Assert.True(resultValueAfter.Success);
            Assert.Equal(value, resultValueAfter.GetValue());
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueAsync_HasErrors()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = RUnitFactory.None(error);
            const string value = "BadStatus";
            var resultValue = RValueFactory.SomeTask(value);

            var resultValueAfter = await resultHasError.ToResultBindValueAsync(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueAsync_HasErrorsBind()
        {
            var resultNoError = RUnitFactory.Some();
            var error = ErrorData.CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<string>(error);

            var resultValueAfter = await resultNoError.ToResultBindValueAsync(resultValue);
            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultValueBindAsync_HasErrorsBindInitial()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = RUnitFactory.None(error);
            var errors = ErrorData.CreateErrorListTwoTest();
            var resultValue = RValueFactory.NoneTask<string>(errors);

            var resultValueAfter = await resultHasError.ToResultBindValueAsync(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }
    }
}