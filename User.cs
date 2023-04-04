using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obchis_3_C
{
    class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public Gender Gender { get; set; }
        public string DriverLicense { get; set; }
        public DateTime DriverLicenseDate { get; set; }

        public User()
        {
            
        }

        public User(int id, string name, int age, DateTime birthDate, string email, string mobilePhone, Gender gender, string driverLicense, DateTime driverLicenseDate)
        {
            ID = id;
            Name = name;
            Age = age;
            BirthDate = birthDate;
            Email = email;
            MobilePhone = mobilePhone;
            Gender = gender;
            DriverLicense = driverLicense;
            DriverLicenseDate = driverLicenseDate;
        }

        public override string ToString()
        {
            return $"ID: {ID}\nName: {Name}\nAge: {Age}\nBirth Date: {BirthDate.ToShortDateString()}\nEmail: {Email}\nMobile Phone: {MobilePhone}\nGender: {Gender}\nDriver License: {DriverLicense}\nDriver License Date: {DriverLicenseDate.ToShortDateString()}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User other = (User)obj;

            return ID == other.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public static bool operator ==(User user1, User user2)
        {
            if (ReferenceEquals(user1, user2))
            {
                return true;
            }

            if ((object)user1 == null || (object)user2 == null)
            {
                return false;
            }

            return user1.ID == user2.ID;
        }

        public static bool operator !=(User user1, User user2)
        {
            return !(user1 == user2);
        }

        public static User UserInput()
        {
            User user = new User();
            Console.WriteLine("Enter user information like in the example:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("id,name,age,birth_date,email,mobile_phone,gender, driver_license,driver_licese_date");
            Console.WriteLine("5,John Smith,35,1988-07-14,john.smith@gmail.com,+380957489305,Male,2020-07-14");
            Console.ResetColor();
            string line = Console.ReadLine();
            bool correct = Validation.ValidateUserData(line);
            if (correct)
            {
                var values = line.Split(',');
                user.ID = int.Parse(values[0]);
                user.Name = values[1];
                user.Age = int.Parse(values[2]);
                user.BirthDate = DateTime.ParseExact(values[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                user.Email = values[4];
                user.MobilePhone = values[5];
                user.Gender = values[6].Equals("M") ? Gender.Male : Gender.Female;
                user.DriverLicense = values[7];
                user.DriverLicenseDate = DateTime.ParseExact(values[8], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            return user;
        }

        public string ToCSVString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID).Append(",")
              .Append(Name).Append(",")
              .Append(Age).Append(",")
              .Append(BirthDate.ToString("yyyy-MM-dd")).Append(",")
              .Append(Email).Append(",")
              .Append(MobilePhone).Append(",")
              .Append(Gender).Append(",")
              .Append(DriverLicense).Append(",")
              .Append(DriverLicenseDate.ToString("yyyy-MM-dd"));

            return sb.ToString();
        }
    }

    enum Gender
    {
        Male,
        Female
    }
}
