using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.CommonErrors
{
    /// <summary>
    /// Ошибка дублирующего значения
    /// </summary>
    public interface IValueDuplicatedErrorResult : IErrorBaseResult<CommonErrorType>
    { }
}