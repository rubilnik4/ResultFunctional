using System.Linq;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа. Тесты
    /// </summary>
    public class ResultErrorVoidExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultErrorVoidOk_Ok_CallVoid()
        {
            var resultOk = new ResultError();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultErrorVoidOk(() => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public void ResultErrorVoidOk_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultError = new ResultError(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultErrorVoidOk(() => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(resultAfterVoid.Errors.Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public void ResultErrorVoidBad_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultError(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultErrorVoidBad(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public void ResultErrorVoidBad_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultError(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultErrorVoidBad(errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public void ResultErrorVoidOkBad_Ok()
        {
            var resultOk = new ResultError();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultErrorVoidOkBad(() => voidObjectMock.Object.TestVoid(),
                                                                errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public void ResultErrorVoidOkBad_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultError(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultErrorVoidOkBad(() => voidObjectMock.Object.TestVoid(),
                                                                   errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultErrorVoidOkWhere_Ok_OkPredicate_CallVoid()
        {
            var resultOk = new ResultError();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultErrorVoidOkWhere(() => true,
                action: () => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultErrorVoidOkWhere_Ok_BadPredicate_NotCallVoid()
        {
            var resultOk = new ResultError();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultErrorVoidOkWhere(() => false,
                action: () => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public void ResultErrorVoidOkWhere_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultError(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultErrorVoidOkWhere(() => true,
                action: () => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public void ResultErrorVoidOkWhere_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = new ResultError(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultErrorVoidOkWhere(() => false,
                action: () => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.Errors));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }
    }
}