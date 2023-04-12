using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
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

    /// <summary>
    /// Проверить объект на нул. Без ошибок
    /// </summary>
    [Fact]
    public void ToResultCollectionNullCheck_Ok()
    {
        var collection = new List<string?> { "1", "2", "3" };

        var resultString = collection.ToRListNullCheck(CreateErrorTest());

        Assert.True(resultString.Success);
        Assert.True(collection.SequenceEqual(resultString.GetValue()));
    }

    /// <summary>
    /// Проверить объект на нул. Ошибка нулевого значения
    /// </summary>
    [Fact]
    public void ToResultCollectionNullCheck_ErrorCollectionNull()
    {
        List<string?>? collection = null;
        var initialError = CreateErrorTest();

        var resultString = collection.ToRListNullCheck(initialError);

        Assert.True(resultString.Failure);
        Assert.True(resultString.GetErrors().First().Equals(initialError));
    }

    /// <summary>
    /// Проверить объект на нул. Ошибка нулевого значения
    /// </summary>
    [Fact]
    public void ToResultCollectionNullCheck_ErrorNull()
    {
        var collection = new List<string?> { "1", null, "3" };
        var initialError = CreateErrorTest();

        var resultString = collection.ToRListNullCheck(initialError);

        Assert.True(resultString.Failure);
        Assert.True(resultString.GetErrors().First().Equals(initialError));
    }
}