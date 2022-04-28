using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.CommonErrors
{
    /// <summary>
    /// Duplicate error
    /// </summary>
    public interface IValueDuplicateErrorResult : IErrorBaseResult<CommonErrorType>
    { }
}