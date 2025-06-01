using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Разработка_программных_модулей
{
    internal class Subject
    {
        public int Id { get; set; }
        public string Semester { get; set; } // "Весна" или "Зима"
        public string Name { get; set; }
        public string AssessmentType { get; set; } // "Зачёт" или "Экзамен"
        public int Grade { get; set; } // 2 - 5

        public int StudentId { get; set; }

        public Subject(int id, string semester, string name, string assessmentType, int grade, int studentId)
        {
            Id = id;
            Semester = semester;
            Name = name;
            AssessmentType = assessmentType;
            Grade = grade;
            StudentId = studentId;
        }

        public void Print()
        {
            Console.ResetColor(); // Без цвета
            Console.WriteLine($"{Name,-15} | {Semester,-8} | {AssessmentType,-10}");
        }

    }
}
