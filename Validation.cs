using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talon_summer_2023
{
    public class Validation
    {
        public static string ValidateWord(string word)
        {
            foreach (char c in word)
            {
                if (!char.IsLetter(c)) throw new ArgumentException($"!!!!{word} contains not only letters!!!!");
            }

            return word;
        }

        public static int ValidateSize(int num)
        {
            if (num < 0) throw new ArgumentException("size can not be negative");

            return num;
        }


    }
}
