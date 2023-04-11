using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueVoidAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkAsync_Ok_CallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ResultValueVoidOkAsync(
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultError = initialError.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultValueVoidOkAsync(
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultValueVoidBadAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultValueVoidBadAsync(
                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultValueVoidBadAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultValueVoidBadAsync(
                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkBadAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ResultValueVoidOkBadAsync(number => voidObjectMock.Object.TestNumberVoidAsync(number),
                                                                _ => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkBadAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultValueVoidOkBadAsync(_ => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkWhereAsync_Ok_OkPredicate_CallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ResultValueVoidOkWhereAsync(_ => true,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkWhereAsync_Ok_BadPredicate_NotCallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOk = initialValue.ToRValue();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ResultValueVoidOkWhereAsync(_ => false,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkWhereAsync_Bad_OkPredicate_NotCallVoid()
        {
            var resultError = CreateErrorTest().ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultValueVoidOkWhereAsync(_ => true,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkWhereAsync_Bad_BadPredicate_NotCallVoid()
        {
            var resultError = CreateErrorTest().ToRValue<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultValueVoidOkWhereAsync(_ => false,
                 number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }
    }
}