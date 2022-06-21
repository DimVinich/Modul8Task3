using System;
using System.IO;
using System.Linq;

namespace Modul8Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folder = @"D:\Temp";             //  отладочная информация, затереть перед сдачей
            long folderSize = 0;

            // Ввод и провека папки на существование
            Console.WriteLine("Введите директорию для расчёта занимаемого места :");
            folder = Console.ReadLine();

            if (!Directory.Exists(folder))
            {
                Console.WriteLine(" Вы ввели не существующую директорию. Программа прекращает свою работу.");
                Console.ReadKey();
                return;
            }

            Console.ReadKey();

        }
    }
}
