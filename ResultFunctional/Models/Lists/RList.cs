using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;

namespace ResultFunctional.Models.Lists;

internal class RList<TValue>: ROption<IReadOnlyCollection<TValue>, IRList<TValue>>, IRList<TValue>
    where TValue: notnull
{
    protected RList(IReadOnlyCollection<TValue> values)
        : base(values)
    { }

    protected RList(IRError error)
        : this(error.ToList())
    { }

    protected RList(IReadOnlyCollection<IRError> errors)
        : base(errors)
    { }

    protected override IRList<TValue> Initialize(IReadOnlyCollection<IRError> errors) =>
        new RList<TValue>(errors);

    public static IRList<TValue> Some(IReadOnlyCollection<TValue> values) =>
        new RList<TValue>(values);

    public static IRList<TValue> None(IRError error) =>
        new RList<TValue>(error);
}