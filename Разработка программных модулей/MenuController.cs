using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Разработка_программных_модулей
{
    internal static class MenuController
    {
        public static void RunMenu()
        {
            while (true)
            {
                UI.ShowMainMenu();
                Console.Write("\nВыберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UI.ShowHeader("Список студентов");
                        foreach (var student in DataStorage.Students)
                            student.Print();
                        Console.ReadKey();
                        break;

                    case "2":
                        UI.ShowHeader("Список групп");
                        foreach (var group in DataStorage.Groups)
                            group.Print();
                        Console.ReadKey();
                        break;

                    case "3":
                        ShowAllSubjects();
                        Console.ReadKey();
                        break;


                    case "4":
                        ShowAverageGradeByFaculty();

                        break;

                    case "5":
                        ShowAverageGradeBySubject();
                        break;
                    case "6":
                        ShowTopGroupBySubject();
                        break;
                    case "7":
                        ShowExpelListByFaculty();
                        break;
                    case "8":
                        ShowMostFailedSubject();
                        break;
                    case "9":
                        AddStudentInteractive();
                        break;

                    case "10":
                        ExpelStudentByRecordBook();
                        break;

                    case "11":
                        Console.WriteLine("Выход из программы...");
                        return;


                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Некорректный ввод! Нажмите любую клавишу...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static void ShowAverageGradeByFaculty()
        {
            UI.ShowHeader("Средний балл по группам факультета");

            Console.Write("Введите название факультета: ");
            string facultyInput = Console.ReadLine();

            var groups = DataStorage.Groups
                .Where(g => g.Faculty.Equals(facultyInput, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!groups.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Факультет не найден.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            foreach (var group in groups)
            {
                var students = DataStorage.Students
                    .Where(s => s.GroupId == group.Id)
                    .Select(s => s.Id)
                    .ToList();

                var grades = DataStorage.Subjects
                    .Where(subj => students.Contains(subj.StudentId))
                    .Select(subj => subj.Grade)
                    .ToList();

                if (grades.Count == 0)
                {
                    Console.WriteLine($"Группа {group.GroupName}: Нет оценок");
                }
                else
                {
                    double avg = grades.Average();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Группа {group.GroupName}: средний балл = {avg:F2}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
        public static void ShowAverageGradeBySubject()
        {
            UI.ShowHeader("Средний балл по каждому предмету");

            var groupedSubjects = DataStorage.Subjects
                .GroupBy(s => s.Name)
                .ToList();

            foreach (var subjectGroup in groupedSubjects)
            {
                var subjectName = subjectGroup.Key;
                var grades = subjectGroup.Select(s => s.Grade).ToList();

                double average = grades.Average();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Предмет: {subjectName} — средний балл среди всех студентов: {average:F2}");
                Console.ResetColor();
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
        // Группа с максимальным средним баллом по заданному предмету
        public static void ShowTopGroupBySubject()
        {
            UI.ShowHeader("Группа с наибольшим средним баллом по предмету");

            Console.Write("Введите название предмета: ");
            string subjectName = Console.ReadLine();

            var subjectGrades = DataStorage.Subjects
                .Where(s => s.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!subjectGrades.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Оценки по данному предмету не найдены.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            var groupAverages = DataStorage.Groups
                .Select(group => new
                {
                    Group = group,
                    Average = subjectGrades
                        .Where(g => DataStorage.Students.Any(s => s.Id == g.StudentId && s.GroupId == group.Id))
                        .Select(g => g.Grade)
                        .DefaultIfEmpty()
                        .Average()
                })
                .Where(g => g.Average > 0) // отбрасываем группы без оценок
                .OrderByDescending(g => g.Average)
                .ToList();

            if (!groupAverages.Any())
            {
                Console.WriteLine("Ни в одной группе нет оценок по данному предмету.");
            }
            else
            {
                var topGroup = groupAverages.First();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Лучшая группа по предмету \"{subjectName}\" — {topGroup.Group.GroupName}");
                Console.WriteLine($"Средний балл: {topGroup.Average:F2}");
                Console.ResetColor();
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
        public static void ShowExpelListByFaculty()
        {
            UI.ShowHeader("Список студентов на отчисление по факультетам");

            var faculties = DataStorage.Groups
                .Select(g => g.Faculty)
                .Distinct()
                .ToList();

            foreach (var faculty in faculties)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nФакультет: {faculty}");
                Console.ResetColor();

                var groupsInFaculty = DataStorage.Groups
                    .Where(g => g.Faculty == faculty)
                    .Select(g => g.Id)
                    .ToList();

                var studentsInFaculty = DataStorage.Students
                    .Where(s => groupsInFaculty.Contains(s.GroupId))
                    .ToList();

                var expelCandidates = studentsInFaculty
                    .Where(s =>
                        DataStorage.Subjects
                            .Where(subj => subj.StudentId == s.Id)
                            .Count(subj => subj.Grade == 2) > 2)
                    .ToList();

                if (expelCandidates.Count == 0)
                {
                    Console.WriteLine("Нет студентов, подлежащих отчислению.");
                }
                else
                {
                    foreach (var student in expelCandidates)
                    {
                        Console.WriteLine($"- {student.FullName} | Зачётка: {student.RecordBookNumber}");
                    }
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
        public static void ShowMostFailedSubject()
        {
            UI.ShowHeader("Предмет с наибольшим количеством неудовлетворительных оценок");

            var failCounts = DataStorage.Subjects
                .Where(s => s.Grade == 2)
                .GroupBy(s => s.Name)
                .Select(g => new
                {
                    SubjectName = g.Key,
                    FailCount = g.Count()
                })
                .OrderByDescending(g => g.FailCount)
                .ToList();

            if (failCounts.Count == 0)
            {
                Console.WriteLine("Нет неудовлетворительных оценок (оценка 2).");
            }
            else
            {
                var worst = failCounts.First();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Предмет: {worst.SubjectName} — неудовлетворительных оценок: {worst.FailCount}");
                Console.ResetColor();
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        //Добовление студента в группу
        public static void AddStudentInteractive()
        {
            UI.ShowHeader("Добавление нового студента");

            try
            {
                Console.Write("ID студента: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("ФИО: ");
                string fullName = Console.ReadLine();

                Console.Write("Номер зачётки: ");
                string recordBook = Console.ReadLine();

                Console.Write("Адрес: ");
                string address = Console.ReadLine();

                Console.Write("Стипендия (₽): ");
                decimal scholarship = decimal.Parse(Console.ReadLine());

                Console.Write("ID группы: ");
                int groupId = int.Parse(Console.ReadLine());

                var student = new Student(id, fullName, recordBook, address, scholarship, groupId);
                DataStorage.Students.Add(student);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nСтудент успешно добавлен!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка при вводе данных: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
        //Метод отчисления студентов 
        public static void ExpelStudentByRecordBook()
        {
            UI.ShowHeader("Отчисление студента");

            Console.Write("Введите номер зачётной книжки: ");
            string recordBook = Console.ReadLine();

            var student = DataStorage.Students
                .FirstOrDefault(s => s.RecordBookNumber.Equals(recordBook, StringComparison.OrdinalIgnoreCase));

            if (student == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Студент не найден.");
                Console.ResetColor();
            }
            else
            {
                DataStorage.Students.Remove(student);
                // Удалим также оценки
                DataStorage.Subjects.RemoveAll(s => s.StudentId == student.Id);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Студент {student.FullName} был отчислен.");
                Console.ResetColor();
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
        public static void ShowAllSubjects()
        {
            UI.ShowHeader("Список предметов");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{"Название",-15} | {"Семестр",-8} | {"Отчётность",-10}");
            Console.WriteLine(new string('-', 40));
            Console.ResetColor();

            foreach (var subject in DataStorage.Subjects)
            {
                subject.Print();
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }








    }
}
