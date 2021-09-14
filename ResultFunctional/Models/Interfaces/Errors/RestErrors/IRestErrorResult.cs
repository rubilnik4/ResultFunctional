using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера
    /// </summary>
    public interface IRestErrorResult: IErrorBaseResult<RestErrorType, IRestErrorResult>
    { }
}