using System;
using System.Collections.Generic;
using WordCounterLib; //  - позволяет избежать необходимость упоминать пространство имен

namespace WordCounter
{

    /// <summary>
    /// Основной класс программы
    /// </summary>
    class Program
    {

        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args">Начальные аргументы (командной строки) - не используются</param>
        static void Main(string[] args)
        {
            // вывод информации о программе
            Console.Write(PathParser.info);
            // считываем путь
            Console.Write("Введите путь к файлу: ");
            string path = Console.ReadLine();

            // выполняем парсинг и производим вывод в файл, далее выводим результат
            bool resultFlag = false; // флаг корректного выполнения программы

            /// ---------------
            /// ЗАДАНИЕ 2
            /// Получить доступ к приватному методу из dll.
            /// Класс - WordCounterLib.FileReader
            /// Метод - WordCounterLib.FileReaderParseFileToDictionary(path)
            /// --------------
           
            // получаем нужный нам метод
            var searchMethod = typeof(FileReader).GetMethod("ParseFileToDictionary", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            // если полученный объект метода пустой 
            if (searchMethod is null)
            {
                Console.WriteLine("Ошибка. Не удалось получить объект библиотеки, проверьте наличие библиотек...");
                return;
            }

            // Создаем объект класса (он доступен в стандартном варианте, в отличие от метода)
            var reader = new FileReader();
            // выполняем извлеченный метод
            var objDictionary = searchMethod?.Invoke(reader, parameters: new object[] { path }); // parameters - поясняющая мнемоника, может быть опущена

            // если объект пустой 
            if (objDictionary is not null)
                if(objDictionary.GetType() == typeof(Dictionary<string, int>))
                {
                        // создаем объект класса парсинга
                        FileWriter writer = new FileWriter();
                        resultFlag = writer.PrintToFile(path, objDictionary as Dictionary<string, int>);
                }
                // если объект не пустой -> не тот тип
                else
                    Console.WriteLine("Не удалось распознать тип результата - системная ошибка...");
            // иначе объкт пусто - будет выведена ошибка, втроенная в lib

            // ожидаем любых действий пользователя
            Console.WriteLine("Для продолжения нажмите на любую клавишу...");
            Console.ReadKey();
        }
    }
}
