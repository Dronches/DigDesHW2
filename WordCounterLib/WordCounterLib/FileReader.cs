using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace WordCounterLib
{

    /// <summary>
    /// Класс считывания данных и их обработки - подсчет количества слов в тексте
    /// </summary>
    public class FileReader : DictionaryWords
    {
        /// <summary>
        /// Раздление строки по словам - запись их в словар
        /// </summary>
        /// <param name="line">строка, которая разделяется на слова</param>
        private void ParseLine(string line)
        {
            bool wasStart = false; // вспомогательная переменная, определяющая - было ли начало
            int posStart = 0; // переменная, запоминающая позицию начала слова
            int posSaver = 0; // вспомогательная переменная, позволяющая определить принадлежность к сноске - является ли значение в квадратных скобках числом
            // пробег по всем буквам
            for (int posEnd = 0; posEnd < line.Length; ++posEnd)
            {
                // Если буква или число (считаем их за возможный элемент слова. Например, "Юпитер-1", "1T"  "1999")
                if (Char.IsLetter(line[posEnd]) || Char.IsDigit(line[posEnd]))
                {
                    // если дошли до начала - начало слова, сохраняем позиции
                    if (!wasStart)
                    {
                        posStart = posEnd;
                        wasStart = true;
                    }
                }
                // если символы, которые могут быть в слове
                else if (line[posEnd] == '\'' || line[posEnd] == '-')
                {
                    // продолжаем движение
                    continue;
                }
                // если [54] - специальное обозначение сноски, однакое если [49 год], то считываются слова 49 и год
                else if (line[posEnd] == '[')
                {
                    // если мы считывали слово, то добавляем его
                    if (wasStart)
                    {
                        AddToDictionary(line.Substring(posStart, posEnd - posStart));
                        wasStart = false; // обнуляем флаг начала
                    }

                    posSaver = posEnd + 1;
                    // движемся до конца строки или до ']'
                    while (posSaver < line.Length && line[posSaver] != ']')
                        ++posSaver;

                    // еcли дошли до ']', если имеем число внутри скобок - значит сноска
                    if (posSaver < line.Length && line[posSaver] == ']' && line.Substring(posEnd + 1, posSaver - posEnd - 1).Trim().All(char.IsDigit))
                        // производим переход на позицию после сноски
                        posEnd = posSaver;
                    // иначе продолжаем идти по циклу
                }
                // если символ, который не относится к слову
                else
                {
                    // если мы считывали слово, то добавляем его
                    if (wasStart)
                    {
                        AddToDictionary(line.Substring(posStart, posEnd - posStart));
                        wasStart = false; // обнуляем флаг начала
                    }
                }
            }
            // если закончили словом, то добавляем его
            if (wasStart)
                AddToDictionary(line.Substring(posStart, line.Length - posStart));
        }



        /// <summary>
        /// Основной метод парсинга всех слов в файле
        /// 1. Считывает данные по пути
        /// 2. Для каждой строки производит разбиение по словам с помощью других вспомогательных методов, добавляет в словарь
        /// </summary>
        /// <returns>словарь результата парсинга - словарь строк с количеством вхождений в тексте</returns>
        private Dictionary<string, int> ParseFileToDictionary(string path)
        {
            try
            {
                // Считываем из файла
                using (StreamReader reader = new StreamReader(path, Encoding.Default))
                {
                    string line = "";  // переменная считывания
                                       // считываем построчно файл, пока есть строки
                    while ((line = reader.ReadLine()) != null)
                        ParseLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось открыть файл '" + path + "' для чтения.");
                return null;
            }
            return getDictionary();
        }
    }
}
