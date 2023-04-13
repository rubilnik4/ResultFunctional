using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для действий. Тесты
    /// </summary>
    public class VoidExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия
        /// </summary>
        [Fact]
        public void Void_CounterAddOne()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = initialNumber.Void(number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при положительном условии
        /// </summary>
        [Fact]
        public void VoidOk_CounterAddOne()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                initialNumber.
                VoidSome(number => number > 0,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка невыполнения действия при отрицательном условии
        /// </summary>
        [Fact]
        public void VoidOk_CounterAddNone()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid =
                initialNumber.
                VoidSome(number => number < 0,
                    action: number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при положительном условии
        /// </summary>
        [Fact]
        public void VoidWhere_Ok()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = initialNumber.VoidOption(_ => true,
                                                          number => voidObjectMock.Object.TestNumberVoid(number),
                                                          _ => voidObjectMock.Object.TestVoid());

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при негативном условии
        /// </summary>
        [Fact]
        public void VoidWhere_Bad()
        {
            const int initialNumber = 1;
            var voidObjectMock = new Mock<IVoidObject>();

            int numberAfterVoid = initialNumber.VoidOption(_ => false,
                                                          _ => voidObjectMock.Object.TestVoid(),
                                                          number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.Equal(initialNumber, numberAfterVoid);
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialNumber), Times.Once);
        }
    }
}