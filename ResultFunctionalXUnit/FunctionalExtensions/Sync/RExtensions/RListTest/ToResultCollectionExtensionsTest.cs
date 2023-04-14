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
public class ToResultCollectionExtensionsTest
{
    /// <summary>
    /// Вернуть результирующий ответ с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public void ToResultCollection_OkStatus()
    {
        var resultNoError = RUnitFactory.Some();
        var collection = new List<string> { "OkStatus" };

        var resultValue = resultNoError.ToRList(collection);

        Assert.True(resultValue.Success);
        Assert.True(collection.SequenceEqual(resultValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией с ошибкой
    /// </summary>      
    [Fact]
    public void ToResultCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RUnitFactory.None(error);
        var collection = new List<string> { "BadStatus" };

        var resultValue = resultHasError.ToRList(collection);

        Assert.True(resultValue.Failure);
        Assert.Single(resultValue.GetErrors());
        Assert.True(error.Equals(resultValue.GetErrors().Last()));
    }
}