using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RUnitTest;

public class ResultErrorWhereASyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public void ResultErrorCheckErrorsOk_Ok_CheckNoError()
    {
        var resultError = RUnitFactory.Some(); ;

        var resultAfterWhere = resultError.ResultErrorCheckErrorsOk(() => true,
                                                                    CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public void ResultErrorCheckErrorsOk_Ok_CheckHasError()
    {
        var resultError = RUnitFactory.Some();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = resultError.ResultErrorCheckErrorsOk(() => false,
                                                                    () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public void ResultErrorCheckErrorsOk_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var resultError = errorInitial.ToRUnit();

        var resultAfterWhere = resultError.ResultErrorCheckErrorsOk(() => true,
                                                                    CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public void ResultErrorCheckErrorsOk_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var resultError = errorsInitial.ToRUnit();

        var resultAfterWhere = resultError.ResultErrorCheckErrorsOk(() => false,
                                                                    CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }
}