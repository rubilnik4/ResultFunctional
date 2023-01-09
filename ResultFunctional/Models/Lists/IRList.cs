using System.Collections.Generic;
using ResultFunctional.Models.Options;

namespace ResultFunctional.Models.Lists;

public interface IRList<out TValue> : IROption<IReadOnlyCollection<TValue>>
    where TValue : notnull
{ }