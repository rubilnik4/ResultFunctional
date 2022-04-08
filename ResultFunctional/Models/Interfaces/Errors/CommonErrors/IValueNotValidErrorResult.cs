using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.CommonErrors
{
    /// <summary>
    /// Not valid error
    /// </summary>
    public interface IValueNotValidErrorResult : IErrorBaseResult<CommonErrorType>
    { }
}