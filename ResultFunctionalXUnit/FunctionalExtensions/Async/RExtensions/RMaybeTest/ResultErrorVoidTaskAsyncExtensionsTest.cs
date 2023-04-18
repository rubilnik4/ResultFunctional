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
    /// Действие над внутренним типом результирующего ответа задачи-объекта. Тесты
    /// </summary>
    public class RMaybeVoidTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkTaskAsync_Ok_CallVoid()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidSomeTask(() => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkTaskAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var RMaybe = RUnitFactory.NoneTask(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await RMaybe.ToRMaybeTask().RMaybeVoidSomeTask(() => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(RMaybe.Result));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidBadTaskAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await RMaybe.ToRMaybeTask().RMaybeVoidNoneTask(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(RMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidBadTaskAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await RMaybe.ToRMaybeTask().RMaybeVoidNoneTask(errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(RMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadTaskAsync_Ok()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidMatchTask(() => voidObjectMock.Object.TestVoidAsync(),
                                                                errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkBadTaskAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await RMaybe.ToRMaybeTask().RMaybeVoidMatchTask(() => voidObjectMock.Object.TestVoidAsync(),
                                                                   errors => voidObjectMock.Object.TestNumberVoidAsync(errors.Count));

            Assert.True(resultAfterVoid.Equals(RMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereTaskAsync_Ok_OkPredicate_CallVoid()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidWhereTask(() => true,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereTaskAsync_Ok_BadPredicate_NotCallVoid()
        {
            var resultOk = RUnitFactory.SomeTask();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ToRMaybeTask().RMaybeVoidWhereTask(() => false,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereTaskAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await RMaybe.ToRMaybeTask().RMaybeVoidWhereTask(() => true,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(RMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestVoidAsync(), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task RMaybeVoidOkWhereTaskAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RMaybe = RUnitFactory.NoneTask(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await RMaybe.ToRMaybeTask().RMaybeVoidWhereTask(() => false,
                () => voidObjectMock.Object.TestVoidAsync());

            Assert.True(resultAfterVoid.Equals(RMaybe.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoidAsync(It.IsAny<int>()), Times.Never);
        }
    }
}