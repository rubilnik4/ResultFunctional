using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest;

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
        var resultNoError = new ResultError();
        var collection = new List<string> { "OkStatus" };

        var resultValue = resultNoError.ToResultCollection(collection);

        Assert.True(resultValue.OkStatus);
        Assert.True(collection.SequenceEqual(resultValue.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией с ошибкой
    /// </summary>      
    [Fact]
    public void ToResultCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = new ResultError(error);
        var collection = new List<string> { "BadStatus" };

        var resultValue = resultHasError.ToResultValue(collection);

        Assert.True(resultValue.HasErrors);
        Assert.Single(resultValue.Errors);
        Assert.True(error.Equals(resultValue.Errors.Last()));
    }

    /// <summary>
    /// Проверить объект на нул. Без ошибок
    /// </summary>
    [Fact]
    public void ToResultCollectionNullCheck_Ok()
    {
        var collection = new List<string?> { "1", "2", "3" };

        var resultString = collection.ToResultCollectionNullCheck(CreateErrorTest());

        Assert.True(resultString.OkStatus);
        Assert.True(collection.SequenceEqual(resultString.Value));
    }

    /// <summary>
    /// Проверить объект на нул. Ошибка нулевого значения
    /// </summary>
    [Fact]
    public void ToResultCollectionNullCheck_ErrorCollectionNull()
    {
        List<string?>? collection = null;
        var initialError = CreateErrorTest();

        var resultString = collection.ToResultCollectionNullCheck(initialError);

        Assert.True(resultString.HasErrors);
        Assert.True(resultString.Errors.First().Equals(initialError));
    }

    /// <summary>
    /// Проверить объект на нул. Ошибка нулевого значения
    /// </summary>
    [Fact]
    public void ToResultCollectionNullCheck_ErrorNull()
    {
        var collection = new List<string?> { "1", null, "3" };
        var initialError = CreateErrorTest();

        var resultString = collection.ToResultCollectionNullCheck(initialError);

        Assert.True(resultString.HasErrors);
        Assert.True(resultString.Errors.First().Equals(initialError));
    }
}