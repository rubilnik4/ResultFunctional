using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.conversionErrors
{
    /// <summary>
    /// Ошибка десериализации
    /// </summary>
    public interface IDeserializeErrorResult : IErrorBaseResult<ConversionErrorType, IDeserializeErrorResult>
    { }
}