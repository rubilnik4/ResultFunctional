namespace ResultFunctional.Models.Interfaces.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка поля в базе данных
    /// </summary>
    public interface IDatabaseValueErrorResult: IDatabaseErrorResult
    {
        /// <summary>
        /// Значение в строковом виде
        /// </summary>
        public string ValueToString { get; }
    }
}