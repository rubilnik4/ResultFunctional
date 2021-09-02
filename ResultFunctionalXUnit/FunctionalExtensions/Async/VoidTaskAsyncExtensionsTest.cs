using System.Threading.Tasks;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для действий задачи-объекта. Тесты
    /// </summary>
    public class VoidTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия
        /// </summary>
        [Fact]
        public async Task VoidTaskAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await numberTask.
                                  VoidTaskAsync(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при положительном условии
        /// </summary>
        [Fact]
        public async Task VoidOkTaskAsync_CounterAddOne()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await numberTask.
                VoidOkTaskAsync(number => number > 0,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка невыполнения действия при отрицательном условии
        /// </summary>
        [Fact]
        public async Task VoidOkTaskAsync_CounterAddNone()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                await numberTask.
                VoidOkTaskAsync(number => number < 0,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при положительном условии
        /// </summary>
        [Fact]
        public async Task VoidWhereTaskAsync_Ok()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await numberTask.VoidWhereTaskAsync(_ => true,
                                                          number => voidObjectMock.Object.TestNumberVoid(number),
                                                          _ => voidObjectMock.Object.TestVoid());

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при негативном условии
        /// </summary>
        [Fact]
        public async Task VoidWhereTaskAsync_Bad()
        {
            const int initialNumber = 1;
            var numberTask = Task.FromResult(initialNumber);
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = await numberTask.VoidWhereTaskAsync(_ => false,
                                                          _ => voidObjectMock.Object.TestVoid(),
                                                          number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }
    }
}