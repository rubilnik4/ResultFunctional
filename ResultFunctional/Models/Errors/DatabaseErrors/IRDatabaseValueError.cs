namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database value field error
    /// </summary>
    public interface IRDatabaseValueError : IDatabaseErrorResult
    {
        /// <summary>
        /// Значение в строковом виде
        /// </summary>
        string ValueToString { get; }
    }
}