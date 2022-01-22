using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab22
{
    internal class Program
    {

        //Сформировать массив случайных целых чисел(размер задается пользователем). Вычислить сумму чисел массива и максимальное число в массиве.Реализовать решение  задачи с  использованием механизма  задач продолжения.

       static int[] CreateArray(uint n=10)
        {
                
            int[] array = new int[n];
            Random r = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = r.Next(0, 1000);
            }
            return array;

        }
        static int SumArray(int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum=sum+array[i];
            }
            return sum;
        }

        static int MaxValue(int[] array)
        {
            int maxValue = array.Max();
            return maxValue;
        }

        static void Print(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.Write("Введите количество чисел в массиве: ");
            uint k = Convert.ToUInt32(Console.ReadLine());

            int[] array = CreateArray(k);
            Task<int[]> task1 = new Task<int[]>(() => CreateArray(k));
            Task<int> task2 = task1.ContinueWith(t => SumArray(t.Result));
            Task<int> task3 = task1.ContinueWith(t => MaxValue(t.Result));
            Task task4 = task1.ContinueWith(t => Print(t.Result));
            task1.Start();
            task4.Wait();
            if (task4.IsCompleted)
            {
                Task task5 = task2.ContinueWith(t => Console.WriteLine("Сумма чисел в массиве = {0}", t.Result));
                Task task6 = task3.ContinueWith(t => Console.WriteLine("Максимальное число в массиве = {0}", t.Result));
            }
           Console.ReadKey(); 
        }
    }
}
