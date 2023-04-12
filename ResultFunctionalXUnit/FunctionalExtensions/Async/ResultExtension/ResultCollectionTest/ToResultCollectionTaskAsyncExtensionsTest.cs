using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Values;

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
        var resultNoError = RValueFactory.SomeTask<IEnumerable<int>>(collection);

        var resultValue = await resultNoError.ToRListTaskAsync();

        Assert.True(resultValue.Success);
        Assert.True(collection.SequenceEqual(resultValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_Enumerable_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RValueFactory.NoneTask<IEnumerable<int>>(error);

        var resultValue = await resultHasError.ToRListTaskAsync();

        Assert.True(resultValue.Failure);
        Assert.Single(resultValue.GetErrors());
        Assert.True(error.Equals(resultValue.GetErrors().Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_IReadOnlyCollection_OkStatus()
    {
        var collection = GetRangeNumber();
        var resultNoError = RValueFactory.SomeTask(collection);

        var resultValue = await resultNoError.ToRListTaskAsync();

        Assert.True(resultValue.Success);
        Assert.True(collection.SequenceEqual(resultValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_IReadOnlyCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RValueFactory.NoneTask<IReadOnlyCollection<int>>(error);

        var resultValue = await resultHasError.ToRListTaskAsync();

        Assert.True(resultValue.Failure);
        Assert.Single(resultValue.GetErrors());
        Assert.True(error.Equals(resultValue.GetErrors().Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_ReadOnlyCollection_OkStatus()
    {
        var collection = GetRangeNumber().ToList().AsReadOnly();
        var resultNoError = RValueFactory.SomeTask(collection);

        var resultValue = await resultNoError.ToRListTaskAsync();

        Assert.True(resultValue.Success);
        Assert.True(collection.SequenceEqual(resultValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_ReadOnlyCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RValueFactory.NoneTask<ReadOnlyCollection<int>>(error);

        var resultValue = await resultHasError.ToRListTaskAsync();

        Assert.True(resultValue.Failure);
        Assert.Single(resultValue.GetErrors());
        Assert.True(error.Equals(resultValue.GetErrors().Last()));
    }


    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_List_OkStatus()
    {
        var collection = GetRangeNumber().ToList();
        var resultNoError = RValueFactory.SomeTask(collection);

        var resultValue = await resultNoError.ToRListTaskAsync();

        Assert.True(resultValue.Success);
        Assert.True(collection.SequenceEqual(resultValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToResultCollectionTaskAsync_List_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RValueFactory.NoneTask<List<int>>(error);

        var resultValue = await resultHasError.ToRListTaskAsync();

        Assert.True(resultValue.Failure);
        Assert.Single(resultValue.GetErrors());
        Assert.True(error.Equals(resultValue.GetErrors().Last()));
    }

    /// <summary>
    /// Преобразовать в ответ со значением-коллекцией. Верно
    /// </summary>
    [Fact]
    public async Task ToResultValue_Ok()
    {
        var numbers = Collections.GetRangeNumber();
        var resultCollectionTask = RListFactory.SomeTask(numbers);

        var resultValue = await resultCollectionTask.ToRValueFromCollectionTaskAsync();

        Assert.IsAssignableFrom<IRValue<IReadOnlyCollection<int>>>(resultValue);
    }

    /// <summary>
    ///  Преобразовать в результирующий ответ коллекции
    /// </summary>
    [Fact]
    public async Task ToResultCollectionTaskAsync()
    {
        var rangeTasks = Enumerable.Range(1, 10).Select(GetTaskNumber);
        var resultCollection = await rangeTasks.ToRListTaskAsync();

        Assert.True(resultCollection.Success);
        Assert.True(resultCollection.GetValue().SequenceEqual(Enumerable.Range(1, 10)));
    }

    private static async Task<IRValue<int>> GetTaskNumber(int number)
    {
        await Task.Delay(1);
        return number.ToRValue();
    }
}