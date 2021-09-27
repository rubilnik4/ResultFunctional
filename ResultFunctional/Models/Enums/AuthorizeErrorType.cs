namespace ResultFunctional.Models.Enums
{
    /// <summary>
    /// Типы ошибок при авторизации
    /// </summary>
    public enum AuthorizeErrorType
    {
        Username,
        Password,
        PasswordConfirm,
        Email,
        Phone,
        Token,
        Register,
        Duplicate
    }
}