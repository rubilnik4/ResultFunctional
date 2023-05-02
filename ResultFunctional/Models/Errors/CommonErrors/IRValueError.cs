using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.CommonErrors;

/// <summary>
/// Value error
/// </summary>
public interface IRValueError : IRBaseError<CommonErrorType>
{
    /// <summary>
    /// Value name
    /// </summary>
    string ValueName { get; }

    /// <summary>
    /// Value type
    /// </summary>
    Type ValueType { get; }
}