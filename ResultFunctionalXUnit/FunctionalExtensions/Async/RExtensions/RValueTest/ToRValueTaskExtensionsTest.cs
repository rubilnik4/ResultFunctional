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
    public class ToRValueTaskExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToRValueNullCheckValueTaskAsync_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var resultString = await initialString.ToRValueEnsureTask(CreateErrorTest());

            Assert.True(resultString.Success);
            Assert.Equal(initialString.Result, resultString.GetValue());
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToRValueNullCheckValueTaskAsync_ErrorNull()
        {
            var initialString = Task.FromResult<string>(null!);
            var initialError = CreateErrorTest();
            var resultString = await initialString.ToRValueEnsureTask(initialError);

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
            var rValue = value.ToRValue();

            var rValueAfter = await resultNoError.ToRValueBindTask(rValue);

            Assert.True(rValueAfter.Success);
            Assert.Equal(value, rValueAfter.GetValue());
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
            var rValue = value.ToRValue();

            var rValueAfter = await resultHasError.ToRValueBindTask(rValue);

            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public async Task ToResultBindValueTaskAsync_HasErrorsBind()
        {
            var resultNoError = RUnitFactory.SomeTask();
            var error = CreateErrorTest();
            var rValue = error.ToRValue<int>();

            var rValueAfter = await resultNoError.ToRValueBindTask(rValue);

            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public async Task ToRValueBindTaskAsync_HasErrorsBindInitial()
        {
            var error = CreateErrorTest();
            var resultHasError = RUnitFactory.NoneTask(error);
            var errors = CreateErrorListTwoTest();
            var rValue = errors.ToRValue<int>();

            var rValueAfter = await resultHasError.ToRValueBindTask(rValue);

            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }
    }
}