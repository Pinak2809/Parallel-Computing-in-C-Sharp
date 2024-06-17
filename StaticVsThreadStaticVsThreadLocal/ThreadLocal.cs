using System;
using System.Threading;

public class Treadlocal
{
    static ThreadLocal<int> num = new ThreadLocal<int>(() => 5);

    public static void Main()
    {
        Console.WriteLine("ThreadLocal Multithreading Context:");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine($"Main Thread - Initial Value: {num}");

        // Start a new thread and modify the num value
        Thread thread = new Thread(() =>
        {
            num.Value = 3;
            Console.WriteLine($"New Thread - Updated value: {num}");
        });
        thread.Start();
        thread.Join(); // Wait for the new thread to finish

        Console.WriteLine($"Main Thread - Final Value: {num}");
        Console.ReadLine();
    }
}