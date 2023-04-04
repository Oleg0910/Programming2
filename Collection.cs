using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Obchis_3_C
{
    class Collection
    {
        private List<User> _users;
        private readonly string[] _userDataList = { "age", "birth_date", "driver_license", "driver_license_date", "email", "gender", "id", "mobile_phone", "name" };

        public Collection(List<User> _users= null)
        {
           this._users = _users ?? new List<User>();
        }

        public List<User> Users
        {
            get { return _users; }
        }

        public void AddUser(User user)
        {
            foreach(User u in _users)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if(u.MobilePhone == user.MobilePhone)
                {
                    Console.WriteLine("Mobile phone can't be the same");
                    Console.ResetColor();
                    return;
                }
                if(u.DriverLicense == user.DriverLicense)
                {
                    Console.WriteLine("Driver license can't be the same");
                    Console.ResetColor();
                    return;
                }
                if(u.Email == user.Email)
                {
                    Console.WriteLine("Email can't be the same");
                    Console.ResetColor();
                    return;
                }
                if(u.ID == user.ID)
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
            User userToDelete = _users.FirstOrDefault(user => user.ID == userId);
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
            User userToEdit = _users.FirstOrDefault(user => user.ID == userId);
            if (userToEdit != null)
            {
                PropertyInfo propInfo = typeof(User).GetProperty(toEdit);
                while (propInfo == null)
                {
                    Console.WriteLine("Invalid edit field. Try again: ");
                    toEdit = Console.ReadLine();
                    propInfo = typeof(User).GetProperty(toEdit);
                    Console.WriteLine("toEdit: " + toEdit);
                    var props = typeof(User).GetProperties().Select(p => p.Name);
                    Console.WriteLine("User properties: " + string.Join(", ", props));
                }
                userToEdit.GetType().GetProperty(toEdit).SetValue(userToEdit, Convert.ChangeType(value, userToEdit.GetType().GetProperty(toEdit).PropertyType));
                while (!Validation.ValidateUserData(userToEdit.ToCSVString()))
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
            List<User> users = new List<User>();

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

                    if (!Validation.ValidateUserData(line)) { continue; }

                    var values = line.Split(',');
                    var id = int.Parse(values[0]);
                    var name = values[1];
                    var age = int.Parse(values[2]);
                    var birthDate = DateTime.ParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var email = values[4];
                    var mobilePhone = values[5];
                    var gender = (Gender)Enum.Parse(typeof(Gender), values[6]);
                    var driverLicense = values[7];
                    var driverLicenseDate = DateTime.ParseExact(values[8], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var user = new User(id, name, age, birthDate, email, mobilePhone, gender, driverLicense, driverLicenseDate);
                    _users.Add(user);
                }
            }
        }

        private void RewriteFile()
        {
            string filePath = "C:\\Users\\09102\\Documents\\Навчальна_Практика\\2\\Users.csv";
            string firstLine = "id,name,birth_date,email,phone_number,gender,driver_license,driver_license_date";
            List<string> lines = new List<string>();
            lines.Add(firstLine);
            foreach(User user in _users)
            {
                lines.Add(user.ToCSVString());
            }

            // Write the temporary string back to the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach(string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        public void SearchUser(string searchCriteria)
        {
            List<User> result = new List<User>();
            foreach (User user in _users)
            {
                foreach (var property in typeof(User).GetProperties())
                {
                    if (property.GetValue(user).ToString().ToLower().Contains(searchCriteria.ToLower()))
                    {
                        result.Add(user);
                        break;
                    }
                }
            }
            foreach (User user in result)
            {
                Console.WriteLine(user.ToString());
            }
        }

        public void SortUsers(string sortField)
        {
            PropertyInfo propInfo = typeof(User).GetProperty(sortField);
            while (propInfo == null)
            {
                Console.WriteLine("Invalid sort field. Try again: ");
                sortField = Console.ReadLine();
                propInfo = typeof(User).GetProperty(sortField);
            }
            List<User> sortedUsers = _users.OrderBy(u => propInfo.GetValue(u)).ToList();
            foreach (User user in sortedUsers)
            {
                Console.WriteLine(user.ToString());
            }
        }

        public void Print()
        {
            foreach (User user in _users)
            {
                Console.WriteLine(user.ToString());
            }
        }
    }
}
