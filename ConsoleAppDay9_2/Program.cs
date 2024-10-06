using System.Collections;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppDay9_2
{

    public class human
    {
        string _name;
        int _age;
        readonly string _gender = string.Empty;

        public string name { get; set; }
        public int age { get; set; }
        public string gender { get; }

        public human(string name, string gender, int age)
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
        }
        public human(string gender)
        {
            this.gender = gender;
        }
        public human()
        {

        }

        public override string ToString()
        {
            return $"The Name = {name}\nThe Age = {age}\nThe Gender = {gender}";
        }


    }
    public class emp : human
    {
        private static int _nextId = 1;
        private int _salary;
        public int Id { get; }

        // Properties
        public int salary { get; set; }

        public emp(string name, string gender, int age, int salary) : base(name, gender, age)
        {
            this.salary = salary;
            Id = _nextId++;
        }

        public emp(string gender) : base(gender)
        {
            Id = _nextId++;
        }
        public emp()
        {
            Id = _nextId++;
        }

        public override string ToString()
        {
            return $"The ID = {Id}\nThe Name = {name}\nThe Age = {age}\nThe Gender = {gender}The Salary = {salary}";
        }

    }

    public static class display_sort
    {
        public static void Display(this List<emp> myList)
        {

            for (int i = 0; i < myList.Count; i++)
            {
                Console.WriteLine($"Employee {i + 1}:");
                Console.WriteLine($"Name: {myList[i].name}");
                Console.WriteLine($"ID: {myList[i].Id}");
                Console.WriteLine($"Age: {myList[i].age}");
                Console.WriteLine($"Salary: {myList[i].salary}");
                Console.WriteLine($"Gender: {myList[i].gender}");
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        //sort Method
        static int sortBySalary(emp x, emp y)
        {
            return -1 * x.salary.CompareTo(y.salary);
        }
        static int sortByName(emp x, emp y)
        {
            return x.name.CompareTo(y.name);
        }
        static int sortById(emp x, emp y)
        {
            return x.Id.CompareTo(y.Id);
        }
        public static void sortmyList(this List<emp> myList)
        {
            Console.WriteLine("Sort by:\n1. Salary\n2. Name\n3. ID");
            int sortOption = int.Parse(Console.ReadLine() ?? "1");
            Comparison<emp> comp = sortBySalary;
            comp += sortByName;
            comp += sortById;
            switch (sortOption)
            {
                case 1:
                    myList.Sort(sortBySalary);
                    break;
                case 2:
                    myList.Sort(sortByName);
                    break;
                case 3:
                    myList.Sort(sortById);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }


    internal class Program
    {


        public static void newMethod(ref List<emp> myList)
        {
            Console.WriteLine("Enter Employee info");
            string name = GetInvalidString("Enter Name : ");
            string gender = GetInvalidString("Enter Gender: ");
            int age = GetValidInteger("Enter Age: ");
            int salary = GetValidInteger("Enter salary: ");
            emp emp = new emp(name, gender, age, salary);
            myList.Add(emp);
            Console.WriteLine("----------------------------------\n");
            Console.Write("if You need to add employee press 1 : ");
            //int add_emp = Convert.ToInt32(Console.ReadLine());
            ConsoleKeyInfo key = Console.ReadKey();
            //switch (key.Key)
            //{
            //    case ConsoleKey.NumPad1:
            //        newMethod(ref myList);
            //        break;
            //}
            if (ConsoleKey.NumPad1 == key.Key || ConsoleKey.D0 == key.Key)
            {
                newMethod(ref myList);
            }

            Console.ReadKey();
        }


        private static int GetValidInteger(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }
        private static string GetInvalidString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (input != "" && input != string.Empty)
                {
                    return input;
                }
                Console.WriteLine("Invalid input. Please enter a valid String.");
            }
        }




        static void Main(string[] args)
        {
            string[] menu = { "  New  ", "Display", "  Sort ", "  Exit " };
            int xShift = Console.WindowWidth / 2;
            int yShift = Console.WindowHeight / (menu.Length + 1);
            int position = 0;
            bool loop = true;
            //emp[] emps = new emp[3];
            List<emp> emps = new List<emp>();
            do
            {
                Console.Clear();
                for (int i = 0; i < menu.Length; i++)
                {
                    if (i == position) Console.BackgroundColor = ConsoleColor.Green;
                    else Console.BackgroundColor = ConsoleColor.Black;

                    Console.SetCursorPosition(xShift, yShift * (i + 1));
                    Console.Write(menu[i]);
                }
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        position--;
                        if (position < 0)
                            position = menu.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        position++;
                        if (position > menu.Length - 1)
                            position = 0;
                        break;
                    case ConsoleKey.Home:
                        position = 0;
                        break;
                    case ConsoleKey.End:
                        position = menu.Length - 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        switch (position)
                        {
                            case 0:
                                newMethod(ref emps);
                                break;
                            case 1:
                                emps.Display();
                                break;
                            case 2:
                                emps.sortmyList();
                                break;
                            case 3:
                                System.Environment.Exit(0);
                                break;

                        }
                        break;
                    case ConsoleKey.Escape:
                        loop = false;
                        break;
                }
            } while (loop);
        }
    }
}