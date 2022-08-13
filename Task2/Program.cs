using System;
using System.IO;
using System.Security;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = "";
            if (args.Length > 0)
                dirName = args[0];
            else
                dirName = Environment.GetFolderPath(Environment.SpecialFolder.System);


            Console.WriteLine($"Папка: {dirName}, размер:{CalculateFolderSize(dirName):### ### ### ###} байт.");
        }

        static long CalculateFolderSize(string dirName)
        {
            long dirSize = 0;

            //проверяем на null чтобы не перехватывать ArgumentNullException
            if (string.IsNullOrEmpty(dirName))
            {
                Console.WriteLine($"Путь к директории не указан!");
                return 0;
            }

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                if (dirInfo.Exists)
                {
                    FileInfo[] files = dirInfo.GetFiles();
                    foreach (var file in files)
                    {
                        dirSize += file.Length;
                    }

                    DirectoryInfo[] subdirectories = dirInfo.GetDirectories();
                    foreach (var dir in subdirectories)
                    {
                        dirSize += CalculateFolderSize(dir.FullName);
                    }
                }
                else
                {
                    Console.WriteLine("Указан неверный путь к директории");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Передан некорректный путь: {ex.Message}");
            }
            catch (PlatformNotSupportedException ex)
            {
                Console.WriteLine($"ОС не поддерживается: {ex.Message}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Папка по заданному адресу не существует: \"{dirName}\"");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет прав для доступа : {ex.Message}");
            }
            catch (SecurityException ex)
            {
                Console.WriteLine($"Ошибка доступа: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка аргумента: {ex.Message}");
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine($"Путь к директории слишком длинный. {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return dirSize;
        }
    }
}
