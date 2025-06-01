using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Разработка_программных_модулей
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SeedData(); // ← ДО запуска меню

            // Запуск консольного меню
            /*RunMenu();

        
            
            DataSeeder.SeedData();  */   // ← заполняем тестовые данные
            MenuController.RunMenu();  // ← запускаем меню из отдельного класса
            
        


        }
        public static void SeedData()
        {
            DataStorage.Groups = new List<Group>
        {
        new Group(1, "Факультет ИТ", "ИТ-101", "Информатика", "Кафедра программирования"),
        new Group(2, "Факультет ИТ", "ИТ-102", "Программная инженерия", "Кафедра ИС"),
        new Group(3, "Факультет экономики", "ЭК-301", "Экономика", "Кафедра экономической теории")
        };

            DataStorage.Students = new List<Student>
        {
        new Student(1, "Иванов Иван", "ЗЧ1001", "г. Москва", 3000, 1),
        new Student(2, "Петрова Анна", "ЗЧ1002", "г. Тула", 2500, 1),
        new Student(3, "Сидоров Сидор", "ЗЧ2001", "г. Калуга", 0, 2),
        new Student(4, "Кузнецова Мария", "ЗЧ3001", "г. Казань", 3500, 3),
        new Student(5, "Александров Алексей", "ЗЧ3002", "г. Рязань", 0, 3)
        };

            DataStorage.Subjects = new List<Subject>
        {
        new Subject(1, "Весна", "Математика", "Экзамен", 5, 1),
        new Subject(2, "Весна", "Программирование", "Экзамен", 4, 1),
        new Subject(3, "Зима", "История", "Зачёт", 3, 2),
        new Subject(4, "Зима", "Физика", "Экзамен", 2, 2),
        new Subject(5, "Весна", "Математика", "Экзамен", 4, 3),
        new Subject(6, "Весна", "Программирование", "Экзамен", 2, 3),
        new Subject(7, "Зима", "История", "Зачёт", 2, 4),
        new Subject(8, "Весна", "Экономика", "Экзамен", 5, 4),
        new Subject(9, "Весна", "Экономика", "Экзамен", 2, 5),
        new Subject(10, "Весна", "Математика", "Экзамен", 2, 5)
        };
        }
        public static void ShowHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('=', 50));
            Console.WriteLine(title.ToUpper().PadLeft(30));
            Console.WriteLine(new string('=', 50));
            Console.ResetColor();
        }
        public static void ShowMainMenu()
        {
            ShowHeader("Система деканата");
            Console.WriteLine("1. Вывод студентов");
            Console.WriteLine("2. Вывод групп");
            Console.WriteLine("3. Вывод предметов");
            Console.WriteLine("4. Выход");
        }


    }
}
