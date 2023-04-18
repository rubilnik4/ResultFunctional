using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Factories
{

    /// <summary>
    /// Фабрика для создания результирующего ответа. Тесты
    /// </summary>
    public class RMaybeFactoryTest
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        [Fact]
        public void CreateRMaybe_Ok()
        {
            var rValue = RUnitFactory.Some();

            Assert.True(rValue.Success);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRMaybe_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = RUnitFactory.None(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRMaybe_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = RUnitFactory.None(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        [Fact]
        public void CreateRMaybeAsync_Ok()
        {
            var rValue = RUnitFactory.Some();

            Assert.True(rValue.Success);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRMaybeAsync_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = RUnitFactory.None(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRMaybeAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = RUnitFactory.None(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        [Fact]
        public async Task CreateTaskRMaybe_Ok()
        {
            var rValue = await RUnitFactory.SomeTask();

            Assert.True(rValue.Success);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRMaybe_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = await RUnitFactory.NoneTask(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRMaybe_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = await RUnitFactory.NoneTask(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        [Fact]
        public async Task CreateTaskRMaybeAsync_Ok()
        {
            var rValue = await RUnitFactory.SomeTask();

            Assert.True(rValue.Success);
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRMaybeAsync_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = await RUnitFactory.NoneTask(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRMaybeAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = await RUnitFactory.NoneTask(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }
    }
}