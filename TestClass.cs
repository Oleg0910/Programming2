using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace o_2_C
{
    internal class TestClass: IClassGenerics<TestClass>
    {
        [Required(ErrorMessage = "ID is required")]
        public int ID { get; set; }

        [RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "Name must start with a capital letter and not contain numbers")]
        public string Name { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        public static TestClass UserInput()
        {
            TestClass user = new TestClass();
            Console.WriteLine("Enter user information like in the example:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("id,name,age,email");
            Console.WriteLine("1,John,35,john.smith@gmail.com");
            Console.ResetColor();
            string line = Console.ReadLine();
            bool correct = Validation.ValidateUserData<TestClass>(line);
            if (correct)
            {
                var values = line.Split(',');
                user.ID = int.Parse(values[0]);
                user.Name = values[1];
                user.Age = int.Parse(values[2]);
                user.Email = values[3];
            }

            return user;
        }

        public string ToCSVString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID).Append(",")
              .Append(Name).Append(",")
              .Append(Age).Append(",")
              .Append(Email);

            return sb.ToString();
        }

        public override string ToString()
        {
            return $"ID: {ID}\nName: {Name}\nAge: {Age}\nEmail: {Email}";
        }

    }
}
