# Functional Result Extensions for C&#35;
[![NuGet version (ResultFunctional)](https://img.shields.io/nuget/v/ResultFunctional.svg)](https://www.nuget.org/packages/ResultFunctional/)
[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/master/LICENSE)

Библиотека предназначена для разделения исключений и преднамеренных ошибок, а также написания кода во fluent стиле с элементами функционального программирования. Возможна работа как с синхронным, так и асинхронным кодом.
## Concepts
Библиотека представляет собой методы расширений. Объект переходит из одного состояния в дргое посредством лямда-выражений. Состоит из двух частей:
- Общая. Отвечает за стандартные функции и подходит для объектов всех типов.
- Специальная (RExtensions). Оперирует result-объектами данной библиотеки и позволяет писать код в функицональном fluent стиле.

Для каждой из функций предусмотрены синхронные и асинхронные расширения.
## Problems
- Разделение преднамеренных и непреднамеренных ошибок (exception);
- Устранение ошибок нулевого значения (null value);
- Устранение или изоляция побочных эффектов в методах (pure methods)
- Жесткое соответствие сигнатурам методом (primitive obsession)
- Неизменяемоесть (immutability)
## Common functions
### Summary
Feature | Description
| ------------ | ------------ |
`Map` | Функция преобразования из одного объекта в другой
`Option` | Функция преобразования одного объекта в другой в зависимости от условия
`Curry` | Трансформация в функцию с меньшим количеством входных аргументов
`Void` | Операции с методами
#### Map
Стандартная функция преобразования из типа A в тип B. Применяется в цепочках, чтобы не нарушать fluent стиль.
Feature | Signature | Description
| ------------ | ------------ | ------------ |
`Map` | (a, a => b) => b | Преобразование одного объекта в другой
```csharp
int number = 2;
string stringNumber = number.Map(convert => convert.ToString());
// or
int number = 2;
string stringNumber =number.ToString();
```
#### Option
Функция преобразования одного объекта в другой в зависимости от условия:
Возвращаемый тип объекта в обоих функциях одинаков.
Feature | Signature | Description
| ------------ | ------------ | ------------ |
`Option` | `(a, a => bool, a => b, a => b) => b` | Преобразование одного объекта в другой в зависимости от условия
`OptionSome` | `(a, a => bool, a => a) => a` | Изменение объекта при выполнении условия
`OptionNone` | `(a, a => bool, a => a) => a` | Изменение при невыполнении условия
```csharp
string stringNumber = "4";
int number = stringNumber
    .Option(parse => Int32.TryParse(parse, out _),
            parse => Int32.Parse(parse),
            _ => 0);
// or
string stringNumber = "4";
bool canParse = Int32.TryParse(parse, out _);
int number = canParse ? Int32.Parse(parse) :  0;
```
#### Curry
Функция предназначенная для уменьшения входных аргументов исходной функции (функций высокого порядка). Каррирование представляет собой частичное выполнение функции
Feature | Signature | Description
| ------------ | ------------ | ------------ |
`Curry` | `(a => t, a) => (() => t)` | Преобразование в функцию без аргументов
`Curry` | `((a, b) => t, a) => (b => t)` | Преобразование в функцию с одним аргументом
...
`Curry` | `((a1, a2... an) => t, a1) => (a2... an => t)` | Общий вид функции для уменьшения количества аргументов
```csharp
Func<int, int, int> func2 = (x, y) => x + y;
Func<int, int> func1 = func2.Curry(4);
Func<int> func = func1.Curry(5);
int result = func();
// or
Func<int, int, int> func2 = (x, y) => x + y;
int result = func2(4 + 5);
```
#### Void
В функциональном программировании отсутствуют методы не вовзращающие значений (void, action). В таких случаях их превращают в функции, вовзращающие тип Unit. Но в некоторых случаях, например для логирования, от этого правила можно отступить.
Feature | Signature | Description
| ------------ | ------------ | ------------ |
`Void` | `(a, a => ()) => a` | Выполнить действие над объектом
`VoidSome` | `(a, a => bool, a => ()) => a` | Выполнить действие над объектом при положительном условии
`VoidOption` | `(a, a => bool, a => (), a => ()) => a` | Выполнить действия над объектом в зависимости от условия
```csharp
int numeric = 1;
var result = numeric
    .VoidOption(number => number> 0,
                number => Console.WriteLine($"{number} is positive"),
                number => Console.WriteLine($"{number} is not negative"));
// or
int number = 1;
if (number > 0)
{
    Console.WriteLine($"{number} is positive");
}
else
{
    Console.WriteLine($"{number} is not negative");
}
```
### Conclusion
Сами по себе общие функции не приводят к улучшению читаемости кода. Однако они являются базой, на которой основаны дальнейшие функции.
## Result library
### Summary
Основным классом, используемым для методов расширений, является `IRMaybe`. Так же как и функциональный класс Option состоит из двух частией: `Some` и `None`. В качестве `Some` используются дженерики разных типов, `None` представляет собой различные вариации ошибок типа `IRError`. Все классы библиотеки имеют префикс `R`.
Class | Generic type | Base class | Description
| ------------ | ------------ | ------------ | ------------ |
`IRMaybe` | - | `-` | Базовый класс, определяющий наличие ошибки
`IRUnit` | Unit | `IRMaybe<Unit>` | Класс с отсутствующим дженериком
`IRValue` | Value | `IRMaybe<TValue>` | Класс, содержащий значение
`IRList` | Collection | `IRMaybe<IReadOnlyCollection<TValue>>` | Класс, содержащий коллекцию значений
### IRMaybe
Все классы наследники `IRMaybe` могут находиться в двух состояниях: `Success` и `Failure`. В случае `Success` класс возвращает хранимое значение дженерика, а в случае `Failure `- коллекцию ошибок. Нахождение в промежуточном состоянии, то есть хранении и значения и ошибок - исключено. 
Property/Method | Description
| ------------ | ------------ |
`Success` | Успешное состояние
`Failure` | Состояние с ошибками
`Value` | Значение хранимой переменной. В случае `Failure` - null
`Errors` | Хранимые ошибки. В случае `Success` - null
`GetValue` | Вернуть переменную. В случае `Failure` выбросить исключение
`GetErrors` | Вернуть список ошибок. В случае `Success` выбросить исключение
`GetErrorsOrEmpty` | Вернуть список ошибок. В случае `Success` вернуть пустой список
### Initialization
Инициализировать классы типа `IRMaybe` можно несколькими способами: через статические классы, через фабрику и через методы расширений. Если методы были применены к значению, то это приведет класс к состоянию `Success`. Если к ошибкам типа `IRError` - то к состоянию типа `Failure`.
#### Static classes
Каждый класс имеет статические методы `Some` и `None` для перехода в состояние `Success` и `Failure` соответсвенно.
Class | Feature | Signature| Status
| ------------ | ------------ | ------------ | ------------ |
`RUnit` | `RUnit.Some` | `_ => IRUnit` | Success
`RUnit` | `RUnit.None` | `IRError => IRUnit` | Failure
`RValue` | `RValue<T>.Some` | `T => IRValue<T>` | Success
`RValue` | `RValue<T>.None` | `IRError => IRValue<T>` | Failure
`RList` | `RList<T>.Some` | `IReadOnlyCollection<T> => IRList<T>` | Success
`RList` | `RList<T>.None` | `IRError => IRList<T>` | Failure
```csharp
int number = 2;
IRValue<int> result = RValue<int>.Some(2);
return result.Success; // true   
```  
```csharp
IRError error = RErrorFactory.Simple("Ошибка");
IRValue<int> result = RValue<int>.None(error);
return result.Success; // false     
```
#### Factory
Class | Feature | Signature| Status
| ------------ | ------------ | ------------ | ------------ |
`RUnitFactory` | `RUnitFactory.Some` | `_ => IRUnit` | Success
`RUnitFactory` | `RUnitFactory.None` | `IRError => IRUnit` | Failure
`RValueFactory` | `RValueFactory.Some<T>` | `T => IRValue<T>` | Success
`RValueFactory` | `RValueFactory.None<T>` | `IRError => IRValue<T>` | Failure
`RListFactory` | `RListFactory.Some<T>` | `IReadOnlyCollection<T> => IRList<T>` | Success
`RListFactory` | `RListFactory.None<T>` | `IRError => IRList<T>` | Failure
```csharp
List<int> collection = new List<int>() {1, 2, 3};
IRList<int> result = RListFactory.Some(collection);
return result.Success; // true     
``` 
```csharp
IRError error = RErrorFactory.Simple("Ошибка");
IRList<int> result = RListFactory<int>.None(error);
return result.Success; // false     
```
#### Extensions
Class | Feature | Signature| Status
| ------------ | ------------ | ------------ | ------------ |
`ToRValueExtensions` | `ToRValue<T>` | `T => IRValue<T>` | Success
`ToRValueExtensions` | `ToRValue<T>` | `IEnumerable<IRError> => IRValue<T>` | Failure
`ToRListExtensions` | `ToRList<T>` | `IReadOnlyCollection<T> => IRList<T>` | Success
`ToRListExtensions` | `ToRList<T>` | `IEnumerable<IRError> => IRList<T>` | Failure
```csharp
int number = 2;
IRValue<int> result = number.ToRValue();
return result.Success; // true   
```
```csharp  
IEnumerable<IRError> error = RErrorFactory.Simple("Ошибка").ToList();
IRValue<int> result = error.ToRValue<int>(error);
return result.Success; // false     
```
#### From IRError
Из ошибок типа `IRError` можно создавать R классы, содержащие эти же ошибки.
Class | Feature | Signature| Status
| ------------ | ------------ | ------------ | ------------ |
`IRError` | `ToRUnit` | `_ => IRUnit` | Failure
`IRError` | `ToRValue<T>` | `_ => IRValue<T>` | Failure
`IRError` | `ToRList<T>` | `_ => IRList<T>` | Failure
```csharp
IRError error = RErrorFactory.Simple("Ошибка");
IRValue<int> result = error.ToRValue<int>(error);
return result.Success; // false     
```
### IRUnit
Класс `IRUnit` представляет собой наиболее простой вариант использования `IRMaybe`. Используется в случаях, когда необходимо передать лишь факт наличия или отсутствия ошибки. В качестве значения `TValue` используется структура `Unit`.
```csharp
private IRMaybe CanUpdate(string field) =>
    !String.IsNullOrWhiteSpace(field)
        ? RUnit.Some()
        : RUnit.None(RErrorFactory.Simple("Empty string"));
        
private void Update(string field)
{
    var result = CanUpdate(field);
    if (result.Success)
    {
        Console.WriteLine("field is updated");
    }
    else
    {
        Console.WriteLine(result.GetErrors().First());
    }
}
```
### IRValue
Класс `IRValue` является самым распространенным вариантом использования `IRMaybe`. Он содержит в себе значение `Value`, которое может быть представленно классом `class` или структурой `struct`. `Value` не может иметь значение null.
```csharp
private IRValue<int> GetPositiveNumber(int number) =>
    number > 0
        ? number.ToRValue(number)
        : RErrorFactory.Simple("Non positive number").ToRValue();
        
private void SetNumber(int number)
{
    var resultNumber = GetPositiveNumber(number);
    if (resultNumber.Success)
    {
        Console.WriteLine($"number {resultNumber.GetValue()} is positive");
    }
    else
    {
        Console.WriteLine(result.GetErrors().First());
    }
}
```
### IRList
Класс `IRList` служит для упрощенной работы с коллекциями и списками. В качестве значения Value используется коллекция `IReadOnlyCollection<TValue>`. Коллекция не может иметь значение null, но может не иметь значение `Empty`.
```csharp
private IRList<int> GetPositiveList(IEnumarable<int> collection) =>
    collection
        .ToRListOption(numbers => numbers.All(number => number > 0),
                       _ => RErrorFactory.Simple("Collection has non positive values"));
        
private void SetNumbers(IEnumarable<int> collection)
{
    var resultNumbers = GetPositiveList(collection);
    if (resultNumbers.Success)
    {
        Console.WriteLine($"numbers {resultNumber.GetValue()} are positive");
    }
    else
    {
        Console.WriteLine(result.GetErrors().First());
    }
}
```
### Polymorphism
Каждый из вышеперечесленных классов можно напрямую преобразовать в `IRMaybe`. `IRMaybe` тоже можно преобразовать в другие классы с помощью специальных методов. Если классы находятся в статусе `Failure`, то ошибка передастся и в конвертируемые объекты.
From | To | Feature | Signature
| ------------ | ------------ | ------------ | ------------ |
`IRUnit` | `IRMaybe` | `(IRMaybe)IRUnit` | `_ => IRMaybe`
`IRValue` | `IRMaybe` | `(IRMaybe)IRValue` |  `_ => IRMaybe`
`IRList` | `IRMaybe` | `(IRMaybe)IRList` |  `_ => IRMaybe`
`IRMaybe` | `IRUnit` | `ToRUnit` | `_ => IRUnit`
`IRMaybe` | `IRValue` | `ToRValue<TValue>` | `TValue => IRValue<TValue>`
`IRMaybe` | `IRList` | `ToRList<TValue>` | `IReadOnlyCollection<TValue> => IRList<TValue>`
`IRList` | `IRValue` | `ToRValue` |  `_ => IRValue<IReadonlyCollection<TValue>>`
```csharp
IRMaybe maybe = RUnit.Some();
IRValue<string> rValue = maybe.ToRValue("value");
return rValue.Success; // true 
```
```csharp
IRMaybe maybe = RErrorFactory.Simple("Error").ToRUnit();
IRList<int> rList = maybe.ToRList(Enumerable.Range(0, 3).ToList());
return rValue.Success; // false 
```
```csharp
IRValue<string> rValue = "value".ToRValue();
IRValue<IReadOnlyCollection<int>> rList = rList.ToRValue(); 
return rValue.Success; // false 
```
### IRError
Объект `IRError` можно обозначить как `R` часть функционального `Either<L, R>` или же часть `None` функционального `Option`. `IRError` может содержать в себе `Exception`, если создан после возникновения исключения. `IRMaybe` в виде правой части содержит в себе коллекцию `IRError`.
Property/Method | Description
| ------------ | ------------ |
`Description` | Описание ошибки
`Exception` | Исключение
`AppendException` | Присвоить исключение
#### Error types
Ошибки можно классифицировать с помощью `IRBaseError<TErrorType>`, где `TErrorType` - любая произвольная структура. Библиотека содержит стандартные решения для некоторых типов ошибок.
Error | Error type | Derived types examples | Description
| ------------ | ------------ | ------------ | ------------ |
RCommonError | `CommonErrorType` | RSimpleError, IRValueNotFoundError, IRValueNullError | Общие ошибки
RAuthorizeError | `AuthorizeErrorType` | - | Ошибки авторизации
RConversionError | `ConversionErrorType` | IRDeserializeError, IRSerializeError, RJsonSchemeError | Ошибки конвертации
RDatabaseError | `DatabaseErrorType` | IRDatabaseAccessError, IRDatabaseValueNotFoundError, IRDatabaseValueNotValidError | Ошибки базы данных
RRestError | `RestErrorType` | RRestHostError, RRestMessageError, RRestTimeoutError | Ошибки rest сервисов
RTypeError | `TErrorType: struct` | - | Ошибка с произвольной структурой
#### Initialization
Ошибки стоит инициализировать через фабрику `RErrorFactory`.
```csharp
RSimpleError errorSimple = RErrorFactory.Simple("Error");

RRestMessageError errorRest = RErrorFactory.Rest(RestErrorType.BadRequest, "BadRequest" ,"Неверно сформированнный запрос"); 

string emptyString = null;
IRValueNullError errorValueNull = RErrorFactory.ValueNull(nameof(emptyString), "Not initialized");  

RTypeError<CommonErrorType> errorByType = RErrorFactory.ByType(CommonErrorType.Unknown, "Ошибка");
```
#### IRError Reflection
Существуют методы для определения типа классификации `IRError` ошибки.
Property/Method | Generic type | Description
| ------------ | ------------ | ------------ |
`IsError<TError>` | `TError: IRError` | Является ли текущим типом ошибки
`HasError<TError>` | `TError: IRError` | Является или наследуется от текущего типа
`HasErrorType<TErrorType>` | `TErrorType: struct` | Содержит ли в себе структуру ошибки
```csharp
string emptyString = null;
IRError notValidError = RErrorFactory.ValueNull(nameof(emptyString), "Not initialized");
bool isNullError = notValidError.IsError<IRValueNullError>(); // true
bool hasNullError = notValidError.HasError<IRValueNullError>(); // false
bool isValueError = notValidError.IsError<IRValueError>(); // true
bool hasValueError = notValidError.HasError<IRValueError>(); // true

IRError errorRest = RErrorFactory.Rest(RestErrorType.BadRequest, "BadRequest" ,"Неверно сформированнный запрос"); 
var isRestErrorType = errorRest.HasErrorType<RestErrorType>(); // true
var isBadRequestType = errorRest.HasErrorType(RestErrorType.BadRequest); // true
var isCommonErrorType = errorRest.HasErrorType<CommonErrorType>(); // false
var isNullValueType = errorRest.HasErrorType(CommonErrorType.NullArgument); // false
```
#### IRMaybe Reflection
В классе `IRMaybe` можно определить тип и классификацию ошибок `IRError`, если он находится в статусе `Failure`. Методы эквиваленты тем, что описаны для класса `IRError`, только применяются для всей коллекции ошибок. Если хоть одна ошибка сооветсвует запрашиваемым параметрам, метод возвращает `true`.
Property/Method | Generic type | Description
| ------------ | ------------ | ------------ |
`IsAnyError<TError>` | `TError: IRError` | Содержит ли текущий тип ошибки
`HasAnyError<TError>` | `TError: IRError` |  Содержит ли текущий или наследуемый тип ошибки
`HasAnyErrorType<TErrorType>` | `TErrorType: struct` | Содержит ли структуру ошибки
```csharp
IRError errorRest = RErrorFactory.Rest(RestErrorType.BadRequest, "BadRequest" ,"Неверно сформированнный запрос"); 
IRMaybe rMaybe = errorRest.ToRMaybe();
bool isValueError = rMaybe.IsAnyError<IRRestError>(); // false
bool hasNullError = rMaybe.HasAnyError<IRRestError>(); // true
var isBadRequestType = rMaybe.HasAnyErrorType(RestErrorType.BadRequest); // true
```
### Conclusion
Класс `IRMaybe` является оберткой для хранения переменных в состоянии Success или же хранения ошибок `IRError` в состояние `Failure`. В последствие в этим классам могут быть применены методы расширения `RExtensions` для последующей обработки данных.
## Result extensions
Методы расширения для библиотеки `RLibrary` предназначены для обработки значений `IRMaybe` в зависимости от статуса. Для каждого из классов (`IRUnit`, `IRValue`, `IRList`) существуют свои методы расширения.
### Сlassification
Все методы расширения можно разбить на функционльные части.
Example | Structure
| ------------ | ------------ |
`RValueBindOptionAsync` | `R(1)-Value(2)-Bind(3)-(4)-Option(5)-Async(6)`
`RMaybeTry` | `R(1)-Maybe(2)-(3)-Try(4)-(5)-(6)`
`RListVoidSomeAwait` | `R(1)-List(2)-(3)-Void(4)-Some(5)-Await(6)`
#### 1. Prefix
Префикс библиотеки `RExtensions`. Для `Common` методов расширения префикс отсутствует.
#### 2. RClass
Класс, для которого применяется метода расширения. Всего таких классов три: `IRUnit`, `IRValue`, `IRList`. Общие методы `IRMaybe` применимы для всех классов.
Extension | Apply to | Description
| ------------ | ------------ | ------------ |
`Maybe` | `IRMaybe`, `IRUnit`, `IRValue`, `IRList` | Общие методы расширения для всех классов
`Value` | `IRValue`, `IRList` | Методы расширения для классов, содержащих значения
`List` |  `IRList` | Методы расширения для классов, содержащих коллекции
#### 3. Function type
Тип функиции, определяющий будет ли использоваться в качестве параметра метода расширения значение переменной или же переменная в обертке `RLibrary`.
Extension | Function type | Signature | Description
| ------------ | ------------ | ------------ | ------------ |
`-` | Functor | `(R<T>, T => T) => R<T>` | Входная функция возвращает объект типа `T`
`Bind` | Monada | `(R<T>, T => R<T>) => R<T>` | Входная функция возвращает объект типа `R<T>`
#### 4. Action type
Тип действия. Для обычных методов расширения тип отсутсвует. Однако есть требуется применить void метод вместо функции или использовать обертку `Try/catch`, то необходимо указать специфический тип действия.
Extension | Signature | Description
| ------------ | ------------ | ------------ |
`-` | - | Отсутствует
`Void` | `R<T> => R<T>` | Выполнение `void` действия
`Try` | `Exception => R<T>` | Преобразование `Exception` в тип `IRError` при `Try/catch`
`Curry` | `(R<T => T>, R<T>) => () => R<T>` |  Уменьшение входных аргументов функции высокого порядка
`Lift` | `R<T> => T` | Разворачивание объекта из обертки `RLibrary`
`Fold` | `List<R<T>> => RList<T>` | Объединение `R` классов в коллекцию
#### 4. Status action
Действие в зависимости от статуса объекта `RLibrary`. Функции могут обрабатывать значения как в статусе `Success`, так и в статусе `Failure`.
Extension | Signature |  Description
| ------------ | ------------ | ------------ |
`Option` | `(R<T>, T => bool, T => T, T => IRError) => R<T>` | Только `Success`. При выполнении условия преобразует `T`, иначе возвращает ошибку `IRError`
`Where` | `(R<T>, T => bool, T => T, T => T) => R<T>` | Только `Success`. В зависимости от условия преобразует `T`
`Match` | `(R<T>, T => T, IRError => T) => R<T>` | При `Success` преобразует `T`. При Failure преобразует `IRError` в `T`
`Some` | `(R<T>, T => T) => R<T>` | Только `Success`. Преобразует `T`
`None` | `(R<T>, IRError => T) => R<T>` | Только `Failure`. Преобразует `IRError` в `T`
`Ensure` | `(R<T>, T => bool, T => IRError) => R<T>` | Проверяет статус. При `Failure` возвращает ошибку `IRError`   
### IRUnit extensions
### IRValue extensions
### IRList extensions
### Conclusion
