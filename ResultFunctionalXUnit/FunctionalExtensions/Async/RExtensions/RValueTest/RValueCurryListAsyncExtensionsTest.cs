﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка. Тесты
    /// </summary>
    public class RValueCurryListAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddOkStatus()
        {
            var initialValue = Collections.GetRangeNumber();
            var rValueFunc = CurryFunctions.IntCollectionToString.ToRValue();
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal(CurryFunctions.IntCollectionToString(initialValue), resultOut.GetValue());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddBadStatus()
        {
            var rValueFunc = CurryFunctions.IntCollectionToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddOkStatus()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, string>>(errorFunc);
            var resultArgument = RListFactory.SomeTask(Collections.GetRangeNumber());

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

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
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddOkStatus_TwoArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var rValueFunc = CurryFunctions.AggregateCollectionTwoToString.ToRValue();
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal(CurryFunctions.AggregateCollectionTwoToString(initialValue, 2), resultOut.GetValue().Invoke(2));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddBadStatus_TwoArguments()
        {
            var rValueFunc = CurryFunctions.AggregateCollectionTwoToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddOkStatus_TwoArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, int, string>>(errorFunc);
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

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
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddOkStatus_ThreeArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var rValueFunc = CurryFunctions.AggregateCollectionThreeToString.ToRValue();
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal(CurryFunctions.AggregateCollectionThreeToString(initialValue, 3, 3), resultOut.GetValue().Invoke(3, 3));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddBadStatus_ThreeArguments()
        {
            var rValueFunc = CurryFunctions.AggregateCollectionThreeToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddOkStatus_ThreeArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, int, int, string>>(errorFunc);
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

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
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddOkStatus_FourArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var rValueFunc = CurryFunctions.AggregateCollectionFourToString.ToRValue();
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal(CurryFunctions.AggregateCollectionFourToString(initialValue, 4, 4, 4), resultOut.GetValue().Invoke(4, 4, 4));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddBadStatus_FourArguments()
        {
            var rValueFunc = CurryFunctions.AggregateCollectionFourToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddOkStatus_FourArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, int, int, int, string>>(errorFunc);
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddBadStatus_FourArguments()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

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
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddOkStatus_FiveArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var rValueFunc = CurryFunctions.AggregateCollectionFiveToString.ToRValue();
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal(CurryFunctions.AggregateCollectionFiveToString(initialValue, 5, 5, 5, 5), resultOut.GetValue().Invoke(5, 5, 5, 5));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_OkStatus_AddBadStatus_FiveArguments()
        {
            var rValueFunc = CurryFunctions.AggregateCollectionFiveToString.ToRValue();
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddOkStatus_FiveArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, int, int, int, int, string>>(errorFunc);
            var resultArgument = RListFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkCollectionAsync_BadStatus_AddBadStatus_FiveArguments()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.None<Func<IReadOnlyCollection<int>, int, int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RListFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryListAsync(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }
    }
}