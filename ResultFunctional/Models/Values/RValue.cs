using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Base;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Units;

namespace ResultFunctional.Models.Values;

/// <summary>
/// Result with value
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
internal class RValue<TValue> : RBase<TValue, IRValue<TValue>>, IRValue<TValue>
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

    /// <summary>
    /// Initialize result by value
    /// </summary>
    /// <param name="value">Value</param>
    /// <returns>Result option</returns>
    protected override IRValue<TValue> Initialize(TValue value) =>
        new RValue<TValue>(value);

    /// <summary>
    /// Initialize result by errors
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result option</returns>
    protected override IRValue<TValue> Initialize(IReadOnlyCollection<IRError> errors) =>
        new RValue<TValue>(errors);

    /// <summary>
    /// Initialize result by value
    /// </summary>
    /// <returns>Result value</returns>
    public static IRValue<TValue> Some(TValue value) =>
        new RValue<TValue>(value);

    /// <summary>
    /// Initialize result by error
    /// </summary>
    /// <param name="error">Error</param>
    /// <returns>Result value</returns>
    public static IRValue<TValue> None(IRError error) =>
        new RValue<TValue>(error);

    /// <summary>
    /// Initialize result by errors
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result value</returns>
    public static IRValue<TValue> None(IReadOnlyCollection<IRError> errors) =>
        new RValue<TValue>(errors);
}