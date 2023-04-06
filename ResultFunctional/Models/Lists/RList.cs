using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Lists;

/// <summary>
/// Result with collection
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
internal class RList<TValue>: RValue<IReadOnlyCollection<TValue>>, IRList<TValue>
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
    /// Initialize result collection by errors
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result option</returns>
    protected override IRList<TValue> Initialize(IReadOnlyCollection<IRError> errors) =>
        new RList<TValue>(errors);

    /// <summary>
    /// Converting to result value
    /// </summary>
    /// <returns>Result value</returns>
    public IRValue<IReadOnlyCollection<TValue>> ToRValue() =>
        this;

    /// <summary>
    /// Initialize result collection by values
    /// </summary>
    /// <returns>Result collection</returns>
    public new static IRList<TValue> Some(IReadOnlyCollection<TValue> values) =>
        new RList<TValue>(values);

    /// <summary>
    /// Initialize result collection by error
    /// </summary>
    /// <param name="error">Error</param>
    /// <returns>Result collection</returns>
    public new static IRList<TValue> None(IRError error) =>
        new RList<TValue>(error);

    /// <summary>
    /// Initialize result collection by errors
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result collection</returns>
    public new static IRList<TValue> None(IReadOnlyCollection<IRError> errors) =>
        new RList<TValue>(errors);
}