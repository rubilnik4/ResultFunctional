using ResultFunctional.Models.Constants;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Values;
using System.Collections.Generic;
using System.Linq;

namespace ResultFunctional.Models.Units;

/// <summary>
/// Result with unit value
/// </summary>
internal class RUnit : ROption<Unit, IRUnit>, IRUnit
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
}