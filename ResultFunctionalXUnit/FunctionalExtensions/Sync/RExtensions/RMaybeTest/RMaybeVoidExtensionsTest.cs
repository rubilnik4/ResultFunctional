using System.Linq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RMaybeTest
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа. Тесты
    /// </summary>
    public class RMaybeVoidExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void RMaybeVoidOk_Ok_CallVoid()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.RMaybeVoidSome(() => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public void RMaybeVoidOk_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var rMaybe = initialError.ToRUnit();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = rMaybe.RMaybeVoidSome(() => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public void RMaybeVoidBad_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRUnit();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = rMaybe.RMaybeVoidNone(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public void RMaybeVoidBad_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRUnit();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = rMaybe.RMaybeVoidNone(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public void RMaybeVoidOkBad_Ok()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.RMaybeVoidMatch(() => voidObjectMock.Object.TestVoid(),
                                                                errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public void RMaybeVoidOkBad_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRUnit();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = rMaybe.RMaybeVoidMatch(() => voidObjectMock.Object.TestVoid(),
                                                                   errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void RMaybeVoidOkWhere_Ok_OkPredicate_CallVoid()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.RMaybeVoidOption(() => true,
                () => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void RMaybeVoidOkWhere_Ok_BadPredicate_NotCallVoid()
        {
            var resultOk = RUnitFactory.Some();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.RMaybeVoidOption(() => false,
                () => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public void RMaybeVoidOkWhere_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRUnit();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = rMaybe.RMaybeVoidOption(() => true,
                () => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public void RMaybeVoidOkWhere_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rMaybe = errorsInitial.ToRUnit();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = rMaybe.RMaybeVoidOption(() => false,
                () => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(rMaybe));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }
    }
}