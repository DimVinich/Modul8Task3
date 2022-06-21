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
            int countDelFiles = 0 ;
            int countDelDirs = 0;
            long asumDelSize = 0;

            // Ввод и провека папки на существование
            Console.WriteLine("Введите директорию для расчёта занимаемого места :");
            //folder = Console.ReadLine();

            if (!Directory.Exists(folder))
            {
                Console.WriteLine(" Вы ввели не существующую директорию. Программа прекращает свою работу.");
                Console.ReadKey();
                return;
            }

            GetDirSize(folder, ref folderSize);
            Console.WriteLine("Размер директории " + folder + " : " + folderSize);

            // чего т вызвать нужно =========================

            folderSize = 0;
            GetDirSize(folder, ref folderSize);
            Console.WriteLine("Размер директории " + folder + " после удаление устаревшей информации : " + folderSize);

            Console.ReadKey();

        }

        public static void GetDirSize(string aFolder, ref long folderSize)          //  метод подсчёта размера содержимого директории
        {
            DirectoryInfo dirInfo = new DirectoryInfo(aFolder);
            DirectoryInfo[] arrDir = dirInfo.GetDirectories();
            FileInfo[] arrFile = dirInfo.GetFiles();

            try
            {
                // Суммируем размер файлов в директории
                foreach (var file in arrFile)
                {
                    folderSize = (folderSize + file.Length);
                }

                // Рекурсивно проходим по всем остальним директориям
                foreach (var dir in arrDir)
                {
                    GetDirSize(dir.FullName, ref folderSize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось считать размер файла или директории по причине\n " + ex.Message);
            }

        }

        static void DeleteFolder(string aFolder, DateTime aoldDate, ref int acountDelFiles,ref int acountDelDirs, ref long asumDelSize)                 //  метод зачистки директории от устаревшей информации
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(aFolder);
                DirectoryInfo[] arrDir = dirInfo.GetDirectories();
                FileInfo[] arrFile = dirInfo.GetFiles();

                foreach (FileInfo file in arrFile.Where(d => d.LastAccessTime < aoldDate))
                {
                    asumDelSize += file.Length;
                    acountDelFiles++;
                    file.Delete();
                }

                foreach (DirectoryInfo dir in arrDir)
                {
                    DeleteFolder(dir.FullName, aoldDate, ref acountDelFiles, ref acountDelDirs, ref asumDelSize);
                    if (dir.GetDirectories().Length == 0 && dir.GetFiles().Length == 0 && dir.LastAccessTime < aoldDate)
                    {
                        acountDelDirs++;
                        dir.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось зачистить устаревшую информаци по причине\n " + ex.Message);
            }
        }

    }
}
