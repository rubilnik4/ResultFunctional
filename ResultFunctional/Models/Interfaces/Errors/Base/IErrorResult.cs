using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Interfaces.Errors.Base
{
    /// <summary>
    /// Ошибка результирующего ответа. Базовый класс
    /// </summary>
    public interface IErrorResult : IEnumerable<IErrorResult>
    {
        /// <summary>
        /// Идентификатор ошибки
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Исключение
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        /// Является ли типом ошибки
        /// </summary>
        bool IsErrorResult<TErrorType>()
            where TErrorType : IErrorResult;

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        bool HasErrorResult<TErrorType>()
            where TErrorType : IErrorResult;

        /// <summary>
        /// Добавить или заменить исключение
        /// </summary>
        IErrorResult AppendException(Exception exception);

        /// <summary>
        /// Преобразовать в ответ
        /// </summary>      
        IResultError ToResult();

        /// <summary>
        /// Преобразовать в ответ с вложенным типом
        /// </summary>      
        IResultValue<TValue> ToResultValue<TValue>();

        /// <summary>
        /// Преобразовать в ответ с вложенной коллекцией
        /// </summary>      
        IResultCollection<TValue> ToResultCollection<TValue>();
    }
}