using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Разработка_программных_модулей
{
    internal class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string RecordBookNumber { get; set; }
        public string Address { get; set; }
        public decimal Scholarship { get; set; }
        public int GroupId { get; set; }

        public Student(int id, string fullName, string recordBook, string address, decimal scholarship, int groupId)
        {
            Id = id;
            FullName = fullName;
            RecordBookNumber = recordBook;
            Address = address;
            Scholarship = scholarship;
            GroupId = groupId;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Студент: {FullName}");
            Console.ResetColor();
            Console.WriteLine($"Зачётка: {RecordBookNumber} | Адрес: {Address} | Стипендия: {Scholarship}₽");
            Console.WriteLine(new string('-', 40));
        }
    }
}
