
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

public class Timing
{
    TimeSpan startingTime;
    TimeSpan duration;
    public Timing()
    {
        startingTime = new TimeSpan(0);
        duration = new TimeSpan(0);
    }
    public void StopTime()
    {
        duration = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startingTime);
    }
    public void startTime()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        startingTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;
    }
    public TimeSpan Result()
    {
        return duration;
    }
}

namespace Sort
{
    class Program
    {
        static int[] QuickSort(int[] A, int L, int R)
        {
            int i = L;
            int j = R;
            var pivot = A[L];

            while (i <= j)
            {
                while (A[i] < pivot) i++;
                while (A[j] > pivot) j--;

                if (i <= j)
                {
                    (A[i], A[j]) = (A[j], A[i]);
                    i++;
                    j--;
                }
            }
            if (L < j)
                QuickSort(A, L, j);

            if (i < R)
                QuickSort(A, i, R);

            return A;
        }
        static void SelectionSort(int[] A)
        {
            for (int i = 0; i < A.Length - 1; i++)
                for (int j = i; j < A.Length; j++)
                    if (A[i] > A[j]) (A[i], A[j]) = (A[j], A[i]);
            //foreach (int i in A)
            //    Console.Write("{0} ", i);
        }
        static void BubbleSort(int[] A)
        {
            for (int i = 0; i <= A.Length - 2; i++)
                for (int j = 0; j <= A.Length - 2; j++)
                    if (A[j] > A[j + 1]) (A[j], A[j + 1]) = (A[j + 1], A[j]);
            //foreach (int i in A)
            //    Console.Write("{0} ", i);
        }
        static void Main(string[] args)
        {
            int n = 10;
            int[] A = new int[n];
            Timing time = new Timing();
            Random rd = new Random();
            for (int i = 0; i < n; i++)
                A[i] = rd.Next(0, 11);

            time.startTime();
            SelectionSort(A);
            time.StopTime();
            Console.WriteLine("\nThoi gian chay phuong phap Selection Sort la: {0}", time.Result().TotalSeconds);

            time.startTime();
            BubbleSort(A);
            time.StopTime();
            Console.WriteLine("\nThoi gian chay phuong phap Bubble Sort la: {0}", time.Result().TotalSeconds);

            time.startTime();
            A = QuickSort(A,0,n-1);
            time.StopTime();
            Console.WriteLine("\nThoi gian chay phuong phap Quick Sort la: {0}", time.Result().TotalSeconds);


        }
    }
}

