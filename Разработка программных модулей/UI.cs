using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Разработка_программных_модулей
{
    internal static class UI
    {
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
            Console.WriteLine("4. Среднний бал по факультету");
            Console.WriteLine("5. Средний балл по каждому предмету");
            Console.WriteLine("6. Лучшая группа по предмету");
            Console.WriteLine("7. Студенты на отчисление");
            Console.WriteLine("8. Предмет с наибольшим числом двоек");
            Console.WriteLine("9. Добавить студента");
            Console.WriteLine("10. Отчислить студента");
            Console.WriteLine("11. Выход");





        }
    }
}
