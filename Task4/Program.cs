using FinalTask;
using System;
using System.IO;
using System.Linq;
using FinalTask.Extensions;
using System.Collections.Generic;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = "";
            StudentCollection students = new();
            
            //загрузить бд студентов
            if (!students.LoadFromFile("Students.dat"))
                return;

            try
            {
                #region создать директорию на рабочем столе 
                dirName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (string.IsNullOrEmpty(dirName))
                {
                    Console.WriteLine("не удалось получить путь к рабочему столу!");
                    return;
                }
                dirName = $"{dirName}\\Students";

                if (!Directory.Exists(dirName))
                    Directory.CreateDirectory(dirName);
                #endregion

                #region Раскидать всех студентов из файла по группам 
                var groups = students.GroupBy(x => x.Group);
                foreach (var group in groups)
                    File.WriteAllLines($"{dirName}\\{group.Key}.txt", group.ToStudentsTable());
                #endregion

                Console.WriteLine($"Работа завершена. обработано студентов: {students.Count}.\nСоздано файлов: {groups.Count()}");
            }
            catch (PlatformNotSupportedException ex)
            {
                Console.WriteLine($"Платформа не поддерживается : {ex.Message}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Папка по заданному адресу не существует: \"{dirName}\"");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Нет прав для доступа : {ex.Message}");
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
           
        }
    }
}
