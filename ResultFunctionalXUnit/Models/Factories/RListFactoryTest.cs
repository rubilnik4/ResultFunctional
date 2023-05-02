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
    public class RListFactoryTest
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public void CreateRList_Ok()
        {
            var initialValue = GetRangeNumber();

            var rValue = RListFactory.Some(initialValue);

            Assert.True(rValue.Success);
            Assert.True(initialValue.SequenceEqual(rValue.GetValue()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRList_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = RListFactory.None<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRList_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = RListFactory.None<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public void CreateRListAsync_Ok()
        {
            var initialValue = GetRangeNumber();

            var rValue = RListFactory.Some(initialValue);

            Assert.True(rValue.Success);
            Assert.True(initialValue.SequenceEqual(rValue.GetValue()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRListAsync_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = RListFactory.None<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void CreateRListAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = RListFactory.None<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskRList_Ok()
        {
            var initialValue = GetRangeNumber();

            var rValue = await RListFactory.SomeTask(initialValue);

            Assert.True(rValue.Success);
            Assert.True(initialValue.SequenceEqual(rValue.GetValue()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRList_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = await RListFactory.NoneTask<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRList_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = await RListFactory.NoneTask<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task CreateTaskRListAsync_Ok()
        {
            var initialValue = GetRangeNumber();

            var rValue = await RListFactory.SomeTask(initialValue);

            Assert.True(rValue.Success);
            Assert.True(initialValue.SequenceEqual(rValue.GetValue()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRListAsync_Error()
        {
            var initialError = CreateErrorTest();

            var rValue = await RListFactory.NoneTask<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.Equals(rValue.GetErrors().First()));
        }

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task CreateTaskRListAsync_Errors()
        {
            var initialError = CreateErrorListTwoTest();

            var rValue = await RListFactory.NoneTask<int>(initialError);

            Assert.True(rValue.Failure);
            Assert.True(initialError.SequenceEqual(rValue.GetErrors()));
        }
    }
}