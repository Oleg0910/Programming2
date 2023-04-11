using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obchis_3_C
{

    class Program
    {
        static void HelpPrint()
        {
            Console.WriteLine("1) search");
            Console.WriteLine("2) sort");
            Console.WriteLine("3) add user");
            Console.WriteLine("4) delete user");
            Console.WriteLine("5) edit user");
            Console.WriteLine("6) print");
            Console.WriteLine("7) help");
            Console.WriteLine("13) exit");
        }


        static bool InputLogic(string value, ref Collection collection)
        {
            switch (value)
            {
                case "search":
                    Console.WriteLine("Enther symbol to search: ");
                    string symbol = Console.ReadLine().ToLower();
                    collection.SearchUser(symbol);
                    return true;
                case "sort":
                    Console.WriteLine("Enther field to sort: ");
                    string field = Console.ReadLine();
                    collection.SortUsers(field);
                    return true;
                case "add user":
                    collection.AddUser(User.UserInput());
                    return true;
                case "delete user":
                    Console.WriteLine("Enther ID to delete: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    collection.DeleteUser(deleteId);
                    return true;
                case "edit user":
                    Console.WriteLine("Enther ID to edit: ");
                    int editId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter field to edit: ");
                    string fieldToEdit = Console.ReadLine();
                    Console.WriteLine("Enter new value: ");
                    string newValue = Console.ReadLine();
                    collection.EditUser(editId, fieldToEdit, newValue);
                    return true;
                case "print":
                    collection.Print();
                    return true;
                case "help":
                    HelpPrint();
                    return true;
                case "exit":
                    return false;
                default:
                    Console.WriteLine("Wrong input!!!");
                    return true;
            }
        }

        static void Main(string[] args)
        {
            Collection collection = new Collection();
            bool programContinue = true;
            collection.FileUsers("C:\\Users\\09102\\Documents\\Навчальна_Практика\\2\\2\\2\\Users.csv");
            HelpPrint();

            while (programContinue)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter your choice: ");
                Console.ResetColor();
                string value = Console.ReadLine().ToLower();
                programContinue = InputLogic(value, ref collection);
            }
            var validData = "John,32,johndoe@example.com";
            var invalidData = "J3An,32,johndoe@example.com";
            Console.WriteLine(Validation.ValidateUserData<TestClass>(validData));
            Console.WriteLine(Validation.ValidateUserData<TestClass>(invalidData));
            Console.ReadKey();
        }
    }
}
