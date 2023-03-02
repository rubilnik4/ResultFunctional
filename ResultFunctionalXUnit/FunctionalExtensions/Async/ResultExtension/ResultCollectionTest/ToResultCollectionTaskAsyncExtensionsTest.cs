using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.Results;
using Newtonsoft.Json.Linq;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest;

/// <summary>
/// Методы расширения для результирующего ответа с коллекцией. Тесты
/// </summary>
public class ToResultCollectionTaskAsyncExtensionsTest
{
    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_Enumerable_OkStatus()
    {
        var collection = GetRangeNumber();
        var resultNoError = ResultValueFactory.CreateTaskResultValue<IEnumerable<int>>(collection);

        var resultValue = await resultNoError.ToResultCollectionTaskAsync();

        Assert.True(resultValue.OkStatus);
        Assert.True(collection.SequenceEqual(resultValue.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_Enumerable_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = ResultValueFactory.CreateTaskResultValueError<IEnumerable<int>>(error);

        var resultValue = await resultHasError.ToResultCollectionTaskAsync();

        Assert.True(resultValue.HasErrors);
        Assert.Single(resultValue.Errors);
        Assert.True(error.Equals(resultValue.Errors.Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_IReadOnlyCollection_OkStatus()
    {
        var collection = GetRangeNumber();
        var resultNoError = ResultValueFactory.CreateTaskResultValue(collection);

        var resultValue = await resultNoError.ToResultCollectionTaskAsync();

        Assert.True(resultValue.OkStatus);
        Assert.True(collection.SequenceEqual(resultValue.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_IReadOnlyCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = ResultValueFactory.CreateTaskResultValueError<IReadOnlyCollection<int>>(error);

        var resultValue = await resultHasError.ToResultCollectionTaskAsync();

        Assert.True(resultValue.HasErrors);
        Assert.Single(resultValue.Errors);
        Assert.True(error.Equals(resultValue.Errors.Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_ReadOnlyCollection_OkStatus()
    {
        var collection = GetRangeNumber().ToList().AsReadOnly();
        var resultNoError = ResultValueFactory.CreateTaskResultValue(collection);

        var resultValue = await resultNoError.ToResultCollectionTaskAsync();

        Assert.True(resultValue.OkStatus);
        Assert.True(collection.SequenceEqual(resultValue.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_ReadOnlyCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = ResultValueFactory.CreateTaskResultValueError<ReadOnlyCollection<int>>(error);

        var resultValue = await resultHasError.ToResultCollectionTaskAsync();

        Assert.True(resultValue.HasErrors);
        Assert.Single(resultValue.Errors);
        Assert.True(error.Equals(resultValue.Errors.Last()));
    }


    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_List_OkStatus()
    {
        var collection = GetRangeNumber().ToList();
        var resultNoError = ResultValueFactory.CreateTaskResultValue(collection);

        var resultValue = await resultNoError.ToResultCollectionTaskAsync();

        Assert.True(resultValue.OkStatus);
        Assert.True(collection.SequenceEqual(resultValue.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_List_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = ResultValueFactory.CreateTaskResultValueError<List<int>>(error);

        var resultValue = await resultHasError.ToResultCollectionTaskAsync();

        Assert.True(resultValue.HasErrors);
        Assert.Single(resultValue.Errors);
        Assert.True(error.Equals(resultValue.Errors.Last()));
    }

    /// <summary>
    /// Преобразовать в ответ со значением-коллекцией. Верно
    /// </summary>
    [Fact]
    public async Task ToResultValue_Ok()
    {
        var numbers = Collections.GetRangeNumber();
        var resultCollectionTask = ResultCollectionFactory.CreateTaskResultCollection(numbers);

        var resultValue = await resultCollectionTask.ToResultValueFromCollectionTaskAsync();

        Assert.IsAssignableFrom<IResultValue<IReadOnlyCollection<int>>>(resultValue);
    }

    /// <summary>
    ///  Преобразовать в результирующий ответ коллекции
    /// </summary>
    [Fact]
    public async Task ToResultCollectionTaskAsync()
    {
        var rangeTasks = Enumerable.Range(1, 10).Select(number => Task.FromResult(number.ToResultValue()));
        var resultCollection = await rangeTasks.ToResultCollectionTaskAsync();

        Assert.True(resultCollection.OkStatus);
        Assert.True(resultCollection.Value.SequenceEqual(Enumerable.Range(1, 10)));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollection_Enumerable_OkStatus()
    {
        var collection = GetRangeNumber();
        var resultValues = collection.Select(value => value.ToResultValue()).GetCollectionTaskAsync();

        var resultCollection = await resultValues.ToResultCollectionTaskAsync();

        Assert.True(resultCollection.OkStatus);
        Assert.True(collection.SequenceEqual(resultCollection.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollection_Enumerable_HasErrors()
    {
        var error = CreateErrorTest();
        var collection = Enumerable.Range(0, 3).ToList();
        var resultValues = collection.Select(value => value.ToResultValue()).
                                      Append(new ResultValue<int>(error)).
                                      GetCollectionTaskAsync();

        var resultCollection = await resultValues.ToResultCollectionTaskAsync();

        Assert.True(resultCollection.HasErrors);
        Assert.Single(resultCollection.Errors);
        Assert.True(error.Equals(resultCollection.Errors.Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollection_Collection_Enumerable_OkStatus()
    {
        var collection = Enumerable.Range(0, 3).ToList();
        var collections = Enumerable.Range(0, 3).Select(_ => collection).ToList();
        var aggregate = collections.SelectMany(value => value).ToList();
        var resultCollections = collections.Select(value => value.ToResultCollection()).GetCollectionTaskAsync();

        var resultCollection = await resultCollections.ToResultCollectionTaskAsync();

        Assert.True(resultCollection.OkStatus);
        Assert.True(aggregate.SequenceEqual(resultCollection.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollection_Collection_Enumerable_HasErrors()
    {
        var error = CreateErrorTest();
        var collection = Enumerable.Range(0, 3).ToList();
        var collections = Enumerable.Range(0, 3).Select(_ => collection).ToList();
        var resultValues = collections.Select(value => value.ToResultCollection()).
                                       Append(new ResultCollection<int>(error))
                                      .GetCollectionTaskAsync();

        var resultCollection = await resultValues.ToResultCollectionTaskAsync();

        Assert.True(resultCollection.HasErrors);
        Assert.Single(resultCollection.Errors);
        Assert.True(error.Equals(resultCollection.Errors.Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_Collection_Enumerable_OkStatus()
    {
        var collection = Enumerable.Range(0, 3).ToList();
        var collections = Enumerable.Range(0, 3).Select(_ => collection).ToList();
        var aggregate = collections.SelectMany(value => value).ToList();
        var resultCollections = collections.Select(value => Task.FromResult(value.ToResultCollection()));

        var resultCollection = await resultCollections.ToResultCollectionTaskAsync();

        Assert.True(resultCollection.OkStatus);
        Assert.True(aggregate.SequenceEqual(resultCollection.Value));
    }

    /// <summary>
    /// Вернуть результирующий ответ с коллекцией с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_Collection_Enumerable_HasErrors()
    {
        var error = CreateErrorTest();
        var collection = Enumerable.Range(0, 3).ToList();
        var collections = Enumerable.Range(0, 3).Select(_ => collection).ToList();
        var resultValues = collections.Select(value => value.ToResultCollection()).
                                       Append(new ResultCollection<int>(error))
                                      .Select(Task.FromResult);

        var resultCollection = await resultValues.ToResultCollectionTaskAsync();

        Assert.True(resultCollection.HasErrors);
        Assert.Single(resultCollection.Errors);
        Assert.True(error.Equals(resultCollection.Errors.Last()));
    }
}