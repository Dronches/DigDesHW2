# DigDesHW2
### *Задание на использование рефлексии:*
+ Создать класс с приватным методом - сделать из данного класса библиотеку (dll)
+ Реализовать взаимодействие из основного кода (exe) с приватным методом класса из библиотеки
### *Результат выполнения задания:*
  1. *Проект WordCounterLib*
  - На выход в результате сборки дает баблиотеку в виде dll-файла
  - Состоит из 3 классов с методами различного уровня доступности
  - Метод, вызваемый основной программой - FileReader.ParseFileToDictionary - имеет модификатор доступа private
  - Метод принимает путь к файлу, с помощью других методов и классов библиотеки реализует обработку данных файла, возвращает результат в виде списка.
  2. *Проект WordCounter*
  - Состоит из 5 классов, включая основной класс (Program с методом main)
  - Комплируется в exe, является основным - использующим WordCounterLib.dll
  - Основной код рефлексивного вызова метода расположен в основной функции Main
  - Передает в функцию из библиотеки путь к файлу, а в качестве релультата получает список сопоставления слова и количества его употреблений
