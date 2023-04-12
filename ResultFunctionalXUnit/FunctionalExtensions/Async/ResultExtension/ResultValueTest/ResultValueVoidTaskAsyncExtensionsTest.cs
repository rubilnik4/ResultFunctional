using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Interfaces;
using Moq;
using ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using ResultFunctional.Models.Factories;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением задачей-объектом.Тесты
    /// </summary>
    public class ResultCollectionVoidTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkTaskAsync_Ok_CallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOkTask = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultValueVoidOkTaskAsync(
                number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkTaskAsync_Bad_NotCallVoid()
        {
            var initialError = CreateErrorTest();
            var resultErrorTask = RValueFactory.NoneTask<int>(initialError);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultValueVoidOkTaskAsync(
                number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(resultAfterVoid.GetErrors().Last().Equals(initialError));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе без ошибок с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadTaskAsync_Ok_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultValueVoidBadTaskAsync(
                errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с ошибкой с негативным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidBadTaskAsync_Bad_CallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultValueVoidBadTaskAsync(
                errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Положительный вариант
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkBadBindAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var resultOk = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOk.ResultValueVoidOkBadTaskAsync(number => voidObjectMock.Object.TestNumberVoid(number),
                                                                _ => voidObjectMock.Object.TestVoid());

            Assert.True(resultAfterVoid.Equals(resultOk.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(initialValue), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения асинхронного действия при результирующем ответе. Негативный вариант
        /// </summary>
        [Fact]
        public async Task ResultValueVoidOkBadBindAsync_Bad()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultError = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultError.ResultValueVoidOkBadTaskAsync(_ => voidObjectMock.Object.TestVoid(),
                                                                errors => voidObjectMock.Object.TestNumberVoid(errors.Count));

            Assert.True(resultAfterVoid.Equals(resultError.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereTaskAsync_Ok_OkPredicate_CallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOkTask = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultValueVoidOkWhereTaskAsync(_ => true,
                number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката без ошибок с положительным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereTaskAsync_Ok_BadPredicate_NotCallVoid()
        {
            int initialValue = Numbers.Number;
            var resultOkTask = RValueFactory.SomeTask(initialValue);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultOkTask.ResultValueVoidOkWhereTaskAsync(_ => false,
                number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultOkTask.Result));
            Assert.Equal(initialValue, resultAfterVoid.GetValue());
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с положительным условием предиката без ошибок с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereTaskAsync_Bad_OkPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultValueVoidOkWhereTaskAsync(_ => true,
                number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }


        /// <summary>
        /// Проверка выполнения действия при результирующем ответе с отрицательным условием предиката с ошибкой с отрицательным условием
        /// </summary>
        [Fact]
        public async Task ResultVoidOkWhereTaskAsync_Bad_BadPredicate_NotCallVoid()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultErrorTask = RValueFactory.NoneTask<int>(errorsInitial);
            var voidObjectMock = new Mock<IVoidObject>();

            var resultAfterVoid = await resultErrorTask.ResultValueVoidOkWhereTaskAsync(_ => false,
                number => voidObjectMock.Object.TestNumberVoid(number));

            Assert.True(resultAfterVoid.Equals(resultErrorTask.Result));
            Assert.True(errorsInitial.SequenceEqual(resultAfterVoid.GetErrors()));
            voidObjectMock.Verify(voidObject => voidObject.TestNumberVoid(It.IsAny<int>()), Times.Never);
        }
    }
}