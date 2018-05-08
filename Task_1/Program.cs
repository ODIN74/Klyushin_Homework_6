using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{



    class Program
    {
        //создаем делегат
        public delegate double Functions(double a, double x);

        //метод вывода таблицы для параболы
        public static void Table(Functions f,double a, double initial, double final)
        {
            Console.WriteLine("Значение х | Значение f(x)");
            while (initial <= final)
            {
                Console.WriteLine(@"{0,10: 0.000} | {1,10: 0.000}", initial, f(a, initial));
                initial++;
            }
        }

        //метод вывода таблицы для синусоиды
        public static void TableSinus(Functions f, double a, double initial, double final)
        {
            double initialRad = initial * Math.PI / 180;
            double finalRad = final * Math.PI / 180;

            Console.WriteLine("Значение х | Значение f(x)");
            while (initialRad <= finalRad)
            {
                Console.WriteLine(@"{0,10: 0.000} | {1,10: 0.000}", initial, f(a, initialRad));
                initialRad += (10*Math.PI/180);
                initial += 10;
            }
        }

        //задаем параболу
        public static double Parabola(double a, double x)
        {
            return a * x * x;
        }

        //задаем синусоиду
        public static double Sinus(double a, double angle)
        {
            return a * Math.Sin(angle);
        }

        //метод считывания параметра "a" (с проверкой на корректность данных)
        public static double ReadParametr()
        {
            Regex parameterFormat = new Regex(@"^([-]{0,1}\d+\b)");

            Console.Write("Введите значение параметра \"a\" для функции: ");

            string aString = Console.ReadLine();

            while (!parameterFormat.IsMatch(aString))
            {
                Console.WriteLine("Неверно введен параметр! Попробуйте еще раз.");
                aString = Console.ReadLine();
            }

            return double.Parse(aString);
        }

        //метод считывания параметра интервала (с проверкой на корректность данных)
        public static double[] ReadInterval()
        {
            string[] intervalString = new string[2];
            double[] interval = new double[2];

            Regex intervalFormat = new Regex(@"^([-]{0,1}[0-9]+[;]{1}[-]{0,1}[0-9]+)");

            Console.Write("Введите интервал, в котором необходимо вывести значения функции (через точку с запятой без пробелов):");

            string intervalReaded = Console.ReadLine();

            while (!intervalFormat.IsMatch(intervalReaded))
            {
                    Console.WriteLine("Неверно введен интервал! Попробуйте еще раз.");
                intervalReaded = Console.ReadLine();
            }
            intervalString = intervalReaded.Split(';');
            for (int i = 0; i < 2; i++)
                {
                    interval[i] = double.Parse(intervalString[i]);
                }
            return interval;
        }

        //метод выводящий на консоль таблицу значений для пораболы
        public static void RunProgrammPorabola()
        {
            Console.WriteLine(@"Данная программа выводит таблицу значений в заданном интервале для функции 
f(x) = a * x^2");

            double a = ReadParametr();
            double[] interval = ReadInterval();

            Console.Clear();

            if (interval[0] >= interval[1])
            {
                Table(new Functions(Parabola), a, interval[1], interval[0]);
            }
            else
            {
                Table(new Functions(Parabola), a, interval[0], interval[1]);
            }
        }

        //метод выводящий на консоль таблицу значений для синусоиды
        public static void RunProgrammSinus()
        {
            Console.WriteLine(@"Данная программа выводит таблицу значений в заданном интервале для функции 
f(x) = a * sin(x). Интервал вводится в градусах.");

            double a = ReadParametr();
            double[] interval = ReadInterval();

            Console.Clear();

            if (interval[0] >= interval[1])
            {
                TableSinus(new Functions(Sinus), a, interval[1], interval[0]);
            }
            else
            {
                TableSinus(new Functions(Sinus), a, interval[0], interval[1]);
            }
        }

        static void Main(string[] args)
        {
            //вывод в консоль значений параболы
            RunProgrammPorabola();
            Console.ReadLine();

            Console.Clear();

            //вывод в консоль значений синусоиды
            RunProgrammSinus();
            Console.ReadLine();
        }

        
    }
}
