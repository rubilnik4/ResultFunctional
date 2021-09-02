using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.CommonErrors
{
    /// <summary>
    /// Ошибка неверного значения
    /// </summary>
    public interface IValueNotValidErrorResult : IErrorBaseResult<CommonErrorType>
    { }
}