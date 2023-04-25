using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            var isValid = true;
            List<string> invalidValues = new List<string>();

            if (values.Length != properties.Length)
            {
                Console.WriteLine("Invalid number of fields: {0}", line);
                return false;
            }

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var propertyType = property.PropertyType;
                var attributes = property.GetCustomAttributes(true);

                foreach (var attribute in attributes)
                {
                    if (attribute is DataTypeAttribute dataTypeAttribute)
                    {
                        if (!DateTime.TryParse(values[i], CultureInfo.InvariantCulture, DateTimeStyles.None, out var adjustedValue)
                            || adjustedValue > DateTime.Today
                            || adjustedValue < new DateTime(1950, 1, 1)
                            || adjustedValue.Day > DateTime.DaysInMonth(adjustedValue.Year, adjustedValue.Month))
                        {
                            isValid = false;
                            invalidValues.Add(values[i]);
                        }
                        break;
                    }
                    if (attribute is EnumValueValidationAttribute enumAttribute)
                    {
                        if (propertyType.IsEnum)
                        {
                            Gender gender;
                            if (!Enum.TryParse(values[i], out gender))
                            {
                                isValid = false;
                                invalidValues.Add(values[i]);
                            }
                            break;
                        }
                    }
                    if (attribute is RangeAttribute rangeAttribute)
                    {
                        if (!rangeAttribute.IsValid(values[i]))
                        {
                            isValid = false;
                            invalidValues.Add(values[i]);
                        }
                        break;
                    }
                    if (attribute is RegularExpressionAttribute regexAttribute)
                    {
                        if (!regexAttribute.IsValid(values[i]))
                        {
                            isValid = false;
                            invalidValues.Add(values[i]);
                        }
                        break;
                    }
                    if (attribute is UrlAttribute urlAttribute)
                    {
                        if (!urlAttribute.IsValid(values[i]))
                        {
                            isValid = false;
                            invalidValues.Add(values[i]);
                        }
                        break;
                    }
                    if (attribute is PhoneAttribute phoneAttribute)
                    {
                        if (!phoneAttribute.IsValid(values[i]))
                        {
                            isValid = false;
                            invalidValues.Add(values[i]);
                        }
                        break;
                    }
                    if (attribute is CreditCardAttribute creditCardAttribute)
                    {
                        if (!creditCardAttribute.IsValid(values[i]))
                        {
                            isValid = false;
                            invalidValues.Add(values[i]);
                        }
                        break;
                    }
                    if (attribute is ValidationAttribute validationAttribute)
                    {
                        if (!validationAttribute.IsValid(values[i]))
                        {
                            isValid = false;
                            invalidValues.Add(values[i]);
                        }
                    }
                }
            }
            if (!isValid) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid values: ");
                Console.ResetColor();

                int invalidIndex = 0;
                for (int i=0; i<values.Length; i++)
                {
                    if (invalidIndex < invalidValues.Count)
                    {
                        if (values[i] == invalidValues[invalidIndex])
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{invalidValues[invalidIndex]}");
                            Console.ResetColor();
                            Console.Write(",");
                            invalidIndex++;
                        }
                        else
                        {
                            Console.Write($"{values[i]},");
                        }
                    }
                    else
                    {
                        Console.Write($"{values[i]},");
                    }
                    
                }
                Console.Write("\b");
                Console.Write(' ');
                Console.WriteLine();
            }

            return isValid;
        }
    }
}
