using System.Collections.Generic;
using System.Linq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultCollectionVoidExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOk_Ok_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultCollectionVoidOk(numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(initialCollection), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOk_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultError = initialError.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidOk(numbers => voidObjectMock.Object.TestNumbersVoid(numbers));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidBad_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidBad(
                errors => voidObjectMock.Object.TestNumbersVoid(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidBad_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidBad(
                errors => voidObjectMock.Object.TestNumbersVoid(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkBad_Ok()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultCollectionVoidOkBad(numbers => voidObjectMock.Object.TestNumbersVoid(numbers),
                                                                     _ => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.Equal(initialCollection, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(initialCollection), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkBad_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidOkBad(
                _ => voidObjectMock.Object.TestVoid(),
                errors => voidObjectMock.Object.TestNumbersVoid(new List<int> { errors.Count }));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkWhere_Ok_OkPredicate_CallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultCollectionVoidOkWhere(_ => true,
                number => voidObjectMock.Object.TestNumbersVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkWhere_Ok_BadPredicate_NotCallVoid()
        {
            var initialCollection = GetRangeNumber();
            var resultOk = initialCollection.ToRList();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultOk.ResultCollectionVoidOkWhere(_ => false,
                number => voidObjectMock.Object.TestNumbersVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOk));
            Assert.True(initialCollection.SequenceEqual(resultAfterVoid.GetValue()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkWhere_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidOkWhere(_ => true,
                number => voidObjectMock.Object.TestNumbersVoid(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public void ResultCollectionVoidOkWhere_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = errorsInitial.ToRList<int>();
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = resultError.ResultCollectionVoidOkWhere(_ => false,
                number => voidObjectMock.Object.TestNumbersVoid(number));

            Assert.True(resultAfterVoid.Equals(resultError));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumbersVoid(It.IsAny<IEnumerable<int>>()), Times.Never);
        }
    }
}