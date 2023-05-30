using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением. Тесты
    /// </summary>
    public class RValueVoidAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RValueVoidOkAsync_Ok_CallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RValueVoidSomeAsync(
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task RValueVoidOkAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var rMaybe = initialError.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidSomeAsync(
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task RValueVoidBadAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidNoneAsync(
                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task RValueVoidBadAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidNoneAsync(
                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RValueVoidOkBadAsyncPart_Ok()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RValueVoidMatchAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number),
                                                                      _ => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RValueVoidOkBadAsyncPart_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidMatchAsync(_ => voidObjectMock.Object.TestVoidAsync(),
                                                                    errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RValueVoidOkBadAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RValueVoidMatchAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number),
                                                                _ => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RValueVoidOkBadAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidMatchAsync(_ => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RValueVoidOkWhereAsync_Ok_OkPredicate_CallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RValueVoidOptionAsync(_ => true,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RValueVoidOkWhereAsync_Ok_BadPredicate_NotCallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RValueVoidOptionAsync(_ => false,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RValueVoidOkWhereAsync_Bad_OkPredicate_NotCallVoid()
        {
            var rMaybe = CreateErrorTest().ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidOptionAsync(_ => true,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RValueVoidOkWhereAsync_Bad_BadPredicate_NotCallVoid()
        {
            var rMaybe = CreateErrorTest().ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidOptionAsync(_ => false,
                 number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }
    }
}