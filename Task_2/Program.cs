using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Task_2
{
    //создаем делегат
    public delegate double Function(double x);

    class Program
    {
        //описываем функции
        public static double ExponentialFunction(double x)
        {
            return Math.Exp(x);
        }

        public static double PowerFunction(double x)
        {
            return x * x * x;
        }

        public static double AnotherPowerFunction(double x)
        {
            return Math.Pow(x, 0.5);
        }

        public static double LogarithmicFunction(double x)
        {
            return Math.Log(x);
        }

        //функция записи в файл
        public static void SaveToFile(string path, Function nameFunction, double initial, double final, double step)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            while(initial <= final)
            {
                bw.Write(nameFunction(initial));
                initial += step;
            }
            bw.Close();
            fs.Close();
        }

        //функция считывания данных из файла и поска минимального значения
        public static double LoadFromFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            double min = double.MaxValue;
            double curentValue;
            for(int i=0; i < fs.Length/sizeof(double); i++)
            {
                curentValue = br.ReadDouble();
                if (curentValue < min) min = curentValue;
            }
            br.Close();
            fs.Close();
            return min;
        }

        //функция запроса интервала с проверкой формата
        public static double[] ReadInterval()
        {
            string[] intervalString = new string[2];
            double[] interval = new double[2];

            Regex intervalFormat = new Regex(@"^([-]{0,1}[0-9]+[;]{1}[-]{0,1}[0-9]+)");

            Console.Write("Введите интервал, в котором необходимо вывести значения функции\n(через точку с запятой без пробелов):");

            string intervalReaded = Console.ReadLine();

            while(!intervalFormat.IsMatch(intervalReaded))
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

        static void Main(string[] args)
        {
            //создаем меню
            Console.WriteLine(@"Данная программа осуществляет запись в файл значений функции на заданном 
интервале с заданным шагом и поиск минимумального значения функции на данном
интервале.
Выберите интерсующую Вас функцию, введя с клавиатуры ее номер:

1. f(x) = e^x
2. f(x) = x^3
3. f(x) = x^1/2
4. f(x) = ln(x)");

            //считываем данные введенные пользователем и проверяем их на соответствие формату
            string selectedFunction = Console.ReadLine();
            Regex formatMenu = new Regex(@"[1-4]{1}\b");

            while (!formatMenu.IsMatch(selectedFunction))
            {
                Console.WriteLine("\nВы ввели что-то не то! Попробуйте еще раз.");
                selectedFunction = Console.ReadLine();
            }

            //считываем итервал
            double[] interval = ReadInterval();

            //считываем шаг и проверяем его на соответствие формата и проверяем, чтобы он был не более половины заданного интервала
            Console.Write("\nВведите шаг для вычисления значений в заданном Вами интервале: ");

            string stepString = Console.ReadLine();

            Regex stepRegex = new Regex(@"^\d{1}[,]{0,1}\d+\b");

            double step = 0.0;

                while (!formatMenu.IsMatch(selectedFunction) || (step = double.Parse(stepString)) > Math.Abs(interval[0] - interval[1]) / 2)
                {
                    Console.WriteLine("\nВы ввели что-то не то, либо шаг слишком велик для заданного интервала! Попробуйте еще раз.");
                    stepString = Console.ReadLine();
                }
            
            //делаем перестановку в интервале если он задан в неверное последовательности
            if (interval[0] > interval[1])
            {
                double helper = interval[1];
                interval[1] = interval[0];
                interval[0] = helper;
            }

            //обработка соответствующей функции в зависимости от выбранной пользователем
            switch (selectedFunction)
            {
                case "1":
                    SaveToFile(@"D:\1\valuesOfFunction.bin", ExponentialFunction, interval[0], interval[1], step);
                    Console.WriteLine($"\nМинимальное значение на данном интервале с данным шагом равно: {LoadFromFile(@"D:\1\valuesOfFunction.bin")}");
                    break;
                case "2":
                    SaveToFile(@"D:\1\valuesOfFunction.bin", PowerFunction, interval[0], interval[1], step);
                    Console.WriteLine($"\nМинимальное значение на данном интервале с данным шагом равно: {LoadFromFile(@"D:\1\valuesOfFunction.bin")}");
                    break;
                case "3":
                    SaveToFile(@"D:\1\valuesOfFunction.bin", AnotherPowerFunction, interval[0], interval[1], step);
                    Console.WriteLine($"\nМинимальное значение на данном интервале с данным шагом равно: {LoadFromFile(@"D:\1\valuesOfFunction.bin")}");
                    break;
                case "4":
                    SaveToFile(@"D:\1\valuesOfFunction.bin", LogarithmicFunction, interval[0], interval[1], step);
                    Console.WriteLine($"\nМинимальное значение на данном интервале с данным шагом равно: {LoadFromFile(@"D:\1\valuesOfFunction.bin")}");
                    break;
            }
            Console.ReadLine();
        }
    }
}
