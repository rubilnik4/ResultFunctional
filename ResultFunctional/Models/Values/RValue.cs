using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Options;
using System.Collections.Generic;
using System.Linq;

namespace ResultFunctional.Models.Values;

internal class RValue<TValue> : ROption<TValue, IRValue<TValue>>, IRValue<TValue>
    where TValue : notnull
{
    protected RValue(TValue value)
        : base(value)
    { }

    protected RValue(IRError error)
        : this(error.ToList())
    { }

    protected RValue(IReadOnlyCollection<IRError> errors)
        : base(errors)
    { }

    protected override IRValue<TValue> Initialize(IReadOnlyCollection<IRError> errors) =>
        new RValue<TValue>(errors);

    public static IRValue<TValue> Some(TValue value) =>
        new RValue<TValue>(value);

    public static IRValue<TValue> None(IRError error) =>
        new RValue<TValue>(error);
}