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
Feature | Description
---------|------------
`Map` | Функтор. Стандартная функция преобразования из одного объекта в другой
`Option` | Функция преобразования одного объекта в другой в зависимости от условия
`Curry` | Трансформация в функцию с меньшим количеством входных аргументов
`Void` | Операции с методами
#### Map
Функтор. Стандартная функция преобразования из типа A в тип B. Применяется в цепочках, чтобы не нарушать fluent стиль.
```csharp
int number = 2;
string stringNumber = number.Map(convert => convert.ToString());
// or
int number = 2;
string stringNumber =number.ToString();
```
#### Option
Функция преобразования одного объекта в другой в зависимости от условия:
- someFunc, если предикат имеет значение true;
- noneFunc, если предикат имеет значение false;
Возвращаемый тип объекта в обоих функциях одинаков.
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
#### Void




