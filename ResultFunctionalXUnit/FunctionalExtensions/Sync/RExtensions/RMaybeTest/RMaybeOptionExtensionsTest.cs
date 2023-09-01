using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RMaybeTest;

public class RMaybeOptionExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public void RMaybeCheckErrorsOk_Ok_CheckNoError()
    {
        var rMaybe = RUnitFactory.Some();

        var resultAfterWhere = rMaybe.RMaybeEnsure(() => true, CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public void RMaybeCheckErrorsOk_Ok_CheckHasError()
    {
        var rMaybe = RUnitFactory.Some();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = rMaybe.RMaybeEnsure(() => false,
                                                                    () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public void RMaybeCheckErrorsOk_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var rMaybe = errorInitial.ToRUnit();

        var resultAfterWhere = rMaybe.RMaybeEnsure(() => true,
                                                                    CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public void RMaybeCheckErrorsOk_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var rMaybe = errorsInitial.ToRUnit();

        var resultAfterWhere = rMaybe.RMaybeEnsure(() => false,
                                                   CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public void RMaybeCollectErrorsOk_Ok_CheckNoError()
    {
        var rMaybe = RUnitFactory.Some();

        var resultAfterWhere = rMaybe.RMaybeConcat(() => true, CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public void RMaybeCollectErrorsOk_Ok_CheckHasError()
    {
        var rMaybe = RUnitFactory.Some();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = rMaybe.RMaybeConcat(() => false,
                                                   () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public void RMaybeCollectErrorsOk_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var rMaybe = errorInitial.ToRUnit();

        var resultAfterWhere = rMaybe.RMaybeConcat(() => true,
                                                   CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public void RMaybeCollectErrorsOk_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var rMaybe = errorsInitial.ToRUnit();
        var errors = CreateErrorListTwoTest();

        var resultAfterWhere = rMaybe.RMaybeConcat(() => false,
                                                    () => errors);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errors.Count + 1, resultAfterWhere.GetErrors().Count);
    }
}