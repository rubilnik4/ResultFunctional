using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка. Тесты
    /// </summary>
    public class ResultValueCurryExtensionsTest
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = CurryFunctions.IntToString.ToRValue();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal(initialValue.ToString(), resultOut.GetValue().Invoke());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus()
        {
            var resultValueFunc = CurryFunctions.IntToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddOkStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, string>>();
            var resultArgument = 2.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, string>>();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = CurryFunctions.AggregateTwoToString.ToRValue();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal((initialValue + initialValue).ToString(), resultOut.GetValue().Invoke(2));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus_TwoArguments()
        {
            var resultValueFunc = CurryFunctions.AggregateTwoToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int,int, string>>();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, int, string>>();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = CurryFunctions.AggregateThreeToString.ToRValue();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal((initialValue * 3).ToString(), resultOut.GetValue().Invoke(initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus_ThreeArguments()
        {
            var resultValueFunc = CurryFunctions.AggregateThreeToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, int, int, string>>();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, int, int, string>>();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = CurryFunctions.AggregateFourToString.ToRValue();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal((initialValue * 4).ToString(), resultOut.GetValue().Invoke(initialValue, initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus_FourArguments()
        {
            var resultValueFunc = CurryFunctions.AggregateFourToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, int, int, int, string>>();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus_FourArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, int, int, int, string>>();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = CurryFunctions.AggregateFiveToString.ToRValue();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal((initialValue * 5).ToString(), resultOut.GetValue().Invoke(initialValue, initialValue,
                                                                               initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus_FiveArguments()
        {
            var resultValueFunc = CurryFunctions.AggregateFiveToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, int, int, int, int, string>>();
            var resultArgument = initialValue.ToRValue();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus_FiveArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = errorFunc.ToRValue<Func<int, int, int, int, int, string>>();
            var errorArgument = CreateErrorTest();
            var resultArgument = errorArgument.ToRValue<int>();

            var resultOut = resultValueFunc.RValueCurry(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }
    }
}