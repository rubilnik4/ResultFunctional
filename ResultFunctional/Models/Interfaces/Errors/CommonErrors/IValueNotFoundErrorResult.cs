using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.CommonErrors
{
    /// <summary>
    /// Not found error subtype
    /// </summary>
    public interface IValueNotFoundErrorResult: IErrorBaseResult<CommonErrorType>
    { }
}