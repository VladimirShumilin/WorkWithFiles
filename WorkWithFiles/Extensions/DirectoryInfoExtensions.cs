using System;
using System.IO;
using System.Security;

namespace WorkWithFiles
{
    public static class DirectoryInfoExtensions
    {
        public static bool DeleteObsoleteFiles(this DirectoryInfo dirInfo, DateTime horizont)
        {

            try
            {
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
                    folder.DeleteObsoleteFiles(horizont);
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
                Console.WriteLine($"Папка по заданному адресу не существует: \"{dirInfo.FullName}\"");
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
