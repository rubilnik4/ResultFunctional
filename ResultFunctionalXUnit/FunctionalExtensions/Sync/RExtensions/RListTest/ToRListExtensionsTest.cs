using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest;

/// <summary>
/// Преобразование значений в результирующий ответ коллекции. Тесты
/// </summary>
public class ToRListExtensionsTest
{
    /// <summary>
    /// Вернуть результирующий ответ с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public void ToRList_OkStatus()
    {
        var resultNoError = RUnitFactory.Some();
        var collection = new List<string> { "OkStatus" };

        var RValue = resultNoError.MaybeRList(collection);

        Assert.True(RValue.Success);
        Assert.True(collection.SequenceEqual(RValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией с ошибкой
    /// </summary>      
    [Fact]
    public void ToRList_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RUnitFactory.None(error);
        var collection = new List<string> { "BadStatus" };

        var RValue = resultHasError.MaybeRList(collection);

        Assert.True(RValue.Failure);
        Assert.Single(RValue.GetErrors());
        Assert.True(error.Equals(RValue.GetErrors().Last()));
    }
}