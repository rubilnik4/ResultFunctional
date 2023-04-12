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
    public class ResultValueFactoryTest
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public void CreateResultValue_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = RValueFactory.Some(initialValue);

            Assert.True(resultValue.Success);
            Assert.Equal(initialValue, resultValue.GetValue());
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateResultValue_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = RValueFactory.None<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateResultValue_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = RValueFactory.None<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public void CreateResultValueAsync_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = RValueFactory.Some(initialValue);

            Assert.True(resultValue.Success);
            Assert.Equal(initialValue, resultValue.GetValue());
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateResultValueAsync_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = RValueFactory.None<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateResultValueAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = RValueFactory.None<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValue_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await RValueFactory.SomeTask(initialValue);

            Assert.True(resultValue.Success);
            Assert.Equal(initialValue, resultValue.GetValue());
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValue_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await RValueFactory.NoneTask<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValue_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await RValueFactory.NoneTask<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValueAsync_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await RValueFactory.SomeTask(initialValue);

            Assert.True(resultValue.Success);
            Assert.Equal(initialValue, resultValue.GetValue());
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValueAsync_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await RValueFactory.NoneTask<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultValueAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await RValueFactory.NoneTask<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }
    }
}