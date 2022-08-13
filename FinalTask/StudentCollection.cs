using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    public class StudentCollection : List<Student>
    {
         public bool LoadFromFile(string dbFileName)
        {
            if (string.IsNullOrEmpty(dbFileName))
            {
                Console.WriteLine($"Путь к файлу не указан!");
                return false;
            }

            BinaryFormatter formater = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream(dbFileName, FileMode.Open))
                {
#pragma warning disable SYSLIB0011 // Тип или член устарел
                    if (formater.Deserialize(fs) is not Student[] data)
#pragma warning restore SYSLIB0011 // Тип или член устарел
                        return false;

                    AddRange(data);
                    return true;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Передан некорректный путь: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Устройство не поддерживается: {ex.Message}");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Файл по заданному адресу не существует: \"{dbFileName}\"");
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
                Console.WriteLine($"Путь к файлу слишком длинный. {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка IO: {ex.Message}");
            }

            return false;
        }

    }
}


