using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace o_2_C
{
    class Validation
    {
        public static bool ValidateUserData<T>(string line) where T : class, new()
        {
            var values = line.Split(',');
            var obj = new T();
            var properties = typeof(T).GetProperties();
            var isValid = true;

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
                    if (attribute is ValidationAttribute validationAttribute)
                    {
                        if (!validationAttribute.IsValid(values[i]))
                        {
                            Console.WriteLine("Invalid value: {0}", line);
                            isValid = false;
                            break;
                        }
                    }
                    else if (attribute is DataTypeAttribute dataTypeAttribute)
                    {
                        if (dataTypeAttribute.DataType == DataType.Date)
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
                    }
                    else if (attribute is EnumValueValidationAttribute enumAttribute)
                    {
                        if (propertyType.IsEnum)
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
