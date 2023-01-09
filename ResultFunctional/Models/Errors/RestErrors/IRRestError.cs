using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.RestErrors
{
    /// <summary>
    /// Rest error
    /// </summary>
    public interface IRRestError : IRBaseError<RestErrorType>
    { }
}