using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Разработка_программных_модулей
{
    internal static class DataStorage
    {
        
            public static List<Group> Groups { get; set; } = new List<Group>();
            public static List<Student> Students { get; set; } = new List<Student>();
            public static List<Subject> Subjects { get; set; } = new List<Subject>();
        

    }
}
