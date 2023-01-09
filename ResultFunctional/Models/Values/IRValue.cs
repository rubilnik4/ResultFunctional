using ResultFunctional.Models.Options;

namespace ResultFunctional.Models.Values;

public interface IRValue<out TValue>: IROption<TValue>
    where TValue : notnull
{ }