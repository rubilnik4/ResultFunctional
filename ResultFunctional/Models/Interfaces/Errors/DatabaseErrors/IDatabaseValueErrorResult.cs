namespace ResultFunctional.Models.Interfaces.Errors.DatabaseErrors
{
    /// <summary>
    /// Database value field error
    /// </summary>
    public interface IDatabaseValueErrorResult: IDatabaseErrorResult
    {
        /// <summary>
        /// Значение в строковом виде
        /// </summary>
        string ValueToString { get; }
    }
}