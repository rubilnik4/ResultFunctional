using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений. Тесты
    /// </summary>
    public class RValueTryOptionExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void RValueTryOk_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = initialValue.ToRValue();

            var numberAfterTry = numberResult.RValueTrySome(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(Division(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void RValueTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = numberResult.RValueTrySome(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void RValueTryOk_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = initialValue.ToRValue();

            var rValue = numberResult.RValueTrySome(Division, Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void RValueTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = numberResult.RValueTrySome(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void RValueTryOkFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = initialValue.ToRValue();

            var numberAfterTry = numberResult.RValueTrySome(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(Division(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void RValueTryOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = numberResult.RValueTrySome(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void RValueTryOkFunc_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = initialValue.ToRValue();

            var rValue = numberResult.RValueTrySome(Division, Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void RValueTryOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = numberResult.RValueTrySome(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}