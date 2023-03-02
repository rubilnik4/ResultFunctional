using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
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
    public void ToResultCollection_Default_OkStatus()
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
    public void ToResultCollection_Default_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = new ResultError(error);
        var collection = new List<string> { "BadStatus" };

        var resultValue = resultHasError.ToResultCollection(collection);

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

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public void ToResultCollection_OkStatus()
    {
        var collection = GetRangeNumber();
        var resultNoError = new ResultValue<IEnumerable<int>>(collection);

        var resultCollection = resultNoError.ToResultCollection();

        Assert.True(resultCollection.OkStatus);
        Assert.True(collection.SequenceEqual(resultCollection.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ со значением с ошибкой
    /// </summary>      
    [Fact]
    public void ToResultCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = new ResultValue<IEnumerable<int>>(error);

        var resultCollection = resultHasError.ToResultCollection();

        Assert.True(resultCollection.HasErrors);
        Assert.Single(resultCollection.Errors);
        Assert.True(error.Equals(resultCollection.Errors.Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public void ToResultCollection_Enumerable_OkStatus()
    {
        var collection = GetRangeNumber();
        var resultValues = collection.Select(value => value.ToResultValue());

        var resultCollection = resultValues.ToResultCollection();

        Assert.True(resultCollection.OkStatus);
        Assert.True(collection.SequenceEqual(resultCollection.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией с ошибкой
    /// </summary>      
    [Fact]
    public void ToResultCollection_Enumerable_HasErrors()
    {
        var error = CreateErrorTest();
        var collection = Enumerable.Range(0, 3).ToList();
        var resultValues = collection.Select(value => value.ToResultValue()).
                                      Append(new ResultValue<int>(error));

        var resultCollection = resultValues.ToResultCollection();

        Assert.True(resultCollection.HasErrors);
        Assert.Single(resultCollection.Errors);
        Assert.True(error.Equals(resultCollection.Errors.Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public void ToResultCollection_Collection_Enumerable_OkStatus()
    {
        var collection = Enumerable.Range(0, 3).ToList();
        var collections = Enumerable.Range(0, 3).Select(_ => collection).ToList();
        var aggregate = collections.SelectMany(value => value).ToList();
        var resultCollections = collections.Select(value => value.ToResultCollection());

        var resultCollection = resultCollections.ToResultCollection();

        Assert.True(resultCollection.OkStatus);
        Assert.True(aggregate.SequenceEqual(resultCollection.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией с ошибкой
    /// </summary>      
    [Fact]
    public void ToResultCollection_Collection_Enumerable_HasErrors()
    {
        var error = CreateErrorTest();
        var collection = Enumerable.Range(0, 3).ToList();
        var collections = Enumerable.Range(0, 3).Select(_ => collection).ToList();
        var resultValues = collections.Select(value => value.ToResultCollection()).
                                       Append(new ResultCollection<int>(error));

        var resultCollection = resultValues.ToResultCollection();

        Assert.True(resultCollection.HasErrors);
        Assert.Single(resultCollection.Errors);
        Assert.True(error.Equals(resultCollection.Errors.Last()));
    }
}