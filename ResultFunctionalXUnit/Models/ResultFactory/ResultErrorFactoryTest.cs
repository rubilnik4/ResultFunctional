using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.ResultFactory
{

    /// <summary>
    /// Фабрика для создания результирующего ответа. Тесты
    /// </summary>
    public class ResultErrorFactoryTest
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        [Fact]
        public async Task CreateTaskResultError_Ok()
        {
            var resultValue = await RUnitFactory.SomeTask();

            Assert.True(resultValue.Success);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultError_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await RUnitFactory.NoneTask(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultError_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await RUnitFactory.NoneTask(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        [Fact]
        public async Task CreateTaskResultErrorAsync_Ok()
        {
            var resultValue = await RUnitFactory.SomeTask();

            Assert.True(resultValue.Success);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultErrorAsync_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await RUnitFactory.NoneTask(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultErrorAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await RUnitFactory.NoneTask(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }
    }
}