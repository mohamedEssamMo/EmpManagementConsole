using System;

namespace ConsoleAppDay9_1
{
    internal class Program
    {
        public delegate bool myDelegate(int L, int M);

        static bool sortAscending(int L, int M)
        {
            return (L > M); 
        }

        static bool sortDescending(int L, int M)
        {
            return (L < M); 
        }

        public static void BubbleSort(ref int[] arr, myDelegate del)
        {
            int temp;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (del(arr[j], arr[j + 1])) 
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

        static void Main(string[] args)
        {
            int[] arr = { 2, 3, 45, 98, 0, 1, 11 };

            Console.WriteLine("Ascending order:");
            BubbleSort(ref arr, sortAscending);

            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Descending order:");
            BubbleSort(ref arr, sortDescending);
        }
    }
}
