using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class FriendlyNumbers
{
    static List<int[]> FinalArray = new List<int[]>();

    static int LengthInput()
    {
        int length;
        bool validInput = false;

        do
        {
            Console.Write("Enter length of friendly numbers array: ");
            if (!int.TryParse(Console.ReadLine(), out length))
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
            }
            else if (length <= 0)
            {
                Console.WriteLine("Invalid input! Please enter a positive integer.");
            }
            else
            {
                validInput = true;
            }
        } while (!validInput);

        return length;
    }

    static List<int[]> FriendlyArrayCreating(int numberOfNumbers)
    {
        int i = 2;

        while (FinalArray.Count < numberOfNumbers)
        {
            IEnumerable<int> firstDivisors = Divisors(i).Take(Divisors(i).Count() - 1);
            int sum1 = firstDivisors.Sum();
            IEnumerable<int> secondDivisors = Divisors(sum1).Take(Divisors(sum1).Count() - 1);
            if (secondDivisors.Sum() == i && i != sum1)
            {
                if (FinalArray.Count != 0)
                {
                    if (FinalArray.Last()[0] != sum1)
                    {
                        FinalArray.Add(new int[] { i, sum1 });
                    }
                }
                else
                {
                    FinalArray.Add(new int[] { i, sum1 });
                }
            }

            i++;
        }

        return FinalArray;
    }

    static List<int> Divisors(int number)
    {
        List<int> divisors = new List<int>();

        for (int i = 1; i <= number; i++)
        {
            if (number % i == 0)
            {
                divisors.Add(i);
            }
        }

        return divisors;
    }

    static void HelpPrint()
    {
        Console.WriteLine("1) start");
        Console.WriteLine("2) help");
        Console.WriteLine("3) exit");
    }

    static bool InputLogic(string value)
    {
        switch (value)
        {
            case "start":
                int length = LengthInput();
                List<int[]> array = FriendlyArrayCreating(length);
                foreach (int[] item in array)
                {
                    Console.WriteLine("1) " + item[0] + ", " + item[1]);
                }
                return true;
            case "help":
                HelpPrint();
                return true;
            case "exit":
                return false;
            default:
                Console.WriteLine("Wrong input!!!");
                return true;
        }
    }

    static void Main()
    {
        bool programContinue = true;
        HelpPrint();

        while (programContinue)
        {
            Console.Write("Enter your choice: ");
            string value = Console.ReadLine().ToLower();
            programContinue = InputLogic(value);
        }
    }
}