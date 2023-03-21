using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text.RegularExpressions;



namespace ConsoleApp_IT_2
{
    internal class Program
    {
        static string wordSubstitutions(string text, string word, string newWord)
        {
            Console.WriteLine($"Происходит замена '{word}' --> '{newWord}':");
            var indices = new List<int>();
            int ind = text.IndexOf(word);
            if (ind == -1)
                throw new Exception("Такого слова нет в тексте(");
            while (ind > -1)
            {
                string pattern = @"[A-zА-я]";
                if (Regex.IsMatch(text.Substring(ind + word.Length, 1), pattern) ||
                    ind != 0 && Regex.IsMatch(text.Substring(ind - 1, 1), pattern))
                {
                    ind = text.IndexOf(word, ind + word.Length);
                    continue;
                }
                indices.Add(ind);
                ind = text.IndexOf(word, ind + word.Length);
            }
            string newText = text.Substring(0, indices[0]) + newWord;
            
            for (int i  = 1; i < indices.Count; i++)
                newText += text.Substring(indices[i - 1] + word.Length, indices[i] - indices[i - 1] - word.Length) + newWord;

            newText += text.Substring(indices[indices.Count - 1] + word.Length);
            return newText;
        }
        static void Main(string[] args)
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader(@"D:\sharpi\ConsoleApp_IT_2\ConsoleApp_IT_2\text.txt");
                line = sr.ReadLine();
                string text = "";
                while (line != null)
                {
                    text += line + "\n";
                    line = sr.ReadLine();
                }
                Console.WriteLine($"Исходный текст:\n{text}");
                Console.WriteLine("Введите слово из текста:");
                string word = Console.ReadLine();
                Console.WriteLine($"Введите слово для замены слова '{word}':");
                string newWord = Console.ReadLine();
                Console.WriteLine($"Новый текст:\n{wordSubstitutions(text, word, newWord)}");
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
