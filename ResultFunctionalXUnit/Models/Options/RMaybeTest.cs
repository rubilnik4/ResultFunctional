using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.Models.Options;

public class RMaybeTest
{
    /// <summary>
    /// Перевод в RUnit
    /// </summary>
    [Fact]
    public void ToRUnit_Ok()
    {
        var rUnit = RUnitFactory.Some();

        var rUnitNew = rUnit.ToRUnit();

        Assert.True(rUnitNew.Success);
    }

    /// <summary>
    /// Перевод в RUnit с ошибкой
    /// </summary>
    [Fact]
    public void ToRUnit_Error()
    {
        var error = CreateErrorTest();
        var rUnit = RUnitFactory.None(error);

        var rUnitNew = rUnit.ToRUnit();

        Assert.True(rUnitNew.Failure);
        Assert.True(error.Equals(rUnitNew.GetErrors().First()));
    }

    /// <summary>
    /// Перевод в RUnit с ошибкой
    /// </summary>
    [Fact]
    public void ToRUnit_Error_Exception()
    {
        IReadOnlyCollection<IRError> error = null!;

        Assert.Throws<ArgumentNullException>(() => RUnitFactory.None(error));
    }

    /// <summary>
    /// Перевод в RValue
    /// </summary>
    [Fact]
    public void ToRValue_Ok()
    {
        const int number = 2;
        var rUnit = RUnitFactory.Some();

        var rUnitNew = rUnit.ToRValue(number);

        Assert.True(rUnitNew.Success);
    }

    /// <summary>
    /// Перевод в RValue с ошибкой
    /// </summary>
    [Fact]
    public void ToRValue_Error()
    {
        const int number = 2;
        var error = CreateErrorTest();
        var rUnit = RUnitFactory.None(error);

        var rUnitNew = rUnit.ToRValue(number);

        Assert.True(rUnitNew.Failure);
        Assert.True(error.Equals(rUnitNew.GetErrors().First()));
    }

    /// <summary>
    /// Перевод в RLIst
    /// </summary>
    [Fact]
    public void ToRList_Ok()
    {
        var collection = GetRangeNumber();
        var rUnit = RUnitFactory.Some();

        var rUnitNew = rUnit.ToRList(collection);

        Assert.True(rUnitNew.Success);
    }

    /// <summary>
    /// Перевод в RLIst с ошибкой
    /// </summary>
    [Fact]
    public void ToRList_Error()
    {
        var collection = GetRangeNumber();
        var error = CreateErrorTest();
        var rUnit = RUnitFactory.None(error);

        var rUnitNew = rUnit.ToRList(collection);

        Assert.True(rUnitNew.Failure);
        Assert.True(error.Equals(rUnitNew.GetErrors().First()));
    }
}