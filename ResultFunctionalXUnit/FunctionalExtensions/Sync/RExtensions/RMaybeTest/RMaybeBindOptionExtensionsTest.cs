﻿using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RMaybeTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа. Тест
    /// </summary>
    public class RMaybeBindOptionExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public void RMaybeBindOkBad_Ok()
        {
            var initialResult = RUnitFactory.Some();
            var addingResult = RUnitFactory.Some();

            var result = initialResult.RMaybeBindMatch(() => addingResult,
                                                            _ => CreateErrorTest().ToRUnit());

            Assert.True(result.Success);
        }

        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public void RMaybeBindOkBad_Error()
        {
            var initialResult = CreateErrorListTwoTest().ToRUnit();
            var addingResult = RUnitFactory.Some();
            var addingResultBad = CreateErrorTest().ToRUnit();

            var result = initialResult.RMaybeBindMatch(() => addingResult,
                                                            _ => addingResultBad);

            Assert.True(result.Failure);
            Assert.Equal(addingResultBad.GetErrors().Count, result.GetErrors().Count);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void RMaybeBindOk_Ok_NoError()
        {
            var initialResult = RUnitFactory.Some();
            var addingResult = RUnitFactory.Some();

            var result = initialResult.RMaybeBindSome(() => addingResult);

            Assert.True(result.Success);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public void RMaybeBindOk_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.Some();
            var addingResult = initialError.ToRUnit();

            var result = initialResult.RMaybeBindSome(() => addingResult);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void RMaybeBindOk_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = initialError.ToRUnit();
            var addingResult = RUnitFactory.Some();

            var result = initialResult.RMaybeBindSome(() => addingResult);

            Assert.True(result.Failure);
            Assert.True(result.Equals(initialResult));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void RMaybeBindOk_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = initialError.ToRUnit();
            var addingResult = initialError.ToRUnit();

            var result = initialResult.RMaybeBindSome(() => addingResult);

            Assert.True(result.Failure);
            Assert.Single(result.GetErrors());
            Assert.True(result.Equals(initialResult));
        }
    }
}