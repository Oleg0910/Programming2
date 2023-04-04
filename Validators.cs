using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Obchis_3_C
{
    class Validation
    {
        static public bool ValidateUserData(string input)
        {
            var values = input.Split(',');
            Console.ForegroundColor = ConsoleColor.Red;
            if (values.Length != 9)
            {
                Console.WriteLine("Invalid number of fields: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (!int.TryParse(values[0], out var id))
            {
                Console.WriteLine("Invalid ID: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (string.IsNullOrWhiteSpace(values[1]))
            {
                Console.WriteLine("Name is required: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (!int.TryParse(values[2], out var age))
            {
                if (age < 0 || age > 110)
                {

                    Console.WriteLine("Invalid age: {0}", input);
                    Console.ResetColor();
                    return false;
                }
            }
            if (!DateTime.TryParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
            {
                Console.WriteLine("Invalid birth date: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (!Regex.IsMatch(values[4], @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.WriteLine("Invalid email: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (!Regex.IsMatch(values[5], @"^\+?\d{12}$"))
            {
                Console.WriteLine("Invalid mobile phone: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (!Enum.TryParse<Gender>(values[6], out var gender))
            {
                Console.WriteLine("Invalid gender: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (string.IsNullOrWhiteSpace(values[7]))
            {
                Console.WriteLine("Driver license is required: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (!DateTime.TryParseExact(values[8], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var driverLicenseDate))
            {
                Console.WriteLine("Invalid driver license date: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (driverLicenseDate > DateTime.Today)
            {
                Console.WriteLine("Driver license date cannot be in the future: {0}", input);
                Console.ResetColor();
                return false;
            }
            if (driverLicenseDate < birthDate.AddYears(16))
            {
                Console.WriteLine("Driver license date must be at least 16 years after birth date: {0}", input);
                Console.ResetColor();
                return false;
            }
            
            Console.ResetColor();
            return true;
        }
    }
}
