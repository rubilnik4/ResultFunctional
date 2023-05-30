using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа. Тесты
    /// </summary>
    public class RMaybeVoidAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkAsync_Ok_CallVoid()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RMaybeVoidSomeAsync(() => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var rMaybe = RUnitFactory.None(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RMaybeVoidSomeAsync(() => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidBadAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.None(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RMaybeVoidNoneAsync(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidBadAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.None(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RMaybeVoidNoneAsync(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadAsyncPart_Ok()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RMaybeVoidMatchAsync(() => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadAsyncPart_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.None(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RMaybeVoidMatchAsync(() => voidObjectMock.Object.TestVoidAsync(),
                                                                   errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadAsync_Ok()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RMaybeVoidMatchAsync(() => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.None(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RMaybeVoidMatchAsync(() => voidObjectMock.Object.TestVoidAsync(),
                                                                   errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereAsync_Ok_OkPredicate_CallVoid()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RMaybeVoidOptionAsync(() => true,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereAsync_Ok_BadPredicate_NotCallVoid()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RMaybeVoidOptionAsync(() => false,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.None(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RMaybeVoidOptionAsync(() => true,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.None(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RMaybeVoidOptionAsync(() => false,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }
    }
}