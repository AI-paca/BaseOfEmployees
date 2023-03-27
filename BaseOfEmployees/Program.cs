using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseOfEmployees
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee[] emp = new Employee[0];
            string[] vac = Enum.GetNames(typeof(Vacancies)); int vacancy; Employee[] sortEmp;
            string[] menuItems = new string[] { "Меню", "Ввести полную информацию обо всех сотрудниках","Вывести полную информацию обо всех сотрудниках", "Вывести полную информацию о сотрудниках( по должности )", 
                "Найти в массиве всех менеджеров ( salary > average salary(клерков) )", "Вывести полную информацию обо всех сотрудниках (позже босса)", "Выход" };
            while (true)
            {
                switch (Menu.Case(menuItems))
                {
                    case 0: return;
                    case 1:
                    len: Console.WriteLine("Введите длину массива"); try { emp = new Employee[int.Parse(Console.ReadLine())]; }
                    catch { goto len; } Console.Clear();
                    for (int i = 0; i < emp.Length; i++)
                    {
                        Console.WriteLine("Введите имя:"); string name = Console.ReadLine();Console.Clear(); vacancy = Menu.Case(vac);
                    sal: Console.Write("Имя: {0}  Вакансия: {1} Зарплата: ", name, vac[vacancy]); try
                        {
                            int salary = int.Parse(Console.ReadLine()); Console.Clear();
                        date: Console.Write("Имя: {0}  Вакансия: {1} Зарплата: {2} Дата назначения:", name, vac[vacancy], salary);
                            string inputDate = Console.ReadLine();
                            try { DateTime hiredate = DateTime.Parse(inputDate); PossibleDate(hiredate); emp[i] = new Employee { name = name, vacancy = (Vacancies)vacancy, salary = salary, hiredate = hiredate }; }
                            catch { Console.WriteLine("Ошибка, введенная дата {0} невозможна", inputDate); goto date; };
                        }
                        catch { goto sal; }; Console.Clear();

                    }
                    break;
                    case 2://вывести полную информацию обо всех сотрудниках (можно использовать переопределение метода ToString() класса Object)
                    Console.WriteLine("Имя        Вакансия   Зарплата   Дата назначения");
                    for (int i = 0; i < emp.Length; i++)
                        Console.WriteLine("{0,10:0.00}|{1,10}|{2,10:0.00}|{3,10:0.00}", emp[i].name, emp[i].vacancy, emp[i].salary, emp[i].hiredate.ToString("dd.MM.yyyy"));
                    break;
                    case 3: /*вывести полную информацию о сотрудниках, работающих в указанной пользователем должности
                             (ввод должности осуществляется пользователем в данном пункте меню);*/
                    vacancy = Menu.Case(vac);
                    for (int i = 0; i < emp.Length; i++)
                        if (emp[i].vacancy == (Vacancies)vacancy)
                            Console.WriteLine("{0,10:0.00}|{1,10}|{2,10:0.00}|{3,10:0.00}", emp[i].name, emp[i].vacancy, emp[i].salary, emp[i].hiredate.ToString("dd.MM.yyyy"));
                    break;
                    case 4:/*найти в массиве всех менеджеров, зарплата которых больше средней зарплаты всех
                            клерков, вывести на экран полную информацию о таких менеджерах, отсортировать в
                            алфавитном порядке по фамилии (исходный массив остается несортированным);*/
                    int sum = 0; int count = 0; double averageSum;
                    for (int i = 0; i < emp.Length; i++)
                        if (emp[i].vacancy == (Vacancies)2)
                        {
                            sum += emp[i].salary; count += 1;
                        }
                    try { averageSum = sum / count; }
                    catch { Console.WriteLine("Клерков не сущетсвует, список всех менеджеров:"); averageSum = 0; }
                    sortEmp = SortByAlpha(emp);
                    foreach (var i in sortEmp)
                        if (i.vacancy == (Vacancies)0 && i.salary > averageSum)
                            Console.WriteLine("{0,10:0.00}|{1,10}|{2,10:0.00}|{3,10:0.00}", i.name, i.vacancy, i.salary, i.hiredate.ToString("dd.MM.yyyy"));
                    break;
                    case 5:/*вывести полную информацию обо всех сотрудниках, принятых на работу позже босса,
                            отсортированную в алфавитном порядке по фамилии сотрудника (исходный массив
                            остается несортированным);*/
                    DateTime remember = new DateTime(1973, 1, 1);
                    for (int i = 0; i < emp.Length; i++)
                        if (emp[i].vacancy == (Vacancies)1)
                        {
                            remember = emp[i].hiredate; break;
                        }
                    sortEmp = SortByAlpha(emp);
                    foreach (var i in sortEmp)
                        if (i.hiredate > remember)
                            Console.WriteLine("{0,10:0.00}|{1,10}|{2,10:0.00}|{3,10:0.00}", i.name, i.vacancy, i.salary, i.hiredate.ToString("dd.MM.yyyy"));
                    break;
                    case 6: return;
                }
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void PossibleDate(DateTime date)
        {
            DateTime minDate = new DateTime(1973, 1, 1);
            DateTime maxDate = DateTime.Now;
            if (date <= minDate && date >= maxDate)
                throw new FormatException();
        }
        static Employee[] SortByAlpha(Employee[] emd)
        {
            Employee[] sortEmp = new Employee[emd.Length];
            emd.CopyTo(sortEmp, 0);
            Array.Sort(sortEmp, new EmployeeComparer());//сортировка массива(исходный массив, экземпляр класса определяющий порядок сортировки)
            return sortEmp;
        }
    }
    class EmployeeComparer : IComparer<Employee> 
        //интерфейс, определяющий метод, который сравнивает два объекта
    {
         public int Compare(Employee v1, Employee v2)//сравнить слова
        {
            return v1.name.CompareTo(v2.name); //вернуть вернуть орентированную позицию (до/после/0)
        }
    }
class Menu
{
    private static void DrawMenu(string[] items, int index /*,int menu_width*/)
    //фронт
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (i == index)
            {
                Console.BackgroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.SetCursorPosition(/*menu_width,*/Console.WindowLeft, Console.WindowHeight / 2 + i - 4);
            Console.WriteLine(items[i]);
            Console.ResetColor();
        }
    }
    public static int Case(string[] menuItems)
    {
        int index = 1;
        while (true)
        {
            DrawMenu(menuItems, index);
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.DownArrow:
                    if (index < menuItems.Length - 1)
                        index++;
                    break;
                case ConsoleKey.UpArrow:
                    if (index > 0)
                        index--;
                    break;
                case ConsoleKey.Escape:
                    return 0;
                case ConsoleKey.Enter:
                    switch (index)
                    {
                        default:
                            Console.Clear();
                            return index;
                    }
            }
        }
    }
}

}
