using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.RestErrors
{
    /// <summary>
    /// Rest error
    /// </summary>
    public interface IRRestError : IRBaseError<RestErrorType>
    { }
}