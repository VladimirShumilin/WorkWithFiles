using System;
using System.IO;
using System.Security;

namespace Task1
{
    class Program
    {

        static void Main(string[] args)
        {
     
            string dir = "";
            if (args.Length > 0)
                    dir = args[0];
            
            while (!DeleteObsoleteFiles(dir,DateTime.Now.AddMinutes(-30)))
            {
                Console.WriteLine("Введите путь к директории или нажмите Ctrl+c для выхода");
                dir = Console.ReadLine();
            }  
        }

        static bool DeleteObsoleteFiles(string dirName,DateTime horizont )
        {

            try
            {
                //проверяем на null чтобы не перехватывать ArgumentNullException
                if (string.IsNullOrEmpty(dirName))
                {
                    Console.WriteLine($"Путь к директории не указан!");
                    return false;
                }

                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                if (!dirInfo.Exists)
                {
                    Console.WriteLine($"Путь \"{dirName}\" не существует!");
                    return false;
                }


                FileInfo[] fileNames = dirInfo.GetFiles();
                int filesCount = 0;
                foreach (var file in fileNames)
                {
                    if (file.LastAccessTime.CompareTo(horizont) < 0)
                    {
                        file.Delete();
                        filesCount++;
                    }
                }
                DirectoryInfo[] folderNames = dirInfo.GetDirectories();
                foreach (var folder in folderNames)
                {
                    DeleteObsoleteFiles(folder.FullName, horizont);
                    if (folder.LastAccessTime.CompareTo(horizont) < 0)
                        folder.Delete(true);
                    
                }
                Console.WriteLine($"Директория: {dirInfo.FullName} удалено файлов: {filesCount}");

                return true;
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
            return false;
        }
    }
}
