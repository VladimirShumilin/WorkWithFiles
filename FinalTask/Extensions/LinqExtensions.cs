using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask.Extensions
{
    public static class LinqExtensions
    {
        public static List<string> ToStudentsTable<TSource>(this IEnumerable<TSource> source)
            where TSource: Student
        {
            List<string> result = new List<string>();

            if (source == null)
                throw new ArgumentException("входящий массив не может быть null");

            if (source.Count() == 0)
                return result;


            foreach (Student student in source)
            {
                result.Add($"{student.Name} {student.DateOfBirth:dd.MM.yyyy}");
            }

            return result;
        }
    }
}
