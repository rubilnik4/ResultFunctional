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
    /// Асинхронное действие над внутренним типом результирующего ответа задачи-объекта. Тесты
    /// </summary>
    public class RMaybeVoidAwaitExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBindAsync_Ok_CallVoid()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidSomeAwait(() => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBindAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var rMaybe = RUnitFactory.NoneTask(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.ToRMaybeTask().RMaybeVoidSomeAwait(() => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidBadBindAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.ToRMaybeTask().RMaybeVoidNoneAwait(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidBadBindAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.ToRMaybeTask().RMaybeVoidNoneAwait(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadBindAsyncPart_Ok()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidMatchAwait(() => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadBindAsyncPart_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.ToRMaybeTask().RMaybeVoidMatchAwait(() => voidObjectMock.Object.TestVoidAsync(),
                                                                   errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadBindAsync_Ok()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidMatchAwait(() => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadBindAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.ToRMaybeTask().RMaybeVoidMatchAwait(() => voidObjectMock.Object.TestVoidAsync(),
                                                                   errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereBindAsync_Ok_OkPredicate_CallVoid()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidOptionAwait(() => true,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereBindAsync_Ok_BadPredicate_NotCallVoid()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidOptionAwait(() => false,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereBindAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.ToRMaybeTask().RMaybeVoidOptionAwait(() => true,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereBindAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await rMaybe.ToRMaybeTask().RMaybeVoidOptionAwait(() => false,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(rMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }
    }
}