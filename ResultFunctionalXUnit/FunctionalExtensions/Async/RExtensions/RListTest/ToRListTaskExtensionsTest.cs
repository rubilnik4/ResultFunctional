using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest;

/// <summary>
/// Методы расширения для результирующего ответа с коллекцией. Тесты
/// </summary>
public class ToRListTaskExtensionsTest
{
    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToRListTaskAsync_Enumerable_OkStatus()
    {
        var collection = GetRangeNumber();
        var resultNoError = RValueFactory.SomeTask<IEnumerable<int>>(collection);

        var rValue = await resultNoError.ToRListTask();

        Assert.True(rValue.Success);
        Assert.True(collection.SequenceEqual(rValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToRListTaskAsync_Enumerable_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RValueFactory.NoneTask<IEnumerable<int>>(error);

        var rValue = await resultHasError.ToRListTask();

        Assert.True(rValue.Failure);
        Assert.Single(rValue.GetErrors());
        Assert.True(error.Equals(rValue.GetErrors().Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToRListTaskAsync_IReadOnlyCollection_OkStatus()
    {
        var collection = GetRangeNumber();
        var resultNoError = RValueFactory.SomeTask(collection);

        var rValue = await resultNoError.ToRListTask();

        Assert.True(rValue.Success);
        Assert.True(collection.SequenceEqual(rValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToRListTaskAsync_IReadOnlyCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RValueFactory.NoneTask<IReadOnlyCollection<int>>(error);

        var rValue = await resultHasError.ToRListTask();

        Assert.True(rValue.Failure);
        Assert.Single(rValue.GetErrors());
        Assert.True(error.Equals(rValue.GetErrors().Last()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToRListTaskAsync_ReadOnlyCollection_OkStatus()
    {
        var collection = GetRangeNumber().ToList().AsReadOnly();
        var resultNoError = RValueFactory.SomeTask(collection);

        var rValue = await resultNoError.ToRListTask();

        Assert.True(rValue.Success);
        Assert.True(collection.SequenceEqual(rValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToRListTaskAsync_ReadOnlyCollection_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RValueFactory.NoneTask<ReadOnlyCollection<int>>(error);

        var rValue = await resultHasError.ToRListTask();

        Assert.True(rValue.Failure);
        Assert.Single(rValue.GetErrors());
        Assert.True(error.Equals(rValue.GetErrors().Last()));
    }


    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта с коллекцией без ошибок
    /// </summary>      
    [Fact]
    public async Task ToRListTaskAsync_List_OkStatus()
    {
        var collection = GetRangeNumber().ToList();
        var resultNoError = RValueFactory.SomeTask(collection);

        var rValue = await resultNoError.ToRListTask();

        Assert.True(rValue.Success);
        Assert.True(collection.SequenceEqual(rValue.GetValue()));
    }

    /// <summary>
    /// Вернуть результирующий ответ задачи-объекта со значением с ошибкой
    /// </summary>      
    [Fact]
    public async Task ToRListTaskAsync_List_HasErrors()
    {
        var error = CreateErrorTest();
        var resultHasError = RValueFactory.NoneTask<List<int>>(error);

        var rValue = await resultHasError.ToRListTask();

        Assert.True(rValue.Failure);
        Assert.Single(rValue.GetErrors());
        Assert.True(error.Equals(rValue.GetErrors().Last()));
    }

    /// <summary>
    /// Преобразовать в ответ со значением-коллекцией. Верно
    /// </summary>
    [Fact]
    public async Task ToRValue_Ok()
    {
        var numbers = Collections.GetRangeNumber();
        var rListTask = RListFactory.SomeTask(numbers);

        var rValue = await rListTask.ToRValueTask();

        Assert.IsAssignableFrom<IRValue<IReadOnlyCollection<int>>>(rValue);
    }

    /// <summary>
    ///  Преобразовать в результирующий ответ коллекции
    /// </summary>
    [Fact]
    public async Task ToRListTaskAsync()
    {
        var rangeTasks = Enumerable.Range(1, 10).Select(GetTaskNumber);
        var rList = await rangeTasks.ToRListTask();

        Assert.True(rList.Success);
        Assert.True(rList.GetValue().SequenceEqual(Enumerable.Range(1, 10)));
    }

    private static async Task<IRValue<int>> GetTaskNumber(int number)
    {
        await Task.Delay(1);
        return number.ToRValue();
    }
}