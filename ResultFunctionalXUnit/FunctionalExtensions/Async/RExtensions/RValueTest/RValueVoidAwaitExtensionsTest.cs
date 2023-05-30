using System.Linq;
using System.Threading.Tasks;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением задачей-объектом.Тесты
    /// </summary>
    public class RValueVoidAwaitExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkBindAsync_Ok_CallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOkTask = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.RValueVoidSomeAwait(
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkBindAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var rMaybeTask = RValueFactory.NoneTask<int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RValueVoidSomeAwait(
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadBindAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybeTask = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RValueVoidNoneAwait(
                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadBindAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybeTask = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RValueVoidNoneAwait(
                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RValueVoidOkBadBindAsyncPart_Ok()
        {
            int initialValue = Numbers.Number;
            var resultOk = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RValueVoidMatchAwait(number => voidObjectMock.Object.TestNumberVoidAsync(number),
                                                                _ => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RValueVoidOkBadBindAsyncPart_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidMatchAwait(_ => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RValueVoidOkBadBindAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var resultOk = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.RValueVoidMatchAwait(number => voidObjectMock.Object.TestNumberVoidAsync(number),
                                                                _ => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(initialValue), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RValueVoidOkBadBindAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.RValueVoidMatchAwait(_ => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereBindAsync_Ok_OkPredicate_CallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOkTask = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.RValueVoidOptionAwait(_ => true,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereBindAsync_Ok_BadPredicate_NotCallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOkTask = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.RValueVoidOptionAwait(_ => false,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereBindAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybeTask = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybeTask.RValueVoidOptionAwait(_ => true,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereBindAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybeTask = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();
            
            var resultAfterVoid = await rMaybeTask.RValueVoidOptionAwait(_ => false,
                number => voidObjectMock.Object.TestNumberVoidAsync(number));

            Assert.True(resultAfterVoid.Equals(rMaybeTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }
    }
}