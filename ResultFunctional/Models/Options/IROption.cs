using ResultFunctional.Models.Errors.Base;
using System.Collections.Generic;

namespace ResultFunctional.Models.Options;

public interface IROption<out TValue>
    where TValue : notnull
{
    public TValue? Value { get; }

    public IReadOnlyCollection<IRError> Errors { get; }
}