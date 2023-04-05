﻿using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;

/// <summary>
/// Result collections extension methods
/// </summary>
public static class ToResultCollectionExtensions
{
    /// <summary>
    /// Converting collection to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection</param>
    /// <returns>Outgoing result collection</returns>
    public static IRList<TValue> ToRList<TValue>(this IEnumerable<TValue> @this)
        where TValue : notnull =>
        RListFactory.Some(@this.ToList());

    /// <summary>
    /// Converting errors to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming errors</param>
    /// <returns>Outgoing result collection</returns>
    public static IRList<TValue> ToRList<TValue>(this IEnumerable<IRError> @this)
        where TValue : notnull =>
        RListFactory.None<TValue>(@this.ToList());

    /// <summary>
    /// Converting result unit to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result unit</param>
    /// /// <param name="values">Incoming collection</param>
    /// <returns>Outgoing result collection</returns>
    public static IRList<TValue> ToRList<TValue>(this IRUnit @this, IReadOnlyCollection<TValue> values)
        where TValue : notnull =>
        @this.Success 
            ? RListFactory.Some(values)
            : @this.GetErrors().ToRList<TValue>();

    /// <summary>
    /// Converting collection to result collection with null checking
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection</param>
    /// <param name="error">Null error</param>
    /// <returns>Outgoing result collection</returns>
    public static IRList<TValue> ToRListNullCheck<TValue>(this IEnumerable<TValue?>? @this, IRError error)
        where TValue : class =>
        @this != null
            ? @this.Select(value => value.ToRValueNullCheck(error)).ToRList()
            : error.ToRList<TValue>();

    /// <summary>
    /// Converting result with collection type to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming result value</param>
    /// <returns>Outgoing result collection</returns>
    public static IRList<TValue> ToRList<TValue>(this IRValue<IEnumerable<TValue>> @this)
        where TValue : notnull =>
        @this.Success
            ? @this.GetValue().ToRList()
            : @this.GetErrors().ToRList<TValue>();

    /// <summary>
    /// Converting collection of result value to result collection
    /// </summary>
    /// <typeparam name="TValue">Result type</typeparam>
    /// <param name="this">Incoming collection of result value</param>
    /// <returns>Outgoing result collection</returns>
    public static IRList<TValue> ToRList<TValue>(this IEnumerable<IRValue<TValue>> @this)
        where TValue : notnull =>
         @this.ToList().
         WhereContinue(collection => collection.All(result => result.Success),
                       collection => collection.Select(result => result.GetValue()).ToRList(),
                       collection => collection.SelectMany(result => result.GetErrors()).ToRList<TValue>());
}