using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talon_summer_2023
{
    public class Anagram
    {
        public static bool checkAnagram(string word1, string word2)
        {
            // if they have different length there is no point to check further
            if (word1.Length != word2.Length) return false;
            // Sarah and sarah are anagrams!
            word1 = word1.ToLower();
            word2 = word2.ToLower();
            Dictionary<char, int> letterCounter = new ();

            // loop to check every letter in both words
            for (int i = 0; i < word1.Length; i++)
            {
                // check word1 letter 
                if (letterCounter.ContainsKey(word1[i]))
                {
                    letterCounter[word1[i]]++;
                }
                else
                {
                    letterCounter.Add(word1[i], 1);
                }

                // check word2 letter
                if (letterCounter.ContainsKey(word2[i]))
                {
                    letterCounter[word2[i]]--;
                }
                else
                {
                    letterCounter.Add(word2[i], -1);
                }
            }

            // if frenquents are all zeros than words are anagrams
            return letterCounter.All(x => x.Value == 0);
        }

        public static void AnagramMenu()
        {
            while (true)
            {
                Console.WriteLine("Enter 1 - to start testing checkAnagram function \nEnter 2 - exit from testing this function");
                string choice = Console.ReadLine();
                try
                {
                    if (choice == "1")
                    {
                        Console.WriteLine("enter first word");
                        var word1 = Console.ReadLine();
                        word1 = Validation.ValidateWord(word1);

                        Console.WriteLine("enter second word");
                        var word2 = Console.ReadLine();
                        word2 = Validation.ValidateWord(word2);

                        bool res = checkAnagram(word1, word2);
                        if (res)
                        {
                            Console.WriteLine("words are anagrams");
                        }
                        else
                        {
                            Console.WriteLine("words are not anagrams");
                        }
                    }
                    else if (choice == "2")
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
