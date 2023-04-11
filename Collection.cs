using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace o_2_C
{
    class Collection<T> where T: class, IClassGenerics<T>, new()
    {
        private List<T> _users;
        private readonly string[] _userDataList = { "age", "birth_date", "driver_license", "driver_license_date", "email", "gender", "id", "mobile_phone", "name" };

        public Collection(List<T> _users = null)
        {
            this._users = _users ?? new List<T>();
        }

        public List<T> Users
        {
            get { return _users; }
        }

        public void AddUser(T user)
        {
            foreach (T u in _users)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (u.ID == user.ID)
                {
                    Console.WriteLine("ID can't be the same");
                    Console.ResetColor();
                    return;
                }
                Console.ResetColor();
            }
            _users.Add(user);
            RewriteFile();
        }

        public void DeleteUser(int userId)
        {
            T userToDelete = _users.FirstOrDefault(user => user.ID == userId);
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);
                RewriteFile();
            }
            else
            {
                Console.WriteLine("There isn't user with this id");
            }
        }

        public void EditUser(int userId, string toEdit, string value)
        {
            T userToEdit = _users.FirstOrDefault(user => user.ID == userId);
            if (userToEdit != null)
            {
                PropertyInfo propInfo = typeof(T).GetProperty(toEdit);
                while (propInfo == null)
                {
                    Console.WriteLine("Invalid edit field. Try again: ");
                    toEdit = Console.ReadLine();
                    propInfo = typeof(T).GetProperty(toEdit);
                    Console.WriteLine("toEdit: " + toEdit);
                    var props = typeof(T).GetProperties().Select(p => p.Name);
                    Console.WriteLine("User properties: " + string.Join(", ", props));
                }
                userToEdit.GetType().GetProperty(toEdit).SetValue(userToEdit, Convert.ChangeType(value, userToEdit.GetType().GetProperty(toEdit).PropertyType));
                while (!Validation.ValidateUserData<T>(userToEdit.ToCSVString()))
                {
                    Console.WriteLine("Try again: ");
                    toEdit = Console.ReadLine();
                    userToEdit.GetType().GetProperty(toEdit).SetValue(userToEdit, Convert.ChangeType(value, userToEdit.GetType().GetProperty(toEdit).PropertyType));

                }
                RewriteFile();
            }
        }

        public void FileUsers(string filePath)
        {
            List<T> users = new List<T>();

            using (var reader = new StreamReader(filePath))
            {
                if (!reader.EndOfStream)
                {
                    //skip header line
                    reader.ReadLine();
                }
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (!Validation.ValidateUserData<T>(line)) { continue; }

                    var values = line.Split(',');
                    var properties = typeof(T).GetProperties();
                    var obj = new T();

                    for (int i = 0; i < values.Length; i++)
                    {
                        var property = properties[i];
                        var propertyType = property.PropertyType;
                        var value = values[i];

                        if (propertyType.IsEnum)
                        {
                            Enum.TryParse(propertyType, value, out var enumValue);
                            property.SetValue(obj, enumValue);
                        }
                        else
                        {
                            var convertedValue = Convert.ChangeType(value, propertyType);
                            property.SetValue(obj, convertedValue);
                        }
                    }

                    users.Add(obj);
                }
            }
            foreach (var user in users)
            {
                _users.Add(user);
            }
        }

        private void RewriteFile()
        {
            string filePath = "C:\\Users\\09102\\Documents\\Навчальна_Практика\\3\\3\\3\\Users.csv";
            string firstLine = "id,name,birth_date,email,phone_number,gender,driver_license,driver_license_date";
            List<string> lines = new List<string>();
            lines.Add(firstLine);
            foreach (T user in _users)
            {
                lines.Add(user.ToCSVString());
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        public void SearchUser(string searchCriteria)
        {
            List<T> result = new List<T>();
            foreach (T user in _users)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    if (property.GetValue(user).ToString().ToLower().Contains(searchCriteria.ToLower()))
                    {
                        result.Add(user);
                        break;
                    }
                }
            }
            foreach (T user in result)
            {
                Console.WriteLine(user.ToString());
            }
        }

        public void SortUsers(string sortField)
        {
            PropertyInfo propInfo = typeof(T).GetProperty(sortField);
            while (propInfo == null)
            {
                Console.WriteLine("Invalid sort field. Try again: ");
                sortField = Console.ReadLine();
                propInfo = typeof(T).GetProperty(sortField);
            }
            List<T> sortedUsers = _users.OrderBy(u => propInfo.GetValue(u)).ToList();
            foreach (T user in sortedUsers)
            {
                Console.WriteLine(user.ToString());
            }
        }

        public void Print()
        {
            foreach (T user in _users)
            {
                Console.WriteLine(user.ToString());
            }
        }
    }
}
