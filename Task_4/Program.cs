using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //задаем регулярное выражение для поиска телефон в форматах ХХХ-ХХ-ХХ, ХХ-ХХ-ХХ, ХХХ-ХХХ
            Regex phoneFormat = new Regex(@"\d{2,3}[-]\d{2,3}[-]\d{2}\b|\d{3}[-]\d{3}\b");
            
            //считываем текст из файла
            string allText = File.ReadAllText(@"D:\1\filewithphones.txt");
  
            //выводим на экран все совпадения с регулярным выражением
            foreach(var phone in phoneFormat.Matches(allText))
            {
                Console.WriteLine(phone);
            }
            Console.ReadLine();
        }
    }
}
