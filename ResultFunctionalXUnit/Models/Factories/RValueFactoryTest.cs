using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Factories
{

    /// <summary>
    /// Фабрика для создания результирующего ответа со значением. Тесты
    /// </summary>
    public class RValueFactoryTest
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public void CreateRValue_Ok()
        {
            int initialValue = Numbers.Number;

            var rValue = RValueFactory.Some(initialValue);

            Assert.True(rValue.Success);
            Assert.Equal(initialValue, rValue.GetValue());
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRValue_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = RValueFactory.None<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRValue_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = RValueFactory.None<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public void CreateRValueAsync_Ok()
        {
            int initialValue = Numbers.Number;

            var rValue = RValueFactory.Some(initialValue);

            Assert.True(rValue.Success);
            Assert.Equal(initialValue, rValue.GetValue());
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRValueAsync_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = RValueFactory.None<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRValueAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = RValueFactory.None<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskRValue_Ok()
        {
            int initialValue = Numbers.Number;

            var rValue = await RValueFactory.SomeTask(initialValue);

            Assert.True(rValue.Success);
            Assert.Equal(initialValue, rValue.GetValue());
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRValue_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = await RValueFactory.NoneTask<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRValue_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = await RValueFactory.NoneTask<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskRValueAsync_Ok()
        {
            int initialValue = Numbers.Number;

            var rValue = await RValueFactory.SomeTask(initialValue);

            Assert.True(rValue.Success);
            Assert.Equal(initialValue, rValue.GetValue());
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRValueAsync_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = await RValueFactory.NoneTask<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRValueAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = await RValueFactory.NoneTask<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }
    }
}