using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;

namespace ResultFunctional.Models.Lists;

/// <summary>
/// Result with collection
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
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

    /// <summary>
    /// Initialize result by errors
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result option</returns>
    protected override IRList<TValue> Initialize(IReadOnlyCollection<IRError> errors) =>
        new RList<TValue>(errors);

    /// <summary>
    /// Initialize result by unit
    /// </summary>
    /// <returns>Result unit</returns>
    public static IRList<TValue> Some(IReadOnlyCollection<TValue> values) =>
        new RList<TValue>(values);

    /// <summary>
    /// Initialize result by error
    /// </summary>
    /// <param name="error">Error</param>
    /// <returns>Result unit</returns>
    public static IRList<TValue> None(IRError error) =>
        new RList<TValue>(error);
}