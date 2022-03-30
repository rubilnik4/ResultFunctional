using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.CommonErrors
{
    /// <summary>
    /// Not valid error subtype
    /// </summary>
    public interface IValueNotValidErrorResult : IErrorBaseResult<CommonErrorType>
    { }
}