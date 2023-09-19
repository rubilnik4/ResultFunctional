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
string stringNumber = number.ToString();
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
`-` | - | Общие методы
`Void` | `R<T> => R<T>` | Выполнение `void` действия
`Try` | `Exception => R<T>` | Преобразование `Exception` в тип `IRError` при `Try/catch`
`Curry` | `(R<T => T>, R<T>) => () => R<T>` |  Уменьшение входных аргументов функции высокого порядка
`Lift` | `R<T> => T` | Разворачивание объекта из обертки `RLibrary`
`Fold` | `List<R<T>> => RList<T>` | Объединение `R` классов в коллекцию
#### 5. Status action
Действие в зависимости от статуса объекта `RLibrary`. Функции могут обрабатывать значения как в статусе `Success`, так и в статусе `Failure`.
При всех действиях за исключением `None` и `Match` функция для `R` объекта исполняется только в статусе `Success`. При статусе `Failure` объект остается неизменным и происходит пропуск шага.
Extension | Signature |  Description
| ------------ | ------------ | ------------ |
`Option` | `(R<T>, T => bool, T => T, T => IRError) => R<T>` | Только `Success`. При выполнении условия преобразует `T`, иначе возвращает ошибку `IRError`
`Where` | `(R<T>, T => bool, T => T, T => T) => R<T>` | Только `Success`. В зависимости от условия преобразует `T`
`Match` | `(R<T>, T => T, IRError => T) => R<T>` | При `Success` преобразует `T`. При `Failure` преобразует `IRError` в `T`
`Some` | `(R<T>, T => T) => R<T>` | Только `Success`. Преобразует `T`
`None` | `(R<T>, IRError => T) => R<T>` | Только `Failure`. Преобразует `IRError` в `T`
`Ensure` | `(R<T>, T => bool, T => IRError) => R<T>` | Проверяет статус. При `Failure` возвращает ошибку `IRError`
`Concat` | `(R<T>, T => bool, T => IRError) => R<T>` | При `Failure` добавляет ошибку `IRError` к текущим
#### 6. Asynchronous
Каждый из методов имеет асинхронное расширение. Асинхронные методы можно использовать в классическом написании с применением `await` или же во fluent стиле с примнением расширений: `Async`, `Task`, `Await`.
Extension | Signature |  Description
| ------------ | ------------ | ------------ |
`Async` | `(T, T => Task<T>) => Task<T>` | Исполняемая асинхронная функция
`Task` | `(Task<T>, T => T) => Task<T>` | Объект в обертке-задаче `Task`
`Await` | `(Task<T>, T => Task<T>) => Task<T>` | Исполняемая асинхронная функция для задачи `Task`
```csharp
private int AddSync(int x, int y) =>
    x + y;
    
private async Task<int> AddAsync(int x, int y) =>
    await Task.FromResult(x + y);
```
```csharp
private async Task<IRValue<int>> Classic(int initial, int additional)
{
    var initialR = initial.ToRValue();
    var firstR = await initialR.RValueSomeAsync(number => AddAsync(number, additional));
    var secondR = firstR.RValueSome(number => AddSync(number, additional));
    var thirdR = await secondR.RValueSomeAsync(number => AddSync(number, additional));
    return thirdR;
}
```
```csharp
private async Task<IRValue<int>> Fluent(int initial, int additional) =>
    await initial
        .ToRValue()
        .RValueSomeAsync(number => AddAsync(number, additional))
        .RValueSomeTask(number => AddSync(number, additional))
        .RValueSomeAwait(number => AddAsync(number, additional));
```
### Abbreviations
Список сокращений для таблиц сигнатур
Extension | Abbreviation
| ------------ | ------------
`IRMaybe` | `RM`
`IRValue` | `RV`
`IRList` | `RL`
`IRError` | `RE`
`Exception` | `Ex`
`Func` | `F`
`Action` | `A`
`List` | `L`
### IRMaybe extensions
Методы расширения `IRMaybe` применимы ко всем типам классов. Они отвечают за обработку и преобразование ошибок 'IRError' в зависимости от статуса.
#### Function and action types
Методы расширения типа функтор не имеют дополнительного индекса в именованивании. Расширения типа монада обозначаются префиксом `Bind`.
Action | Functor | Monad
| ------------ | ------------ | ------------
`Main` | :heavy_check_mark: | :heavy_check_mark:
`Try` | :heavy_check_mark: |
`Fold` | :heavy_check_mark: |
`Void` | :heavy_check_mark: |
#### Main action type
Общие методы расширения класса `IRMaybe`. Позволяют добавлять и обрабатывать ошибки 'IRError'.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RMaybeEnsure` | `(RM, () => bool, () => RE) => RM`
2 | `RMaybeConcat` | `(RM, () => bool, () => RE) => RM`
3 | `RMaybeBindMatch` | `(RM, () => RM, RE => RM) => RM`
4 | `RMaybeBindSome` | `(RM, () => RM) => RM`
##### 1. RMaybeEnsure
Проверить статус, проверить условие и присвоить ошибку в случае невыполнения.
```
(IRMaybe, () => bool, () => IRError) => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeEnsure(() => true,
                      () => RErrorFactory.Simple("First error"))
        .RMaybeEnsure(() => true,
                      () => RErrorFactory.Simple("Second error"));
// Success
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeEnsure(() => false,
                      () => RErrorFactory.Simple("First error"))
        .RMaybeEnsure(() => true,
                      () => RErrorFactory.Simple("Second error"));
// Failure. Errors: first
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.None(RErrorFactory.Simple("Initial error"))
        .RMaybeEnsure(() => false,
                      () => RErrorFactory.Simple("First error"))
        .RMaybeEnsure(() => true,
                      () => RErrorFactory.Simple("Second error"));
// Failure. Errors: initial
```
##### 2. RMaybeConcat
```
(IRMaybe, () => bool, () => IRError) => IRMaybe
```
Проверить условие и добавить ошибку к текущим. Методы типа `Concat` не выполняют пропуск шагов в состоянии `Failure` и исполняются вне зависимости от их статуса. Такие методы хорошо подходят для сбора ошибок на wpf формах.
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeConcat(() => true,
                      () => RErrorFactory.Simple("First error"))
        .RMaybeConcat(() => true,
                      () => RErrorFactory.Simple("Second error"));
// Success
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeConcat(() => false,
                      () => RErrorFactory.Simple("First error"))
        .RMaybeConcat(() => false,
                      () => RErrorFactory.Simple("Second error"));
// Failure. Errors: first, second
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.None(RErrorFactory.Simple("Initial error"))
        .RMaybeEnsure(() => true,
                      () => RErrorFactory.Simple("First error"))
        .RMaybeEnsure(() => false,
                      () => RErrorFactory.Simple("Second error"));
// Failure. Errors: initial, second
```
##### 3. RMaybeBindMatch
Заменить класс в зависимости от статуса
```
(IRMaybe, () => IRMaybe, IRError => IRMaybe) => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeBindMatch(() => RUnitFactory.Some(),
                         () => RErrorFactory.Simple("First error"));
// Success
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.None(RErrorFactory.Simple("Initial error"))
        .RMaybeBindMatch(() => RUnitFactory.Some(),
                         () => RErrorFactory.Simple("First error"));
// Failure. Errors: First
```
##### 4. RMaybeBindSome
Заменить класс при статусе 'Success'
```
(IRMaybe, () => IRMaybe) => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeBindSome(() => RUnitFactory.Some());
// Success
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeBindSome(() => RErrorFactory.Simple("First error"));
// Failure. Errors: First
```
#### Try action type
Методы расширения класса `IRMaybe` для обратбоки исключений. Позволяют преобразовать обычные методы к функциональному типу.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RMaybeTrySome` | `(RM, () => (), Ex => RE) => IRMaybe`
1 | `RMaybeTrySome` | `(RM, () => (), RE) => RM`
##### 1. RMaybeTrySome
Проверить статус и преобразовать метод к функциональному типу, а также исключение `Exception` к типу `IRError`.
```
(IRMaybe, () => (), Exception => IRError) => IRMaybe
(IRMaybe, () => (), IRError) => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeTrySome(() => DoAction(),
                       RErrorFactory.Simple("Exception error"));
// Success
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeTrySome(() => ThrowException(),
                       exception => RErrorFactory.Simple("Exception error").Append(exception));
// Failure. Errors: exception
```
#### Fold action type
Методы расширения для слияния коллекции `IRMaybe`. Наличие хотя бы одной ошибки переводит результирующий класс в состояние `Failure`.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RMaybeFold` | `L<RM> => RM`
##### 1. RMaybeFold
Агрегировать все ошибки типа `IRError` и перевести в суммарный класс `IRMaybe`.
```
List<IRMaybe> => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    Enumerable
        .Range(0, 3)
        .Select(_ => RUnitFactory.Some())
        .ToList()
        .RMaybeFold();
// Success
```
```csharp
private IRMaybe GetMaybe() =>
    Enumerable
        .Range(0, 3)
        .Select(_ => RUnitFactory.Some())
        .Append(RErrorFactory.Simple("Aggregate error"))
        .ToList()
        .RMaybeFold();
//  Failure. Errors: aggregate
```
#### Void action type
Методы расширения для выполнения методов, не являющихся функциями и не возвращающих значений. Может использоваться для присвоения значений в родительском классе или второстепенных процессов, например логгирования.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RMaybeVoidSome` | `(RM, () => ()) => RM`
2 | `RMaybeVoidNone` | `(RM, RE => ()) => RM`
3 | `RMaybeVoidMatch` | `(RM, () => (), RE => ()) => RM`
4 | `RMaybeVoidOption` | `(RM, () => bool, () => ()) => RM`
##### 1. RMaybeVoidSome
Выполнить метод в состоянии `Success`
```
(IRMaybe, () => ()) => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeVoidSome(() => DoAction());
// Success. DoAction
```
##### 2. RMaybeVoidNone
Выполнить метод в состоянии `Failure`. В статусе `Success` выполняется пропуск шага.
```
(IRMaybe, IRError => ()) => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeVoidNone(errors => DoErrorAction(errors));
// Success
```
private IRMaybe GetMaybe() =>
    RUnitFactory.None(RErrorFactory.Simple("Initial error"))
        .RMaybeVoidNone(errors => DoErrorAction(errors));
// Failure. Errors: initial. DoErrorAction
##### 3. RMaybeVoidMatch
Выполнить метод в зависимости от состояния.
```
(IRMaybe, () => (), IRError => ()) => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeVoidMatch(() => DoAction(),
                         errors => DoErrorAction(errors));
// Success. DoAction
```
private IRMaybe GetMaybe() =>
    RUnitFactory.None(RErrorFactory.Simple("Initial error"))
        .RMaybeVoidMatch(() => DoAction(),
                         errors => DoErrorAction(errors));
// Failure. Errors: initial. DoErrorAction
##### 4. RMaybeVoidOption
Выполнить метод в зависимости от условия.
```
(IRMaybe, () => bool, () => ()) => IRMaybe
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeVoidOption(() => true,
                          () => DoAction());
// Success. DoAction
```
```csharp
private IRMaybe GetMaybe() =>
    RUnitFactory.Some()
        .RMaybeVoidOption(() => false,
                          () => DoAction());
// Success
```
### IRUnit extensions
Класс `IRUnit` следует применять в том случае, когда достаточно информации о статусе объекта и нет необходимости проводить операции со значением `Value`. Класс `IRUnit` применяется в основном для стартовой инициализации методов расшрения типа `IRMaybe`.
#### Initialize functions
Методы расширений для инициализации класса `IRUnit` посредством коллекций других типов. Аналогичны операции Fold.
Extension | Signature
| ------------ | ------------
`ToRUnit` | `List<IRUnit> => IRUnit`
`ToRUnit` | `List<IRMaybe> => IRUnit`
`ToRUnit` | `List<IRError> => IRUnit`
### IRValue extensions
Методы расширения `IRValue` предназначены для обработки состояния объекта и преобразования значения `Value` с учетом статуса.
#### Function and action types
Методы расширения типа функтор не имеют дополнительного индекса в именованивании. Расширения типа монада обозначаются префиксом `Bind`.
Action | Functor | Actor | Monad
| ------------ | ------------ | ------------ | ------------
`Main` | :heavy_check_mark: |  | :heavy_check_mark:
`Try` | :heavy_check_mark: |  | :heavy_check_mark:
`Void` | :heavy_check_mark: |  |
`Lift` | :heavy_check_mark: |  |
`Curry` |  | :heavy_check_mark: |
`ToList` | :heavy_check_mark: |  | :heavy_check_mark:
`Init` | :heavy_check_mark: |  | :heavy_check_mark:
#### Main action type
Общие методы расширения класса `IRValue`. Позволяют производить операции со значением `Value` и ошибками 'IRError'.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RValueOption` | `(RV<TIn>, TIn => bool, TIn => TOut, TIn => RE) => RV<TOut>`
2 | `RValueWhere` | `(RV<TIn>, TIn => bool, TIn => TOut, TIn => TOut) => RV<TOut>`
3 | `RValueMatch` | `(RV<TIn>, TIn => TOut, RE => TOut) => RV<TOut>`
4 | `RValueSome` | `(RV<TIn>, TIn => TOut) => RV<TOut>`
5 | `RValueNone` | `(RV<T>, RE => T) => RV<T>`
6 | `RValueEnsure` | `(RV<T>, T => bool, T => RE) => RV<T>`
7 | `RValueBindOption` | `(RV<TIn>, TIn => bool, TIn => RV<TOut>, TIn => RE) => RV<TOut>`
8 | `RValueBindWhere` | `(RV<TIn>, TIn => bool, TIn => RV<TOut>, TIn => RV<TOut>) => RV<TOut>`
9 | `RValueBindMatch` | `(RV<TIn>, TIn => bool, TIn => RV<TOut>, RE => RV<TOut>) => RV<TOut>`
10 | `RValueBindSome` | `(RV<TIn>, TIn => RV<TOut>) => RV<TOut>`
11 | `RValueBindNone` | `(RV<T>, IRError => RV<T>) => RV<T>`
12 | `RValueBindEnsure` | `(RV<T>, T => bool, T => RM) => RV<T>`
##### 1. RValueOption
Перевести один тип значения `Value` в другой с учетом статуса объекта. В случае невыполнения условия объект `IRValue` переходит в состояние `Failure` с соответствующей ошибкой.
```
(IRValue<TIn>, TIn => bool, TIn => TOut, TIn => IRError) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueOption(number => true,
                      number => number.ToString(),
                      number => RErrorFactory.Simple("Condition error"));
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueOption(number => false,
                      number => number.ToString(),
                      number => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 2. RValueWhere
Перевести один тип значения `Value` в другой с учетом статуса объекта. В случае невыполнения условия выполняется альтернативный метод инициализирующий объект `IRValue`.
```
(IRValue<TIn>, TIn => bool, TIn => TOut, TIn => TOut) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueWhere(number => true,
                     number => number.ToString(),
                     number => String.Empty);
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueWhere(number => false,
                     number => number.ToString(),
                     number => String.Empty);
// Success. Value: empty
```
##### 3. RValueMatch
Перевести один тип значения `Value` в другой с учетом статуса объекта. В случае состояние `Failure` выполняется альтернативный метод инициализирующий объект `IRValue` на основе ошибок `IRError`.
```
(IRValue<TIn>, TIn => TOut, IRError => TOut) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueMatch(number => number.ToString(),
                     errors => errors.First().Description);
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    RValueFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RValueMatch(number => number.ToString(),
                     errors => errors.First().Description);
// Success. Value: "Initial error"
```
##### 4. RValueSome
Перевести один тип значения `Value` в другой при условии статуса объекта `Success`.
```
(IRValue<TIn>, TIn => TOut) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueSome(number => number.ToString());
// Success. Value: "1"
```
##### 5. RValueNone
Перевести один тип значения `Value` в другой при условии статуса объекта `Failure`.
```
(IRValue<T>, IRError => T) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueNone(errors => GetErrorCode(errors.First()));
// Success. Value: 1
```
```csharp
private IRValue<int> GetValue() =>
    RValueFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RValueNone(errors => GetErrorCode(errors.First()));
// Success. Value: 400
```
##### 6. RValueEnsure
Проверить статус, проверить условие и присвоить ошибку в случае невыполнения.
```
(IRValue<T>, T => bool, T => IRError) => IRValue<T>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueEnsure(number => true,
                      number => RErrorFactory.Simple("Condition error"));
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueEnsure(number => false,
                      number => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 7. RValueBindOption
Заменить один объект `IRValue` другим с учетом статуса объекта. В случае невыполнения условия объект `IRValue` переходит в состояние `Failure` с соответствующей ошибкой.
```
(IRValue<TIn>, TIn => bool, TIn => IRValue<TOut>, TIn => IRError) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueBindOption(number => true,
                          number => ToRValueString(number),
                          number => RErrorFactory.Simple("Condition error"));
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueBindOption(number => false,
                          number => ToRValueString(number),
                          number => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 8. RValueBindWhere
Заменить один объект `IRValue` другим с учетом статуса объекта. В случае невыполнения условия выполняется альтернативный метод инициализирующий объект `IRValue`.
```
(IRValue<TIn>, TIn => bool, TIn => IRValue<TOut>, TIn => IRValue<TOut>) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueBindWhere(number => true,
                         number => ToRValueString(number),
                         number => String.Empty.ToRValue());
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueBindWhere(number => false,
                         number => ToRValueString(number),
                         number => String.Empty.ToRValue());
// Success. Value: empty
```
##### 9. RValueBindMatch
Заменить один объект `IRValue` другим с учетом статуса объекта. В случае состояние `Failure` выполняется альтернативный метод инициализирующий объект `IRValue` на основе ошибок `IRError`.
```
(IRValue<TIn>, TIn => bool, TIn => IRValue<TOut>, IRError => IRValue<TOut>) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueBindMatch(number => ToRValueString(number),
                         errors => errors.First().Description.ToRValue());
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    RValueFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RValueBindMatch(number => ToRValueString(number),
                         errors => errors.First().Description.ToRValue());
// Success. Value: "Initial error"
```
##### 10. RValueBindSome
Заменить один объект `IRValue` другим при условии статуса объекта `Success`.
```
(IRValue<TIn>, TIn => IRValue<TOut>) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueBindSome(number => ToRValueString(number));
// Success. Value: "1"
```
##### 11. RValueBindNone
Заменить один объект `IRValue` другим при условии статуса объекта `Failure`.
```
(IRValue<T>, IRError => IRValue<T>) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueBindNone(errors => GetRErrorCode(errors.First()));
// Success. Value: 1
```
```csharp
private IRValue<int> GetValue() =>
    RValueFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RValueBindNone(errors => GetRErrorCode(errors.First()));
// Success. Value: 400
```
##### 12. RValueBindEnsure
Проверить статус, проверить условие и присвоить ошибку в случае невыполнения.
```
(IRValue<T>, T => bool, T => IRMaybe) => IRValue<T>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueEnsure(number => true,
                      number => RErrorFactory.Simple("Condition error").ToRUnit());
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueEnsure(number => false,
                      number => RErrorFactory.Simple("Condition error").ToRUnit());
// Failure. Errors: сondition
```
#### Try action type
Методы расширения класса `IRValue` для обратбоки исключений. Позволяют преобразовать обычные функции к функциональному типу.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RValueTrySome` | `(RV<TIn>, TIn => TOut, Ex => RE) => RV<TOut>`
1 | `RValueTrySome` | `(RV<TIn>, TIn => TOut, RE) => RV<TOut>`
2 | `RValueBindTrySome` | `(RV<TIn>, TIn => RV<TOut>, Ex => RE) => RV<TOut>`
2 | `RValueBindTrySome` | `(RV<TIn>, TIn => RV<TOut>, RE) => RV<TOut>`
##### 1. RValueTrySome
Проверить статус и преобразовать функцию к функциональному типу, а также исключение `Exception` к типу `IRError`.
```
(IRValue<TIn>, TIn => TOut, Exception => IRError) => IRValue<TOut>
(IRValue<TIn>, TIn => TOut, IRError) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueTrySome(number => number.ToString(),
                       RErrorFactory.Simple("Exception error"));
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueTrySome(number => ThrowException(number),
                       exception => RErrorFactory.Simple("Exception error").Append(exception));
// Failure. Errors: exception
```
##### 2. RValueBindTrySome
Проверить статус и заменить объект функциональным типом, а также исключение `Exception` типом `IRError`.
```
(IRValue<TIn>, TIn => TOut, Exception => IRError) => IRValue<TOut>
(IRValue<TIn>, TIn => TOut, IRError) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueTrySome(number => ToRValueString(number),
                       RErrorFactory.Simple("Exception error"));
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueTrySome(number => ThrowRException(number),
                       exception => RErrorFactory.Simple("Exception error").Append(exception));
// Failure. Errors: exception
```
#### Void action type
Методы расширения для выполнения методов, не являющихся функциями и не возвращающих значений. Может использоваться для присвоения значений в родительском классе или второстепенных процессов, например логгирования. Значение `Value` остается неизменным.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RValueVoidSome` | `(RV<T>, T => ()) => RV<T>`
2 | `RValueVoidNone` | `(RV<T>, RE => ()) => RV<T>`
3 | `RValueVoidMatch` | `(RV<T>, T => (), RE => ()) => RV<T>`
4 | `RValueVoidOption` | `(RV<T>, T => bool, T => ()) => RV<T>`
##### 1. RValueVoidSome
Выполнить метод в состоянии `Success`
```
(IRValue<T>, T => ()) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
     GetNumber()
        .ToRValue()
        .RValueVoidSome(number => DoAction(number));
// Success. DoAction. Value: "1"
```
##### 2. RValueVoidNone
Выполнить метод в состоянии `Failure`. В статусе `Success` выполняется пропуск шага.
```
(IRValue<T>, IRError => ()) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
     GetNumber()
        .ToRValue()
        .RValueVoidNone(errors => DoErrorAction(errors));
// Success. Value: "1"
```
```csharp
private IRValue<int> GetValue() =>
    RValueFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RValueVoidNone(errors => DoErrorAction(errors));
// Failure. Errors: initial. DoErrorAction
```
##### 3. RValueVoidMatch
Выполнить метод в зависимости от состояния.
```
(IRValue<T>, T => (), IRError => ()) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
     GetNumber()
        .ToRValue()
        .RValueVoidMatch(number => DoAction(number),
                         errors => DoErrorAction(errors));
// Success. DoAction.  Value: "1"
```
```csharp
private IRValue<int> GetValue() =>
    RValueFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RValueVoidMatch(number => DoAction(number),
                         errors => DoErrorAction(errors));
// Failure. Errors: initial. DoErrorAction
```
##### 4. RValueVoidOption
Выполнить метод в зависимости от условия.
```
(IRValue<T>, T => bool, T => ()) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
     GetNumber()
        .ToRValue()
        .RValueVoidOption(number => true,
                          number => DoAction(number));
// Success. DoAction. Value: "1"
```
```csharp
private IRValue<int> GetValue() =>
     GetNumber()
        .ToRValue()
        .RValueVoidOption(number => false,
                          number => DoAction(number));
// Success. Value: "1"
```
#### Lift action type
Методы расширения для разворачивания объекта R<T> -> T в зависимости от статуса.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RValueLiftMatch` | `(RV<TIn>, TIn => TOut, ER => TOut) => TOut`
##### 1. RValueLiftMatch
Разворачивает значение `Value` в зависимости от текущего статуса объекта
```
(IRValue<TIn>, TIn => TOut, IRError => TOut) => TOut
```
```csharp
private string GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueLiftMatch(number => number.ToString(),
                         errors => GetErrorCode(errors).ToString());
// Value: "1"
```
```csharp
private string GetValue() =>
    RValueFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RValueLiftMatch(number => number.ToString(),
                         errors => GetErrorCode(errors).ToString());
// Value: "400"
```
#### Curry action type
Методы расширения для каррирования R функций. Под каррированием понимается уменьшение количества входных параметров функции высокого порядка за счет подстановки аргумента. Возвращаемая функция имеет на один аргумент меньше. Каррирование применяется для инициализирующих объекты функций, аргументами которых являются R классы.
Методы расширения каррирования являются акторами.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RValueCurry` | `(RV<F<TIn, TOut>>, RV<TIn>) => RV<TOut>`
2 | `RValueCurryList` | `(RV<F<L<TIn>, TOut>>, RL<TIn>) => RV<TOut>`
##### 1. RValueCurry
Подставляет объект `RValue<T>` в функцию высшего подрядка. Если функция или подставляемое значение находятся в статусе `Failure`, то выполняется пропуск шага.
```
(RValue<Func<TIn, TOut>>, RValue<TIn>) => RValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    ToString
        .ToRValue()
        .RValueCurry(GetRValueNumber);
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    ToString
        .ToRValue()
        .RValueCurry(RValueFactory.None<int>(RErrorFactory.Simple("Curry error")));
// Failure. Errors: curry
```
##### 2. RValueCurryList
Подставляет объект `RList<T>` в функцию высшего подрядка. Если функция или подставляемое значение находятся в статусе `Failure`, то выполняется пропуск шага. Метод применяется при использовании коллекций.
```
(IRValue<Func<List<TIn>, TOut>>, IRList<TIn>) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    ListToString
        .ToRValue()
        .RValueCurryList(GetRValueNumbers());
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    ListToString
        .ToRValue()
        .RValueCurryList(RListFactory.None<int>(RErrorFactory.Simple("Curry error")));
// Failure. Errors: curry
```
#### ToList action type
Методы расширения аналогичные `Main actions`, где значением `T` выступает коллекция. Фактически метод расширения преобразует `IRValue<T> -> IRList<T>`.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RValueToListOption` | `(RV<TIn>, TIn => bool, TIn => L<TOut>, TIn => RE) => RL<TOut>`
2 | `RValueToListMatch` | `(RV<TIn>, TIn => L<TOut>, RE => L<TOut>) => RL<TOut>`
3 | `RValueToListSome` | `(RV<TIn>, TIn => L<TOut>) => RL<TOut>`
4 | `RValueToListBindSome` | `(RV<TIn>, TIn => RL<TOut>) => RL<TOut>`
##### 1. RValueToListOption
Перевести один тип значения `Value` в коллекцию с учетом статуса объекта. В случае невыполнения условия объект `IRValue` преобразуется в `IRList` с состоянием `Failure` и соответствующей ошибкой.
```
(IRValue<TIn>, TIn => bool, TIn => List<TOut>, TIn => IRError) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumber()
        .ToRValue()
        .RValueToListOption(number => true,
                            number => NumberToList(number),
                            number => RErrorFactory.Simple("Condition error"));
// Success. List: {"0"}
```
```csharp
private IRList<string> GetList() =>
    GetNumber()
        .ToRValue()
        .RValueToListOption(number => false,
                      number => NumberToList(number),
                      number => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 2. RValueToListMatch
Перевести один тип значения `Value` в коллекцию с учетом статуса объекта. В случае состояние `Failure` выполняется альтернативный метод инициализирующий объект `IRList` на основе ошибок `IRError`.
```
(IRValue<TIn>, TIn => List<TOut>, IRError => List<TOut>) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumber()
        .ToRValue()
        .RValueToListMatch(number => NumberToList(number),
                           errors => new List<string> { errors.First().Description });
// Success. List: {"0"}
```
```csharp
private IRList<string> GetList() =>
    RValueFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RValueToListMatch(number => NumberToList(number),
                           errors => new List<string> { errors.First().Description });
// Success. List: "Initial error"
```
##### 3. RValueToListSome
Заменить один объект `IRValue` коллекцией `IRList` при условии статуса объекта `Success`.
```
(IRValue<TIn>, TIn => List<TOut>) => IRList<TOut>
```
```csharp
private IRList<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueToListSome(number => NumberToList(number));
// Success. List: {"0"}
```
##### 4. RValueToListBindSome
Перевести один тип значения `Value` в коллекцию при условии статуса объекта `Success`.
```
(IRValue<TIn>, TIn => IRList<TOut>) => IRList<TOut>
```
```csharp
private IRList<string> GetValue() =>
    GetNumber()
        .ToRValue()
        .RValueToListBindSome(number => NumberToRList(number));
// Success. List: {"0"}
```
#### Initialize functions
Методы расширений для инициализации класса `IRValue`.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `ToRValue` | `T => RV<T>`
2 | `ToRValue` | `(RM, T) => RV<T>`
3 | `ToRValueBind` | `(RM, RV<T>) => RV<T>`
4 | `ToRValueEnsure` | `(T, RE) => RV<T>`
5 | `ToRValueOption` | `(T, T => bool, T => RE) => RV<T>`
##### 1. ToRValue
Привести значение `Value` к объекту типа `IRValue`. При этом тип значения `Value` должно соответсвовать условию `notnull`.
```
T => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
    GetNumber()
        .ToRValue();
// Success. Value: {"0"}
```
##### 2. ToRValue
Присвоить объекту `IRMaybe` значение `Value` в зависимости от статуса. 
```
(IRMaybe, T) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
    RUnitFactory.Some()
        .ToRValue(GetNumber());
// Success. Value: 1
```
```csharp
private IRValue<int> GetValue() =>
    RUnitFactory.None(RErrorFactory.Simple("Initial error"))
        .ToRValue(GetNumber());
// Success. Errors: initial
```
##### 3. ToRValueBind
Заменить объект `IRMaybe` объектом `IRValue` в зависимости от статуса. 
```
(IRMaybe, IRValue<T>) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
    RUnitFactory.Some()
        .ToRValue(GetRValueNumber());
// Success. Value: 1
```
```csharp
private IRValue<int> GetValue() =>
    RUnitFactory.Some()
        .ToRValue(RErrorFactory.Simple("Value error"));
// Success. Value: 1
```
```csharp
private IRValue<int> GetValue() =>
    RUnitFactory.None(RValueFactory.None<int>(RErrorFactory.Simple("Value error")))
        .ToRValue(GetRValueNumber());
// Success. Errors: value
```
##### 4. ToRValueEnsure
Перевести значение `Value` к объекту типа `IRValue`. При этом значении `Value` равному `null` будет присвоен статус `Failure`.
```
(T, IRError) => IRValue<T>
```
```csharp
private IRValue<string> GetValue() =>
    GetNullString()
        .ToRValueEnsure(RErrorFactory.Simple("Null error"));
// Failure. Errors: null
```
##### 5. ToRValueOption
Привести значение `Value` к объекту типа `IRValue` с учетом условия. При этом тип значения `Value` должно соответсвовать условию `notnull`.
```
(T, T => bool, T => RE) => IRValue<T>
```
```csharp
private IRValue<int> GetValue() =>
    GetNumber()
        .ToRValueOption(number => true,
                        number => RErrorFactory.Simple("Condition error"));
// Success. Value: 1
```
```csharp
private IRValue<int> GetValue() =>
    GetNumber()
        .ToRValueOption(number => false,
                        number => RErrorFactory.Simple("Condition error"));
// Success. Errors: condition
```
### IRList extensions
Методы расширения `IRList` предназначены для обработки состояния объекта и преобразования коллекции `List` с учетом статуса.
#### Function and action types
Методы расширения типа функтор не имеют дополнительного индекса в именованивании. Расширения типа монада обозначаются префиксом `Bind`.
Action | Functor | Actor | Monad
| ------------ | ------------ | ------------ | ------------
`Main` | :heavy_check_mark: |  | :heavy_check_mark:
`Try` | :heavy_check_mark: |  | :heavy_check_mark:
`Void` | :heavy_check_mark: |  |
`Fold` | :heavy_check_mark: |  |
`Lift` | :heavy_check_mark: |  |
`ToValue` | :heavy_check_mark: |  | :heavy_check_mark:
`Init` | :heavy_check_mark: |  | :heavy_check_mark:
#### Main action type
Общие методы расширения класса `IRList`. Позволяют производить операции с коллекцией `List` и ошибками 'IRError'.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RListOption` | `(RL<TIn>, L<TIn> => bool, L<TIn> => L<TOut>, L<TIn> => RE) => RL<TOut>`
2 | `RListWhere` | `(RL<TIn>, L<TIn> => bool, L<TIn> => L<TOut>, L<TIn>=> L<TOut>) => RL<TOut>`
3 | `RListMatch` | `(RL<TIn>, L<TIn> => L<TOut>, RE => L<TOut>) => RL<TOut>`
4 | `RListSome` | `(RL<TIn>, L<TIn> => L<TOut>) => RL<TOut>`
5 | `RListNone` | `(RL<T>, RE => L<T>) => RL<T>`
6 | `RListEnsure` | `(RL<T>, L<T> => bool, L<T> => RE) => RL<T>`
7 | `RListBindOption` | `(RL<TIn>, L<TIn> => bool, L<TIn> => RL<TOut>, L<TIn> => RE) => RL<TOut>`
8 | `RListBindWhere` | `(RL<TIn>, L<TIn> => bool, L<TIn> => RL<TOut>, L<TIn> => RL<TOut>) => RL<TOut>`
9 | `RListBindMatch` | `(RL<TIn>, L<TIn>=> bool, L<TIn> => RL<TOut>, RE => RL<TOut>) => RL<TOut>`
10 | `RListBindSome` | `(RL<TIn>, L<TIn> => RL<TOut>) => RL<TOut>`
11 | `RListBindNone` | `(RL<T>, IRError => RL<T>) => RL<T>`
12 | `RListBindEnsure` | `(RL<T>, L<T> => bool, L<T> => RM) => RL<T>`
##### 1. RListOption
Перевести один тип коллеции `List` в другой с учетом статуса объекта. В случае невыполнения условия объект `IRList` переходит в состояние `Failure` с соответствующей ошибкой.
```
(IRList<TIn>, List<TIn> => bool, List<TIn> => List<TOut>, List<TIn> => IRError) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListOption(numbers => true,
                     numbers => ListToString(numbers),
                     numbers => RErrorFactory.Simple("Condition error"));
// Success. List: "1"
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListOption(numbers => false,
                     numbers => ListToString(numbers),
                     numbers => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 2. RListWhere
Перевести один тип коллекции `List` в другой с учетом статуса объекта. В случае невыполнения условия выполняется альтернативный метод инициализирующий объект `IRList`.
```
(IRList<TIn>, List<TIn> => bool, List<TIn> => List<TOut>, List<TIn> => List<TOut>) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListWhere(numbers => true,
                    numbers => ListToString(numbers),
                    numbers => new List<string> {String.Empty});
// Success. List: "1"
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListWhere(numbers => false,
                    numbers => ListToString(numbers),
                    numbers => new List<string> {String.Empty});
// Success. List: empty
```
##### 3. RListMatch
Перевести один тип коллекции `List` в другой с учетом статуса объекта. В случае состояние `Failure` выполняется альтернативный метод инициализирующий объект `IRList` на основе ошибок `IRError`.
```
(IRList<TIn>, List<TIn> => List<TOut>, IRError => List<TOut>) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListMatch(numbers => ListToString(numbers),
                    errors => new List<string> {errors.First().Description});
// Success. List: "1"
```
```csharp
private IRList<string> GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListMatch(numbers => ListToString(numbers),
                    errors => new List<string> {errors.First().Description});
// Success. List: "Initial error"
```
##### 4. RListSome
Перевести один тип коллекции `List` в другой при условии статуса объекта `Success`.
```
(IRList<TIn>, List<TIn> => List<TOut>) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListSome(numbers => ListToString(numbers));
// Success. List: "1"
```
##### 5. RListNone
Перевести один тип коллекции `List` в другой при условии статуса объекта `Failure`.
```
(IRList<T>, IRError => List<T>) => IRList<T>
```
```csharp
private IRList<int> GetList() =>
    GetNumbers()
        .ToRList()
        .RListNone(errors => GetErrorCodeList(errors.First()));
// Success. List: 1
```
```csharp
private IRValue<int> GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListNone(errors => GetErrorCodeList(errors.First()));
// Success. List: 400
```
##### 6. RListEnsure
Проверить статус, проверить условие и присвоить ошибку в случае невыполнения.
```
(IRList<T>, List<T> => bool, List<T> => IRError) => IRList<T>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListEnsure(numbers => true,
                     numbers => RErrorFactory.Simple("Condition error"));
// Success. List: "1"
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListEnsure(numbers => false,
                     numbers => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 7. RListBindOption
Заменить один объект `IRList` другим с учетом статуса объекта. В случае невыполнения условия объект `IRList` переходит в состояние `Failure` с соответствующей ошибкой.
```
(IRList<TIn>, List<TIn> => bool, List<TIn> => IRList<TOut>, List<TIn> => IRError) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindOption(numbers => true,
                         numbers => ToRListString(numbers),
                         numbers => RErrorFactory.Simple("Condition error"));
// Success. List: "1"
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindOption(numbers => false,
                         numbers => ToRListString(numbers),
                         numbers => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 8. RListBindWhere
Заменить один объект `IRList` другим с учетом статуса объекта. В случае невыполнения условия выполняется альтернативный метод инициализирующий объект `IRList`.
```
(IRList<TIn>, List<TIn> => bool, List<TIn> => IRList<TOut>, List<TIn> => IRList<TOut>) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindWhere(numbers => true,
                        numbers => ToRListString(numbers),
                        numbers => String.Empty.ToRValue());
// Success. Value: "1"
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindWhere(numbers => false,
                        numbers => ToRListString(numbers),
                        numbers => String.Empty.ToRValue());
// Success. Value: empty
```
##### 9. RListBindMatch
Заменить один объект `IRList` другим с учетом статуса объекта. В случае состояние `Failure` выполняется альтернативный метод инициализирующий объект `IRList` на основе ошибок `IRError`.
```
(IRList<TIn>, List<TIn> => bool, List<TIn> => IRList<TOut>, IRError => IRList<TOut>) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindMatch(number => ToRListString(number),
                         errors => EmptyRListString());
// Success. List: "1"
```
```csharp
private IRList<string> GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListBindMatch(number => ToRListString(number),
                         errors => EmptyRListString());
// Success. List: empty
```
##### 10. RListBindSome
Заменить один объект `IRList` другим при условии статуса объекта `Success`.
```
(IRList<TIn>, List<TIn> => IRList<TOut>) => IRList<TOut>
```
```csharp
private IRLIst<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindSome(numbers => ToRListString(numbers));
// Success. List: "1"
```
##### 11. RListBindNone
Заменить один объект `IRList` другим при условии статуса объекта `Failure`.
```
(IRLIst<T>, IRError => IRLIst<T>) => IRLIst<T>
```
```csharp
private IRList<int> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindNone(errors => GetRErrorCodeList(errors.First()));
// Success. List: 1
```
```csharp
private IRList<int> GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListBindNone(errors => GetRErrorCodeList(errors.First()));
// Success. List: 400
```
##### 12. RListBindEnsure
Проверить статус, проверить условие и присвоить ошибку в случае невыполнения.
```
(IRList<T>, List<T> => bool, List<T> => IRMaybe) => IRList<T>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindEnsure(numbers => true,
                         numbers => RErrorFactory.Simple("Condition error").ToRUnit());
// Success. Value: "1"
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindEnsure(numbers => false,
                         numbers => RErrorFactory.Simple("Condition error").ToRUnit());
// Failure. Errors: сondition
```
#### Try action type
Методы расширения класса `IRList` для обратбоки исключений. Позволяют преобразовать обычные функции к функциональному типу.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RListTrySome` | `(RL<TIn>, L<TIn> => L<TOut>, Ex => RE) => RL<TOut>`
1 | `RListTrySome` | `(RL<TIn>, L<TIn> => L<TOut>, RE) => RL<TOut>`
2 | `RListBindTrySome` | `(RL<TIn>, L<TIn> => RL<TOut>, Ex => RE) => RL<TOut>`
2 | `RListBindTrySome` | `(RL<TIn>, L<TIn> => RL<TOut>, RE) => RL<TOut>`
##### 1. RListTrySome
Проверить статус и преобразовать функцию к функциональному типу, а также исключение `Exception` к типу `IRError`.
```
(IRList<TIn>, List<TIn> => List<TOut>, Exception => IRError) => IRList<TOut>
(IRList<TIn>, List<TIn> => List<TOut>, IRError) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListTrySome(numbers => ListToString(numbers),
                      RErrorFactory.Simple("Exception error"));
// Success. List: "1"
```
```csharp
private IRValue<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListTrySome(numbers => ThrowException(numbers),
                      exception => RErrorFactory.Simple("Exception error").Append(exception));
// Failure. Errors: exception
```
##### 2. RListBindTrySome
Проверить статус и заменить объект функциональным типом, а также исключение `Exception` типом `IRError`.
```
(IRList<TIn>, List<TIn> => List<TOut>, Exception => IRError) => IRList<TOut>
(IRList<TIn>, List<TIn> => List<TOut>, IRError) => IRList<TOut>
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindTrySome(numbers => ToRListString(numbers),
                          RErrorFactory.Simple("Exception error"));
// Success. List: "1"
```
```csharp
private IRList<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListBindTrySome(numbers => ThrowRException(number),
                          exception => RErrorFactory.Simple("Exception error").Append(exception));
// Failure. Errors: exception
```
#### Void action type
Методы расширения для выполнения методов, не являющихся функциями и не возвращающих значений. Может использоваться для присвоения значений в родительском классе или второстепенных процессов, например логгирования. Значение `List` остается неизменным.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RListVoidSome` | `(RL<T>, List<T> => ()) => RL<T>`
2 | `RListVoidNone` | `(RL<T>, RE => ()) => RL<T>`
3 | `RListVoidMatch` | `(RL<T>, List<T> => (), RE => ()) => RL<T>`
4 | `RListVoidOption` | `(RL<T>, List<T> => bool, T => ()) => RL<T>`
##### 1. RListVoidSome
Выполнить метод в состоянии `Success`
```
(IRList<T>, List<T> => ()) => IRList<T>
```
```csharp
private IRList<int> GetList() =>
     GetNumbers()
        .ToRList()
        .RListVoidSome(numbers => DoAction(numbers));
// Success. DoAction. List: "1"
```
##### 2. RListVoidNone
Выполнить метод в состоянии `Failure`. В статусе `Success` выполняется пропуск шага.
```
(IRList<T>, IRError => ()) => IRList<T>
```
```csharp
private IRList<int> GetList() =>
     GetNumbers()
        .ToRList()
        .RListVoidNone(errors => DoErrorAction(errors));
// Success. List: "1"
```
```csharp
private IRList<int> GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListVoidNone(errors => DoErrorAction(errors));
// Failure. Errors: initial. DoErrorAction
```
##### 3. RListVoidMatch
Выполнить метод в зависимости от состояния.
```
(IRList<T>, T => (), IRError => ()) => IRList<T>
```
```csharp
private IRList<int> GetList() =>
     GetNumbers()
        .ToRList()
        .RListVoidMatch(numbers => DoAction(numbers),
                        errors => DoErrorAction(errors));
// Success. DoAction. List: "1"
```
```csharp
private IRValue<int> GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListVoidMatch(numbers => DoAction(numbers),
                        errors => DoErrorAction(errors));
// Failure. Errors: initial. DoErrorAction
```
##### 4. RListVoidOption
Выполнить метод в зависимости от условия.
```
(IRList<T>, List<T> => bool, List<T> => ()) => IRList<T>
```
```csharp
private IRList<int> GetList() =>
     GetNumbers()
        .ToRList()
        .RListVoidOption(numbers => true,
                         numbers => DoAction(numbers));
// Success. DoAction. List: "1"
```
```csharp
private IRList<int> GetList() =>
     GetNumbers()
        .ToRList()
        .RListVoidOption(numbers => false,
                         numbers => DoAction(numbers));
// Success. List: "1"
```
#### Fold action type
Методы расширения для слияния коллекции `IRList`. Наличие хотя бы одной ошибки переводит результирующий класс в состояние `Failure`.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RListFold` | `L<RL<T>> => RL<T>`
##### 1. RListFold
Агрегировать все классы типа `IRList` и перевести в суммарный класс.
```
List<IRList<T>> => IRList<T>
```
```csharp
private IRList<T> GetList() =>
    Enumerable
        .Range(0, 3)
        .Select(number => new List<int> {number}.ToRList())
        .ToList()
        .RListFold();
// Success. List: 0,1,2
```
```csharp
private IRList<T> GetList() =>
    Enumerable
        .Range(0, 3)
        .Select(number => new List<int> {number}.ToRList())
        .Append(RListFactory.None<int>(RErrorFactory.Simple("Aggregate error")))
        .ToList()
        .RListFold();
//  Failure. Errors: aggregate
```
#### Lift action type
Методы расширения для разворачивания объекта RV<T> -> T в зависимости от статуса.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RListLiftMatch` | `(RL<TIn>, L<TIn> => L<TOut>, ER => L<TOut>) => RL<TOut>`
##### 1. RListLiftMatch
Разворачивает значение `List` в зависимости от текущего статуса объекта
```
(IRList<TIn>, List<TIn> => List<TOut>, IRError => List<TOut>) => List<TOut>
```
```csharp
private List<string> GetList() =>
    GetNumbers()
        .ToRList()
        .RListLiftMatch(numbers => ListToString(numbers),
                        errors => GetErrorCodeList(errors).Select(code => code.ToString()).ToList());
// List: "1"
```
```csharp
private string GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListLiftMatch(numbers => ListToString(numbers),
                        errors => GetErrorCodeList(errors).Select(code => code.ToString()).ToList());
// List: "400"
```
#### ToValue action type
Методы расширения аналогичные `Main actions`, где коллекция `List` преобразуется в значение `Value`. Фактически метод расширения преобразует `IRList<T> -> IRValue<T>`.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `RListToValueOption` | `(RL<TIn>, L<TIn> => bool, L<TIn> => TOut, L<TIn> => RE) => RV<TOut>`
2 | `RListToValueMatch` | `(RL<TIn>, L<TIn> => TOut, RE => TOut) => RV<TOut>`
3 | `RListToValueSome` | `(RL<TIn>, L<TIn> => TOut) => RV<TOut>`
4 | `RListToValueBindOption` | `(RL<TIn>, L<TIn> => RV<TOut>) => RV<TOut>`
5 | `RListToValueBindMatch` | `(RL<TIn>, L<TIn> =>RVRL<TOut>) => RV<TOut>`
6 | `RListToValueBindSome` | `(RL<TIn>, L<TIn> => RV<TOut>) => RV<TOut>`
##### 1. RListToValueOption
Перевести один тип значения `Value` в коллекцию с учетом статуса объекта. В случае невыполнения условия объект `IRValue` преобразуется в `IRList` с состоянием `Failure` и соответствующей ошибкой.
```
(IRList<TIn>, List<TIn> => bool, List<TIn> => TOut, List<TIn> => IRError) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumbers()
        .ToRList()
        .RListToValueOption(numbers => true,
                            numbers => ListToString(numbers),
                            numbers => RErrorFactory.Simple("Condition error"));
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumbers()
        .ToRList()
        .RListToValueOption(numbers => false,
                            numbers => ListToString(numbers),
                            numbers => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 2. RListToValueMatch
Перевести один тип коллекции `List` в значение с учетом статуса объекта. В случае состояние `Failure` выполняется альтернативный метод инициализирующий объект `IRValue` на основе ошибок `IRError`.
```
(IRList<TIn>, List<TIn> => TOut, IRError => TOut) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumbers()
        .ToRList()
        .RListToValueMatch(numbers => ListToString(numbers),
                           errors => errors.First().Description);
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListToValueMatch(numbers => ListToString(numbers),
                           errors => errors.First().Description);
// Success. Value: "Initial error"
```
##### 3. RListToValueSome
Заменить один объект `IRList` коллекцией `IRValue` при условии статуса объекта `Success`.
```
(IRList<TIn>, List<TIn> => IRValue<TOut>) => IRList<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumbers()
        .ToRList()
        .RListToValueSome(numbers => ListToString(numbers));
// Success. Value: "1"
```
##### 4. RListToValueBindOption
Заменить коллекцию `IRList` в значение с учетом статуса объекта. В случае невыполнения условия объект `IRList` преобразуется в `IRValue` с состоянием `Failure` и соответствующей ошибкой.
```
(IRList<TIn>, List<TIn> => bool, List<TIn> => IRValue<TOut>, List<TIn> => IRError) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumbers()
        .ToRList()
        .RListToValueBindOption(numbers => true,
                                numbers => NumbersToRValue(numbers),
                                numbers => RErrorFactory.Simple("Condition error"));
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    GetNumbers()
        .ToRList()
        .RListToValueBindOption(numbers => false,
                                numbers => NumbersToRValue(numbers),
                                numbers => RErrorFactory.Simple("Condition error"));
// Failure. Errors: сondition
```
##### 5. RListToValueBindMatch
Заменить тип коллекции `IRList` значением с учетом статуса объекта. В случае состояние `Failure` выполняется альтернативный метод инициализирующий объект `IRValue` на основе ошибок `IRError`.
```
(IRList<TIn>, List<TIn> => IRValue<TOut>, IRError => TOut) => IRValue<TOut>
```
```csharp
private IRValue<string> GetValue() =>
    GetNumbers()
        .ToRList()
        .RListToValueBindMatch(numbers => NumbersToRValue(numbers),
                               errors => errors.First().Description.ToRValue());
// Success. Value: "1"
```
```csharp
private IRValue<string> GetValue() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .RListToValueBindMatch(numbers => NumbersToRValue(numbers),
                               errors => errors.First().Description.ToRValue());
// Success. Value: "Initial error"
```
##### 6. RListToValueBindSome
Заменить тип коллекции `IRList` на значение при условии статуса объекта `Success`.
```
(IRList<TIn>, List<TIn> => IRValue<TOut>) => IRValue<TOut>
```
```csharp
private IRList<string> GetValue() =>
    GetNumbers()
        .ToRList()
        .RListToValueBindSome(numbers => NumbersToRValue(numbers));
// Success. Value: "1"
```
#### Initialize functions
Методы расширений для инициализации класса `IRList`.
Id | Extension | Signature
| ------------ | ------------ | ------------
1 | `ToRList` | `L<T> => RL<T>`
2 | `ToRList` | `L<RE> => RL<T>`
3 | `ToRList` | `(RM, L<T>) => RL<T>`
4 | `ToRList` | `RV<List<T>> => RL<T>`
5 | `ToRList` | `L<RV<T>> => RL<T>`
6 | `ToRValueOption` | `(L<T>, L<T> => bool, L<T> => RE) => RL<T>`
##### 1. ToRList
Привести коллекцию `List` к объекту типа `IRList`. При этом тип значения коллекции `List` должны соответсвовать условию `notnull`.
```
List<T> => IRList<T>
```
```csharp
private IRList<int> GetList() =>
    GetNumbers()
        .ToRList();
// Success. List: "0"
```
##### 2. ToRList
Преобразовать коллекцию ошибок `IRError` к объекту `IRList` со статусом `Failure`. 
```
List<IRError> => IRList<T>
```
```csharp
private IRList<int> GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .ToRList();
// Success. Errors: initial
```
##### 3. ToRList
Присвоить объекту `IRMaybe` коллекцию `List` в зависимости от статуса. 
```
(IRMaybe, List<T>) => IRList<T>
```
```csharp
private IRList<int> GetList() =>
    RUnitFactory.Some()
        .ToRList(GetNumbers());
// Success. List: 1
```
```csharp
private IRList<int> GetList() =>
    RListFactory.None<int>(RErrorFactory.Simple("Initial error"))
        .ToRList(GetNumbers());
// Success. Errors: initial
```
##### 4. ToRList
Преобразовать коллекцию значений `Value` в обертке `IRValue` к объекту `IRList`. 
```
IRValue<List<T>> => IRList<T>
```
```csharp
private IRList<int> GetList() =>
    GetNumbers()
        .ToRValue()
        .ToRList();
// Success. List: 1
```
##### 4. ToRList
Преобразовать коллекцию значений `Value` в обертке `IRValue` к объекту `IRList`. 
```
List<IRValue<T>> => IRList<T>
```
```csharp
private IRList<int> GetList() =>
    GetNumbers()
        .Select(number => number.ToRValue)
        .ToList()
        .ToRList();
// Success. List: 1
```
##### 5. ToRListOption
Привести коллекцию `List` к объекту типа `IRList` с учетом условия. При этом тип значений коллекции `Value` должны соответсвовать условию `notnull`.
```
(List<T>, List<T> => bool, List<T> => RE) => IRList<T>
```
```csharp
private IRList<int> GetList() =>
    GetNumbers()
        .ToRListOption(numbers => true,
                       numbers => RErrorFactory.Simple("Condition error"));
// Success. List: 1
```
```csharp
private IRList<int> GetList() =>
    GetNumbers()
        .ToRListOption(numbers => false,
                       numbers => RErrorFactory.Simple("Condition error"));
// Success. Errors: condition
```
### Conclusion
### Example functions
Здесь приведены некоторые простые функции, которые используются в примерах.

<details>
<summary>Examples</summary>
    
##### Action
```csharp
private void DoAction()
{}
```
```csharp
private void DoAction(int number)
{}
```
```csharp
private void DoAction(IEnumerable<int> numbers)
{}
```
```csharp
private void DoErrorAction(IReadOnlyCollection<IRError> errors)
{}
```
##### Function
```csharp
private int GetNumber() =>
    1;
```
```csharp
private List<int> GetNumbers() =>
    new List<int> {"1"};
```
```csharp
private string ToString(int number) =>
    number.ToString();
```
```csharp
private string ListToString(IEnumerable<int> numbers) =>
    numbers.Sum().ToString();
```
```csharp
private List<string> NumberToList(int number) =>
    Enumerable
        .Range(0, number)
        .Select(n => n.ToString())
        .ToList();
```
```csharp
private string GetNullString() =>
    null;
```
##### Exception
```csharp
private int ThrowException() =>
    throw new Exception();
```
```csharp
private string ThrowException(int number) =>
    throw new Exception();
```
```csharp
private string ThrowException(IEnumberable<int> numbers) =>
    throw new Exception();
```
```csharp
private IRValue<string> ThrowRException(int number) =>
    throw new Exception();
```
##### IRError
```csharp
private int GetErrorCode(IRError error) =>
    400;
```
```csharp
private List<int> GetErrorCodeList(IRError error) =>
    GetErrorCode(error)
        .Map(code => NumberToList(code));
```
##### RValue
```csharp
private IRValue<string> GetRValueNumber() =>
    GetNumber()
        .ToRValue();
```
```csharp
private IRValue<string> ToRValueString(int number) =>
    number
        .ToRValue()
        .RValueSome(number => number.ToString());
```
```csharp
private IRValue<string> NumbersToRValue(List<int> numbers) =>
    numbers
        .First()
        .ToRValue();
```
```csharp
private IRValue<int> GetRErrorCode(IRError error) =>
    400.ToRValue();
```
##### RList
```csharp
private IRList<int> GetRValueNumbers() =>
    new List<int> {1}
        .ToRList();
```
```csharp
private IRList<string> NumberToRList(int number) =>
    Enumerable
        .Range(0, number)
        .Select(n => n.ToString())
        .ToRList();
```
```csharp
private IRList<string> ToRListString(IEnumarable<int> numbers) =>
    numbers
        .ToRList()
        .RListSome(numbers => numbers.Select(number => number.ToString()).ToList);
```
```csharp
private IRList<string> EmptyRListString() =>
    new List<string> {String.Empty}
        .ToRList();
```
```csharp
private IRList<int> GetRErrorCodeList(IRError error) =>
    new List<int> {400}.ToRList();
```
</details>

## Solving problems
Библиотека позволяет перейти к другому стилю написания кода, а также устранить проблемы, возникающие при классическом стиле.
### Exceptions
Каждая функция имеет собственную сигнатуру. По сигнатуре функции можно определелить не только тип входящих и исходящих параметров, но и при внимательном анализе операции, осуществляемые этой функцией. Чистые функции (pure) жестко следуют сигнатурам, не вносят никаких дополнительных действий (side-effect), легко тестируются, посколько имеют однозначную интерпретацию. 
Исключения в функциях никак не могут быть интерпретированы сигнатурой. Поэтому наличие исключений может быть уточнено только в документации к функции.
Следует различать два вида исключений: 
    - Исключения, вызванные некорректным поведением программы. Такие ошибки действительно должны проводить к сбою и прерыванию выполнения кода для быстрой отладки.
    - Исключения, вызванные исходными параметрами, с которыми фукнция не может быть исполнена. Это нормальное поведение программы. И обработка таких ошибок не должна прерывать код.
Отладка исключений - задача непростая. При плохо документированном коде достаточно сложно выяснить, где могут появиться исключения. Ситуация осложняется еще и тем, что при неграмотной архитектуре кода практически невозможно уследить без детального разбора, где были установлены перехватчики `try/catch`. Использование функциональных библиотек частично решает проблему со штатными ошибками за счет класса `IRMaybe`, который указывается в сигнатуре метода. Таким образом можно точно установить, что функция на выходе может содержать ошибку. 
### Execute result steps
Методы расширения выполняются пошагово. Для отладки необходимо ставить точки останова внутри лямбда-выражений. Как правило методы обрабатывают объекты в состоянии `Success`. В состоянии `Failure` лямбда-функция не исполняется и выполняется пропуск шага. Исключением являются расшрения типа действия `None`, обрабатывающие состояние `Failure`. А также тип действия `Match`, обрабатывающий оба состояния объекта. Как правило R объекты содержат в себе только одну ошибку, если не использовались специально предназначенные методы типа `Concat`.
![image](https://github.com/rubilnik4/ResultFunctional/assets/53042259/f13dfbab-964a-4d20-a2d4-fdb136d9445a)
Если rest функция `GetTransactions` вернет состояние `RRestError` (BadRequest, InternalError), то будет осуществлен пропуск всех последующих шагов.
