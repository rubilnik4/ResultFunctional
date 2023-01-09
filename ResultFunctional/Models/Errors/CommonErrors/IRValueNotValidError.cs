﻿using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Not valid error
    /// </summary>
    public interface IRValueNotValidError : IRBaseError<CommonErrorType>
    { }
}