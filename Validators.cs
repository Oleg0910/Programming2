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
        public static bool ValidateUserData<T>(string line) where T : class, new()
        {
            var values = line.Split(',');
            var obj = new T();
            var properties = typeof(T).GetProperties();

            if (values.Length != properties.Length)
            {
                Console.WriteLine("Invalid number of fields: {0}", line);
                return false;
            }

            var isValid = true;
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var propertyType = property.PropertyType;

                if (propertyType == typeof(int))
                {
                    if (!int.TryParse(values[i], out var value))
                    {
                        Console.WriteLine("Invalid value: {0}", line);
                        isValid = false;
                        break;
                    }
                }
                else if (propertyType == typeof(float))
                {
                    if (!float.TryParse(values[i], out var value))
                    {
                        Console.WriteLine("Invalid value: {0}", line);
                        isValid = false;
                        break;
                    }
                }
                else if (propertyType == typeof(decimal))
                {
                    if (!decimal.TryParse(values[i], out var value))
                    {
                        Console.WriteLine("Invalid value: {0}", line);
                        isValid = false;
                        break;
                    }
                }
                else if (propertyType == typeof(DateTime))
                {
                    if (!DateTime.TryParseExact(values[i], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var value))
                    {
                        Console.WriteLine("Invalid value: {0}", line);
                        isValid = false;
                        break;
                    }
                    if (value > DateTime.Today || value < new DateTime(1950, 1, 1))
                    {
                        Console.WriteLine("Invalid value: {0}", line);
                        isValid = false;
                        break;
                    }
                }
                else if (propertyType.IsEnum)
                {
                    var attributes = property.GetCustomAttributes(true);
                    foreach (var attribute in attributes)
                    {
                        if (attribute is EnumValueValidationAttribute enumAttribute)
                        {
                            if (!enumAttribute.IsValid(Enum.Parse(propertyType, values[i])))
                            {
                                Console.WriteLine("Invalid value: {0}", line);
                                isValid = false;
                                break;
                            }
                        }
                    }
                }

                if (!isValid)
                {
                    break;
                }
            }

            return isValid;
        }
    }
}
