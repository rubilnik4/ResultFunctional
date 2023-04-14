using System.Threading.Tasks;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронных действий. Тесты
    /// </summary>
    public class VoidBindAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия
        /// </summary>
        [Fact]
        public async Task VoidBindAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await numberTask.
                                  VoidAwait(number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при положительном условии
        /// </summary>
        [Fact]
        public async Task VoidOkBindAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await numberTask.
                VoidSomeAwait(number => number > 0,
                    number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка невыполнения действия при отрицательном условии
        /// </summary>
        [Fact]
        public async Task VoidOkBindAsync_CounterAddNone()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await numberTask.
                VoidSomeAwait(number => number < 0,
                    number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при положительном условии
        /// </summary>
        [Fact]
        public async Task VoidWhereBindAsync_Ok()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await numberTask.VoidOptionAwait(_ => true,
                                                          number => voidObjectMock.Object.TestNumberVoidAsync(number),
                                                          _ => voidObjectMock.Object.TestVoidAsync());

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при негативном условии
        /// </summary>
        [Fact]
        public async Task VoidWhereBindAsync_Bad()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await numberTask.VoidOptionAwait(_ => false,
                                                          _ => voidObjectMock.Object.TestVoidAsync(),
                                                          number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialNumber), Times.Once);
        }
    }
}