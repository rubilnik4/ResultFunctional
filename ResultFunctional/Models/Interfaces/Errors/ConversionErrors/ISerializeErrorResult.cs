using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.ConversionErrors
{
    /// <summary>
    /// Ошибка сериализации
    /// </summary>
    public interface ISerializeErrorResult: IErrorBaseResult<ConversionErrorType>
    { }
}