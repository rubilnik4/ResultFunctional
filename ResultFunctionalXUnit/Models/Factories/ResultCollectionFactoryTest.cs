using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.Models.Factories
{

    /// <summary>
    /// Фабрика для создания результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionFactoryTest
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public void CreateResultCollection_Ok()
        {
            var initialValue = GetRangeNumber();

            var resultValue = RListFactory.Some(initialValue);

            Assert.True(resultValue.Success);
            Assert.True(initialValue.SequenceEqual(resultValue.GetValue()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateResultCollection_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = RListFactory.None<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateResultCollection_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = RListFactory.None<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public void CreateResultCollectionAsync_Ok()
        {
            var initialValue = GetRangeNumber();

            var resultValue = RListFactory.Some(initialValue);

            Assert.True(resultValue.Success);
            Assert.True(initialValue.SequenceEqual(resultValue.GetValue()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateResultCollectionAsync_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = RListFactory.None<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateResultCollectionAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = RListFactory.None<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollection_Ok()
        {
            var initialValue = GetRangeNumber();

            var resultValue = await RListFactory.SomeTask(initialValue);

            Assert.True(resultValue.Success);
            Assert.True(initialValue.SequenceEqual(resultValue.GetValue()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollection_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await RListFactory.NoneTask<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollection_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await RListFactory.NoneTask<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollectionAsync_Ok()
        {
            var initialValue = GetRangeNumber();

            var resultValue = await RListFactory.SomeTask(initialValue);

            Assert.True(resultValue.Success);
            Assert.True(initialValue.SequenceEqual(resultValue.GetValue()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollectionAsync_Error()
        {
            var initialError = CreateErrorTest();

            var resultValue = await RListFactory.NoneTask<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.Equals(resultValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskResultCollectionAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var resultValue = await RListFactory.NoneTask<int>(initialError);

            Assert.True(resultValue.Failure);
            Assert.True(initialError.SequenceEqual(resultValue.GetErrors()));
        }
    }
}