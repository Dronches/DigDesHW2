using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounterLib
{
    /// <summary>
    /// класс взаимодействия со словарем информации
    /// </summary>
    public class DictionaryWords
    {
        /// <summary>
        /// основной словарь сопоставления слов и их количества
        /// </summary>
        protected Dictionary<string, int> uniqueWords;

        /// <summary>
        /// конструктор, выделяющий память под uniqueWords
        /// </summary>
        protected DictionaryWords() => uniqueWords = new Dictionary<string, int>();

        /// <summary>
        /// Добавить слова в словарь или увеличение счетчика, если слово уже определено
        /// </summary>
        /// <param name="word">строка, которую пытаемся добавить в словарь</param>
        protected void AddToDictionary(string word)
        {
            // обрезка дефиса справа
            word = WordParser.CutHyphen(word);
            // привести к нижнему регистру
            word = word.ToLower();
            // если нашли в словаре:
            if (uniqueWords.ContainsKey(word))
                // увеличиваем значение счетчика
                uniqueWords[word] += 1;
            // если не нашли в словаре:
            else
                // 1 - начальное значение счетчика (т.к. нашли)
                uniqueWords.Add(word, 1);
        }


        /// <summary>
        /// Метод получения словаря из класса (специально оформлен не как getter)
        /// </summary>
        /// <returns>Хранимый в классе словарь</returns>
        protected Dictionary<string, int> getDictionary()
        {
            return uniqueWords;
        }

        /// <summary>
        /// Необязательная составляющая - помощь для garbage collector
        /// </summary>
        ~DictionaryWords()
        {
            uniqueWords.Clear();
        }

    }
}
