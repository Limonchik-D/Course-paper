using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Разработка_программных_модулей
{
    internal class Group
    {
        public int Id { get; set; }
        public string Faculty { get; set; }
        public string GroupName { get; set; }
        public string Specialty { get; set; }
        public string Department { get; set; }

        public Group(int id, string faculty, string groupName, string specialty, string department)
        {
            Id = id;
            Faculty = faculty;
            GroupName = groupName;
            Specialty = specialty;
            Department = department;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Группа: {GroupName} ({Faculty})");
            Console.ResetColor();
            Console.WriteLine($"Специальность: {Specialty}");
            Console.WriteLine($"Кафедра: {Department}");
            Console.WriteLine(new string('-', 40));
        }
    }
}
