using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talon_summer_2023
{
    public class MergeSort
    {
        static public void Merge<T>(T[] first, T[] second, T[] result) where T : IComparable<T>
        {
            if (first.Length == 0) return;
            if (second.Length == 0) return;

            int counter = 0;
            int firstCounter = 0;
            int secondCounter = 0;
            while (counter < result.Length)
            {
                if (firstCounter < first.Length && secondCounter < second.Length){
                    if (first[firstCounter].CompareTo(second[secondCounter]) <= 0) {
                        result[counter++] = first[firstCounter++];
                    }
                    else
                    {
                        result[counter++] = second[secondCounter++];
                    }
                }
                else {
                    if (firstCounter < first.Length)
                    {
                        result[counter++] = first[firstCounter++];
                    }
                    else if (secondCounter < second.Length)
                    {
                        result[counter++] = second[secondCounter++];
                    }
                }
            }
        }
        static public void mergeSort<T>(T[] array) where T : IComparable<T>
        {
            if (array.Length < 2) return;

            int middleIndex = array.Length / 2;
            T[] left = new T[middleIndex];
            T[] right = new T[array.Length - middleIndex];
            Array.Copy(array, 0, left, 0, middleIndex);
            Array.Copy(array, middleIndex, right, 0, array.Length - middleIndex);

            mergeSort(left);
            mergeSort(right);

            Merge(left, right, array);
        }

        public static void MergeSortMenu()
        {
            while (true)
            {
                Console.WriteLine("Enter 1 - to start testing mergeSort function \nEnter 2 - exit from testing this function");
                string choice = Console.ReadLine();
                try
                {
                    if (choice == "1")
                    {
                        Console.WriteLine("enter length of array to sort");
                        int length = Validation.ValidateSize(int.Parse(Console.ReadLine()));
                        
                        int[] array = new int[length];
                        for (int i = 0; i < length; i++)
                        {
                            Console.WriteLine($"enter {i} element of array to sort");
                            array[i] = int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("Result array before sorting:");
                        foreach (var n in array) Console.Write(n + " ");
                        Console.WriteLine();
                        mergeSort(array);
                        Console.WriteLine("Result array after sorting:");
                        foreach (var n in array) Console.Write(n + " ");
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
