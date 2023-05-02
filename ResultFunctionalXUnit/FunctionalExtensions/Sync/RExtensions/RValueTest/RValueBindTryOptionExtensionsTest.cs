using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений. Тесты
    /// </summary>
    public class RValueBindTryOptionExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void RValueBindTryOk_OkResult_OkTry()
        {
            const int initialNumber = 2;
            var numberResult = initialNumber.ToRValue();

            var numberAfterTry = numberResult.RValueBindTrySome(number => Division(number).ToRValue(),
                                                                   Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(Division(initialNumber), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void RValueBindTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = numberResult.RValueBindTrySome(number => Division(number).ToRValue(),
                                                                   Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void RValueBindTryOk_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = initialNumber.ToRValue();

            var rValue = numberResult.RValueBindTrySome(number => Division(number).ToRValue(),
                                                                   Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void RValueBindTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = numberResult.RValueBindTrySome(number => Division(number).ToRValue(), 
                                                                   Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void RValueBindTryOkFunc_OkResult_OkTry()
        {
            const int initialNumber = 2;
            var numberResult = initialNumber.ToRValue();

            var numberAfterTry = numberResult.RValueBindTrySome(number => Division(number).ToRValue(),
                                                                   Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(Division(initialNumber), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void RValueBindTryOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = numberResult.RValueBindTrySome(number => Division(number).ToRValue(),
                                                                   Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void RValueBindTryOkFunc_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = initialNumber.ToRValue();

            var rValue = numberResult.RValueBindTrySome(number => Division(number).ToRValue(),
                                                                   Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void RValueBindTryOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = numberResult.RValueBindTrySome(number => Division(number).ToRValue(),
                                                                   Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}