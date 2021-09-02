using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
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
            var resultValueFunc = new ResultValue<Func<int, string>>(CurryFunctions.IntToString);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(initialValue.ToString(), resultOut.Value.Invoke());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus()
        {
            var resultValueFunc = new ResultValue<Func<int, string>>(CurryFunctions.IntToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddOkStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, string>>(errorFunc);
            var resultArgument = new ResultValue<int>(2);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, int, string>>(CurryFunctions.AggregateTwoToString);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue + initialValue).ToString(), resultOut.Value.Invoke(2));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus_TwoArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, string>>(CurryFunctions.AggregateTwoToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
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
            var resultValueFunc = new ResultValue<Func<int, int, string>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, int, int, string>>(CurryFunctions.AggregateThreeToString);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 3).ToString(), resultOut.Value.Invoke(initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus_ThreeArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, int, string>>(CurryFunctions.AggregateThreeToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
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
            var resultValueFunc = new ResultValue<Func<int, int, int, string>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, int, int, int, string>>(CurryFunctions.AggregateFourToString);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 4).ToString(), resultOut.Value.Invoke(initialValue, initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus_FourArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, int, int, string>>(CurryFunctions.AggregateFourToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
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
            var resultValueFunc = new ResultValue<Func<int, int, int, int, string>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus_FourArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, int, int, int, int, string>>(CurryFunctions.AggregateFiveToString);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 5).ToString(), resultOut.Value.Invoke(initialValue, initialValue,
                                                                               initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_OkStatus_AddBadStatus_FiveArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, int, int, int, string>>(CurryFunctions.AggregateFiveToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
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
            var resultValueFunc = new ResultValue<Func<int, int, int, int, int, string>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOk_BadStatus_AddBadStatus_FiveArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }
    }
}