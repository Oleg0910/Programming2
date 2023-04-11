using System;
using System.Collections.Generic;
using System.Linq;

class FriendlyNumbers
{
    static List<int[]> finalArray = new List<int[]>();

    static int LengthInput()
    {
        int numberOfNumbers = 0;
        bool validInput = false;

        while (!validInput)
        {
            Console.Write("Enter number of friendly numbers: ");
            if (!int.TryParse(Console.ReadLine(), out numberOfNumbers))
            {
                Console.WriteLine("Invalid input! Please enter a valid integer.");
            }
            else if (numberOfNumbers <= 0)
            {
                Console.WriteLine("Invalid input! Please enter a positive integer.");
            }
            else
            {
                validInput = true;
            }
        }

        return numberOfNumbers;
    }

    static List<int[]> FriendlyArrayCreating(int numberOfNumbers)
    {
        int i = 2;
        List<int[]> finalArray = new List<int[]>();

        while (finalArray.Count() < numberOfNumbers)
        {
            List<int> firstDivisors = Divisors(i);
            int sum1 = firstDivisors.Sum();
            List<int> secondDivisors = Divisors(sum1);
            if (secondDivisors.Sum() == i && i != sum1)
            {
                if (finalArray.Count() != 0)
                {
                    if (finalArray.Last()[0] != sum1)
                    {
                        finalArray.Add(new int[] { i, sum1 });
                    }
                }
                else
                {
                    finalArray.Add(new int[] { i, sum1 });
                }
            }

            i++;
        }

        return finalArray;
    }

    static List<int> Divisors(int number)
    {
        var divisors = new List<int>();

        for (int i = 1; i <= (double)number / 2; i++)
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
                int numberOfNumbers = LengthInput();
                List<int[]> array = FriendlyArrayCreating(numberOfNumbers);
                foreach (int[] item in array)
                {
                    Console.WriteLine(item[0] + ", " + item[1]);
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
