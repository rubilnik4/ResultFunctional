# Functional Result Extensions for C&#35;
[![NuGet version (ResultFunctional)](https://img.shields.io/nuget/v/ResultFunctional.svg)](https://www.nuget.org/packages/ResultFunctional/)
[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/vkhorikov/CSharpFunctionalExtensions/blob/master/LICENSE)

Библиотека предназначена для разделения исключений и преднамеренных ошибок, а также написания кода во fluent стиле с элементами функционального программирования. Возможна работа как с синхронным, так и асинхронным кодом.
## Concepts
Библиотека представляет собой методы расширений. Объект переходит из одного состояния в дргое посредством лямда-выражений. Состоит из двух частей:
- Общая. Отвечает за стандартные функции и подходит для объектов всех типов.
- Специальная (RExtensions). Оперирует result-объектами данной библиотеки и позволяет писать код в функицональном fluent стиле.

Для каждой из функций предусмотрены синхронные и асинхронные расширения.
## Common functions
### Summary
Feature | Description
| ------------ | ------------ |
`Map` | Функтор. Стандартная функция преобразования из одного объекта в другой
`Option` | Функция преобразования одного объекта в другой в зависимости от условия
`Curry` | Трансформация в функцию с меньшим количеством входных аргументов
`Void` | Операции с методами
#### Map
Стандартная функция преобразования из типа A в тип B. Применяется в цепочках, чтобы не нарушать fluent стиль.
Feature | Signature | Description
| ------------ | ------------ | ------------ |
`Map` | (a, a -> b) -> b | Преобразование одного объекта в другой
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
`Option` | (a, a -> bool, a -> b, a -> b) => b | Преобразование одного объекта в другой в зависимости от условия
`OptionSome` | (a, a -> bool, a -> a) => a | Изменение объекта при выполнении условия
`OptionNone` | (a, a -> bool, a -> a) => a | Изменение при невыполнении условия
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
`Curry` | (a -> t, a) => (() => t) | Преобразование в функцию без аргументов
`Curry` | ((a, b) -> t, a) => (b => t) | Преобразование в функцию с одним аргументом
...
`Curry` | ((a1, a2... an) -> t, a1) => (a2... an => t) | Общий вид функции для уменьшения количества аргументов
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
`Void` | (a, a => ()) => a | Выполнить действие над объектом
`VoidSome` | (a, a => bool, a => ()) => a | Выполнить действие над объектом при положительном условии
`VoidOption` | (a, a => bool, a => (), a => ()) => a | Выполнить действия над объектом в зависимости от условия
```csharp
int numeric = 1;
var result = numeric
    .VoidOption(number => number> 0,
                number => Log.Debug($"{number} is positive"),
                number => Log.Debug($"{number} is not negative"));
// or
int number = 1;
if (number > 0)
{
    Log.Debug($"{number} is positive");
}
else
{
    Log.Debug($"{number} is not negative");
}
```
### Conclusion
Сами по себе общие функции не приводят к улучшению читаемости кода. Однако они являются базой, на которой основаны дальнейшие функции.
## Result library
### Summary
Основным классом, используемым для методов расширений, является IRMaybe. Так же как и функциональный класс Option состоит из двух частией: Some и None. В качестве Some используются дженерики разных типов, None представляет собой различные вариации ошибок типа IRError. Все классы библиотеки имеют префикс R.
Class | Generic type | Description
| ------------ | ------------ | ------------ |
`IRMaybe` | - | Базовый класс, определяющий наличие ошибки
`IRUnit` | Unit | Класс с отсутствующим дженериком
`IRValue` | Value | Класс, содержащий значение
`IRList` | Collection | Класс, содержащий коллекцию значений
### Concepts
Все классы наследники IRMaybe могут находиться в двух состояниях: Success и Failure. В случае Success класс возвращает хранимое значение дженерика, а в случае Failure - коллекцию ошибок. Нахождение в промежуточном состоянии, то есть хранении и значения и ошибок - исключено. 
Property/Method | Description
| ------------ | ------------ |
`Success` | Успешное состояние
`Failure` | Состояние с ошибками
`Value` | Значение хранимой переменной. В случае Failure - null
`Errors` | Хранимые ошибки. В случае Success - null
`GetValue` | Вернуть переменную. В случае Failure выбросить исключение
`GetErrors` | Вернуть список ошибок. В случае Success выбросить исключение
`GetErrorsOrEmpty` | Вернуть список ошибок. В случае Success вернуть пустой список
### Initialization
Инициализировать классы типа IRMaybe можно несколькими способами: через статические классы, через фабрику и через методы расширений. Если методы были применены к значению, то это приведет класс к состоянию Success. Если к ошибкам типа IRError - то к состоянию типа Failure.
#### Static classes
Каждый класс имеет статические методы Some и None для перехода в состояние Success и Failure соответсвенно.
Class | Feature | Signature| Status
| ------------ | ------------ | ------------ | ------------ |
`RUnit` | `RUnit.Some` | _ => IRUnit | Success
`RUnit` | `RUnit.None` | IRError => IRUnit | Failure
`RValue` | `RValue<T>.Some` | T => IRValue<T> | Success
`RValue` | `RValue<T>.None` | IRError => IRValue<T> | Failure
`RList` | `RList<T>.Some` | IReadOnlyCollection<T> => IRList<T> | Success
`RList` | `RList<T>.None` | IRError => IRList<T> | Failure
```csharp
int number = 2;
IRValue<int> result = RValue<int>.Some(2);
return result.Success; // true   
    
IRError error = RErrorFactory.Simple("Ошибка");
IRValue<int> result = RValue<int>.None(error);
return result.Success; // false     
```
#### Factory
Class | Feature | Signature| Status
| ------------ | ------------ | ------------ | ------------ |
`RUnitFactory` | `RUnitFactory.Some` | _ => IRUnit | Success
`RUnitFactory` | `RUnitFactory.None` | IRError => IRUnit | Failure
`RValueFactory` | `RValueFactory.Some<T>` | T => IRValue<T> | Success
`RValueFactory` | `RValueFactory.None<T>` | IRError => IRValue<T> | Failure
`RListFactory` | `RListFactory.Some<T>` | IReadOnlyCollection<T> => IRList<T> | Success
`RListFactory` | `RListFactory.None<T>` | IRError => IRList<T> | Failure
```csharp
List<int> collection = new List<int>() {1, 2, 3};
IRList<int> result = RListFactory.Some(collection);
return result.Success; // true     
    
IRError error = RErrorFactory.Simple("Ошибка");
IRList<int> result = RListFactory<int>.None(error);
return result.Success; // false     
```
#### Extensions
Class | Feature | Signature| Status
| ------------ | ------------ | ------------ | ------------ |
`ToRValueExtensions` | `ToRValue<T>` | T => IRValue<T> | Success
`ToRValueExtensions` | `ToRValue<T>` | IEnumerable<IRError> => IRValue<T> | Failure
`ToRListExtensions` | `ToRList<T>` | IReadOnlyCollection<T> => IRList<T> | Success
`ToRListExtensions` | `ToRList<T>` | IEnumerable<IRError> => IRList<T> | Failure
```csharp
int number = 2;
IRValue<int> result = number.ToRValue();
return result.Success; // true   
    
IRError error = RErrorFactory.Simple("Ошибка");
IRValue<int> result = error.ToRValue<int>(error);
return result.Success; // false     
```
### Result Error
