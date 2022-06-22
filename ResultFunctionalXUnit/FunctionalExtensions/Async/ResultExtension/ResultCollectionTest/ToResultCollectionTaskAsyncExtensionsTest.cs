using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

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

        var resultValue = await resultCollectionTask.ToResultValueTaskAsync();

        Assert.IsAssignableFrom<IResultValue<IReadOnlyCollection<int>>>(resultValue);
    }
}