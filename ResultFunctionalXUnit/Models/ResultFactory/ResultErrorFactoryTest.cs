using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.ResultFactory;
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
            var resultValue = await ResultErrorFactory.CreateTaskResultError();

            Assert.True(resultValue.OkStatus);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultError_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await ResultErrorFactory.CreateTaskResultError(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.Equals(resultValue.Errors.First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultError_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await ResultErrorFactory.CreateTaskResultError(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.SequenceEqual(resultValue.Errors));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        [Fact]
        public async Task CreateTaskResultErrorAsync_Ok()
        {
            var resultValue = await ResultErrorFactory.CreateTaskResultError();

            Assert.True(resultValue.OkStatus);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultErrorAsync_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await ResultErrorFactory.CreateTaskResultError(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.Equals(resultValue.Errors.First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultErrorAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await ResultErrorFactory.CreateTaskResultError(initialError);

            Assert.True(resultValue.HasErrors);
            Assert.True(initialError.SequenceEqual(resultValue.Errors));
        }
    }
}