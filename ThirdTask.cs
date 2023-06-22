using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talon_summer_2023
{
    public class ThirdTask
    {
        public static string Third(SingleLinkedList<int> list)
        {
            var n = list.head;
            int count = 0;
            string series = "";
            var ser = new SingleLinkedList<int>();
            var result = new SingleLinkedList<int>();
            int sign = 0;
            while (n != null)
            {
                if (sign == 0 || Math.Sign(n.data) == Math.Sign(sign))
                {
                    result.InsertToTail(ser.ReversedList());
                    if (!ser.IsEmpty() && ser.head.next != null)
                    {
                        series += ser.PrintedList() + "\n";
                        count++;
                    }
                    ser = new SingleLinkedList<int>();
                }
                sign = n.data;
                ser.InsertToTail(n.data);
                n = n.next;
            }

            result.InsertToTail(ser.ReversedList());
            if (!ser.IsEmpty() && ser.head.next != null)
            {
                series += ser.PrintedList() + "\n";
                count++;
            }
            list.head = result.head;
            return "Num of series: " + count + "\n" + series;
        }

        public static void ThirdMenu()
        {
            while (true)
            {
                Console.WriteLine("Enter 1 - to start testing Third function \nEnter 2 - exit from testing this function");
                string choice = Console.ReadLine();
                try
                {
                    if (choice == "1")
                    {
                        Console.WriteLine("enter length of array to test");
                        int length = Validation.ValidateSize(int.Parse(Console.ReadLine()));

                        var array = new SingleLinkedList<int>();
                        for (int i = 0; i < length; i++)
                        {
                            Console.WriteLine($"enter {i} element of array to test");
                            array.InsertToTail(int.Parse(Console.ReadLine()));
                        }
                        Console.WriteLine("Result array before function:");
                        array.PrintList();
                        string s = Third(array);
                        Console.WriteLine(s);
                        Console.WriteLine("Result array:");
                        array.PrintList();
                        Console.WriteLine();
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
