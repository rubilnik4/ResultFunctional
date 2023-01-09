using ResultFunctional.Models.Constants;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Values;
using System.Collections.Generic;
using System.Linq;

namespace ResultFunctional.Models.Units;

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

    protected override IRUnit Initialize(IReadOnlyCollection<IRError> errors) =>
        new RUnit(errors);

    public static IRUnit Some() =>
        new RUnit();

    public static IRUnit None(IRError error) =>
        new RUnit(error);
}