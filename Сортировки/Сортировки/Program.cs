using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сортировки
{
    class Program
    {
        static int inputInt()//функция, првоеряющая, чтобы мы не вводили фигню:)
        {
            int x;
            string Inpustr = Console.ReadLine();
            while (!Int32.TryParse(Inpustr, out x))
            {
                Console.WriteLine("Некорректный ввод данных!");
                Inpustr = Console.ReadLine();
            }
            return x;
        }

        static int Size() //функция, отвечающая за длину массива
        {
            int size;
            do
            {
                Console.WriteLine("Введите длину массива: ");
                size = inputInt();
            }
            while (size <= 0);
            return size;
        }

        static void VvodMassiva(int size, int[] N) //функция, отвечающая за ввод элементов в массив
        {
            for (int a = 0; a < size; a++)
            {
                Console.WriteLine("Введите элемент массива: ");
                N[a] = inputInt();
            }
        }

        static void Display(int size, int[] N)
        {
            Console.WriteLine("Массив:");
            for (int i = 0; i < size; i++) Console.Write(" " + N[i] + " ");
            Console.Write("\n");
        }

        static void Swap(ref int e1, ref int e2) //меняет местами элементы (это нужнл для сортировки вставки)
        {
            int temp = e1;
            e1 = e2;
            e2 = temp;
        }

        static int[] InsertionSort(int[] array) //сортировка вставки
        {
            for (int i = 0; i < array.Length; i++)
            {
                int key = array[i];
                int j = i;
                while ((j > 0) && (array[j - 1] > key))
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }
                array[j] = key;
            }
            return array;
        }

        static int[] MergeSort(int[] array) //сортировка слияния
        {
            if (array.Length == 1)
            {
                return array;
            }

            int middle = array.Length / 2;
            return Merge(MergeSort(array.Take(middle).ToArray()), MergeSort(array.Skip(middle).ToArray()));
        }

        static int[] Merge(int[] arr1, int[] arr2) //функция, подразделяющая на массивы для их сравнения
        {
            int ptr1 = 0, ptr2 = 0;
            int[] merged = new int[arr1.Length + arr2.Length];

            for (int i = 0; i < merged.Length; ++i)
            {
                if (ptr1 < arr1.Length && ptr2 < arr2.Length)
                {
                    if (arr1[ptr1] > arr2[ptr2]) merged[i] = arr2[ptr2++];
                    else merged[i] = arr1[ptr1++];
                }
                else
                {
                    if (ptr2 < arr2.Length) merged[i] = arr2[ptr2++];
                    else merged[i] = arr1[ptr1++];
                }
            }

            return merged;
        }

        static void BubbleSort(ref int[] mas) //пузырьковая сортировка
        {
            int temp;
            for (int i = 0; i < mas.Length; i++)
            {
                for (int j = mas.Length - 1; j > i; j--)
                {
                    if (mas[j - 1] > mas[j])
                    {
                        temp = mas[j - 1];
                        mas[j - 1] = mas[j];
                        mas[j] = temp;
                    }
                }
            }
        }

        static void Vivod1(int[] A) //вывод массива
        {
            for (int j = 0; j < A.Length; j++) Console.Write("{0,10:0.00}", A[j]);
            Console.WriteLine("");
        }
        static int BinarySearch(int[] mas, int val) //бинарный поиск
        {
            BubbleSort(ref mas);
            Vivod1(mas);
            int left = 0;
            if (mas[left] == val) return left + 1;
            for (int right = mas.Length, middle; left < right;)
            {
                middle = left + (right - left) / 2;
                if (middle <= 0)
                    break;
                var c = mas[middle];
                if (c == val)
                {
                    return middle + 1;
                }
                if (c < val)
                    left = middle;
                else
                    right = middle;
            }
            return -1;
        }

        //для быстрой сортировки
        static int Partition(int[] array, int minin, int maxin)
        {
            var p = minin - 1;
            for (var i = minin; i < maxin; i++)
            {
                if (array[i] < array[maxin])
                {
                    p++;
                    Swap(ref array[p], ref array[i]);
                }
            }

            p++;
            Swap(ref array[p], ref array[maxin]);
            return p;
        }

        static int[] Bistrosort(int[] array, int minin, int maxin) // сама быстрая сортировка
        {
            if (minin >= maxin)
            {
                return array;
            }

            var pivotIndex = Partition(array, minin, maxin);
            Bistrosort(array, minin, pivotIndex - 1);
            Bistrosort(array, pivotIndex + 1, maxin);

            return array;
        }
        static int[] Bistrosort(int[] array)
        {
            return Bistrosort(array, 0, array.Length - 1);
        }

        static void Main(string[] args)
        {
            int[] A;
            Console.WriteLine("Давайте отсортируем массив, как бы Вам этого не хотелось, но придется!");
            int stroki = Size();
            A = new int[stroki];
            VvodMassiva(stroki, A);
            int[] B = A;
            int n;
            bool a = true;
            while (a)
            {
                Console.WriteLine("Выберите сортировку: ");
                Console.WriteLine("1. Пузырьковая сортировка; \n2. Сортировка вставками; \n3. Быстрая сортировка;  \n4. Сортировка слиянием; \n5. Бинарный поиск.");
                A = B;
                Display(stroki, A);
                n = inputInt();
                switch(n)
                {
                    case 1:
                        BubbleSort(ref A);
                        Display(stroki, A);
                        break;
                    case 2:
                        InsertionSort(A);
                        Display(stroki, A);
                        break;
                    case 3:
                        A = Bistrosort(A);
                        Display(stroki, A);
                        break;
                    case 4:
                        A=MergeSort(A);
                        Display(stroki, A);
                        break;
                    case 5:
                        Console.WriteLine("Введите число");
                        int val = inputInt();
                        val = BinarySearch(A, val);
                        if (val == -1) Console.WriteLine("Элемент не найден");
                        else Console.WriteLine("номер элемента " + (val));
                        break;
                    default:
                        a = false;
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
