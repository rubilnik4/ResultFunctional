namespace ResultFunctional.Models.Enums
{
    /// <summary>
    /// Ошибки rest сервиса
    /// </summary>
    public enum RestErrorType
    {
        ServerNotFound,
        BadGateway,
        BadRequest,
        GatewayTimeout,
        InternalServerError,
        RequestTimeout,
        RequestEntityToLarge,
        ValueNotFound,
        Unauthorized,
        UnsupportedMediaType,
        UnknownRestStatus,
    }
}