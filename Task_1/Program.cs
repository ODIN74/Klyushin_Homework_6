using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public delegate double Functions(double a, double x);

    class Program
    {
        public static void Table(Functions f,double a, double initial, double final)
        {
            Console.WriteLine("Значение х | Значение f(x)");
            while (initial <= final)
            {
                Console.WriteLine(@"{0,8: 0.000} | {1,8: 0.000}", initial, f(a, initial));
                initial++;
            }
        }

        public static double Parabola(double a, double x)
        {
            return a * x * x;
        }

        static void Main(string[] args)
        {


            Console.WriteLine(@"Данная программа выводит таблицу значений в заданном интервале для функции 
f(x) = a * x^2");

            bool correctData = false;
            Regex parameterFormat = new Regex(@"^([-]{0,1}\d+\b)");

            Console.Write("Введите значение параметра \"a\" для функции: ");

            string aString = Console.ReadLine();

            while (!parameterFormat.IsMatch(aString))
            {
                Console.WriteLine("Неверно введен параметр! Попробуйте еще раз.");
                aString = Console.ReadLine();
            }

            double a = double.Parse(aString);
            string[] intervalString = new string[2];
            double[] interval = new double[2];

            Regex intervalFormat = new Regex(@"^([-]{0,1}[0-9]+[;]{1}[-]{0,1}[0-9]+)");

            Console.Write("Введите интервал, в котором необходимо вывести значения функции (через точку с запятой без пробелов):");
            do
            {
                string intervalReaded = Console.ReadLine();

                if (!intervalFormat.IsMatch(intervalReaded))
                { 
                    Console.WriteLine("Неверно введен интервал! Попробуйте еще раз.");
                    correctData = false;
                    continue;
                }
                else
                {
                    intervalString = intervalReaded.Split(';');
                    for (int i = 0; i < 2; i++)
                    {
                        interval[i] = double.Parse(intervalString[i]);
                    }
                    correctData = true;
                }

            } while (!correctData);

            Console.Clear();

            if (interval[0] >= interval[1])
            {
                Table(new Functions(Parabola), a, interval[1], interval[0]);
            }
            else
            {
                Table(new Functions(Parabola), a, interval[0], interval[1]);
            }

            Console.ReadLine();
            
        }
    }
}
