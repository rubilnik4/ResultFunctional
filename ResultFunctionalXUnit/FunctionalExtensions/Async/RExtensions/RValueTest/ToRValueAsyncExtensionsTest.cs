using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа. Тесты
    /// </summary>
    public class ToRValueAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToRValueNullValueCheckBindAsync_Ok()
        {
            const string initialString = "NotNull";

            var resultString = await initialString.ToRValueEnsureAsync(CreateErrorTestTask());

            Assert.True(resultString.Success);
            Assert.Equal(initialString, resultString.GetValue());
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToRValueNullValueCheckBindAsync_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTestTask();
            var resultString = await initialString!.ToRValueEnsureAsync(initialError);

            Assert.True(resultString.Failure);
            Assert.True(resultString.GetErrors().First().Equals(initialError.Result));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueBindAsync_OkStatus()
        {
            var resultNoError = RUnitFactory.SomeTask();
            const string value = "OkStatus";
            var rValue = RValueFactory.SomeTask(value);

            var rValueAfter = await resultNoError.ToRValueBindAwait(rValue);

            Assert.True(rValueAfter.Success);
            Assert.Equal(value, rValueAfter.GetValue());
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
            var rValue = RValueFactory.SomeTask(value);

            var rValueAfter = await resultHasError.ToRValueBindAwait(rValue);

            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueBindAsync_HasErrorsBind()
        {
            var resultNoError = RUnitFactory.SomeTask();
            var error = ErrorData.CreateErrorTest();
            var rValue = RValueFactory.NoneTask<string>(error);

            var rValueAfter = await resultNoError.ToRValueBindAwait(rValue);
            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToRValueBindBindAsync_HasErrorsBindInitial()
        {
            var error = ErrorData.CreateErrorTest();
            var resultHasError = RUnitFactory.NoneTask(error);
            var errors = ErrorData.CreateErrorListTwoTest();
            var rValue = RValueFactory.NoneTask<string>(errors);

            var rValueAfter = await resultHasError.ToRValueBindAwait(rValue);

            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }
    }
}