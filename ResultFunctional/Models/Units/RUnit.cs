using ResultFunctional.Models.Constants;
using ResultFunctional.Models.Values;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Base;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.Models.Units;

/// <summary>
/// Result with unit value
/// </summary>
internal class RUnit : RBase<Unit, IRUnit>, IRUnit
{
    protected RUnit()
        : base(Unit.Value)
    { }

    protected RUnit(IRError error)
        : this(error.ToList())
    { }

    protected RUnit(IReadOnlyCollection<IRError> errors)
        : base(errors)
    { }

    /// <summary>
    /// Initialize result by value
    /// </summary>
    /// <returns>Result option</returns>
    protected override IRUnit Initialize(Unit _) =>
        new RUnit();

    /// <summary>
    /// Initialize result by errors
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result unit</returns>
    protected override IRUnit Initialize(IReadOnlyCollection<IRError> errors) =>
        new RUnit(errors);

    /// <summary>
    /// Initialize result by unit
    /// </summary>
    /// <returns>Result unit</returns>
    public static IRUnit Some() =>
        new RUnit();

    /// <summary>
    /// Initialize result by error
    /// </summary>
    /// <param name="error">Error</param>
    /// <returns>Result unit</returns>
    public static IRUnit None(IRError error) =>
        new RUnit(error);

    /// <summary>
    /// Initialize result by errors
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result unit</returns>
    public static IRUnit None(IReadOnlyCollection<IRError> errors) =>
        new RUnit(errors);
}