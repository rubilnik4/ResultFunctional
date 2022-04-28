namespace ResultFunctional.Models.Enums
{
    /// <summary>
    /// Database error types
    /// </summary>
    public enum DatabaseErrorType
    {
        Connection,
        Save,
        TableAccess,
        ValueDuplicate,
        ValueNotFound,
        ValueNotValid,
    }
}