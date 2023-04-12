using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
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

            Assert.True(resultString.Success);
            Assert.Equal(initialString.Result, resultString.GetValue());
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

            Assert.True(resultString.Failure);
            Assert.True(resultString.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckValueTaskAsync_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var resultString = await initialString.ToResultValueNullValueCheckTaskAsync(CreateErrorTest());

            Assert.True(resultString.Success);
            Assert.Equal(initialString.Result, resultString.GetValue());
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

            Assert.True(resultString.Failure);
            Assert.True(resultString.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueTaskAsync_OkStatus()
        {
            var resultNoError = RUnitFactory.SomeTask();
            const string value = "OkStatus";
            var resultValue = value.ToRValue();

            var resultValueAfter = await resultNoError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.Success);
            Assert.Equal(value, resultValueAfter.GetValue());
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueTaskAsync_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = RUnitFactory.NoneTask(error);
            const string value = "BadStatus";
            var resultValue = value.ToRValue();

            var resultValueAfter = await resultHasError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueTaskAsync_HasErrorsBind()
        {
            var resultNoError = RUnitFactory.SomeTask();
            var error = CreateErrorTest();
            var resultValue = error.ToRValue<int>();

            var resultValueAfter = await resultNoError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToResultValueBindTaskAsync_HasErrorsBindInitial()
        {
            var error = CreateErrorTest();
            var resultHasError = RUnitFactory.NoneTask(error);
            var errors = CreateErrorListTwoTest();
            var resultValue = errors.ToRValue<int>();

            var resultValueAfter = await resultHasError.ToResultBindValueTaskAsync(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }
    }
}